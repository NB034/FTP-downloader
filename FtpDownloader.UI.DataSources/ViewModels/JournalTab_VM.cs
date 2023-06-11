using FtpDownloader.Services.Interfaces.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.UI.DataSources.DataTypes;
using FtpDownloader.UI.DataSources.Command;
using FtpDownloader.UI.DataSources.Mappers;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.UI.DataSources.ViewModels
{
    public class JournalTab_VM : INotifyPropertyChanged
    {
        private readonly CustomizableCommand _searchCommand;
        private readonly CustomizableCommand _resetCommand;
        private readonly CustomizableCommand _removeEntryCommand;
        private readonly CustomizableCommand _removeAllEntriesCommand;

        private readonly IJournal _journal;
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly LogicLayerMapper _logicLayerMapper;
        private readonly DownloadDtoToEntryDtoMapper _dtoMapper;
        private JournalEntry _entry = null;

        private string _searchLine = "";
        private bool _isLoading = false;


        public JournalTab_VM(IJournal journal,
            IDownloader downloader,
            NotificationPanel_VM notificationPanel,
            LogicLayerMapper logicLayerMapper,
            DownloadDtoToEntryDtoMapper dtoMapper)
        {
            _logicLayerMapper = logicLayerMapper;
            _dtoMapper = dtoMapper;
            _journal = journal;
            _notificationPanel = notificationPanel;

            _searchCommand = new CustomizableCommand(_ => Search(), _ => CanSearch());
            _resetCommand = new CustomizableCommand(_ => Reset(), _ => CanReset());
            _removeEntryCommand = new CustomizableCommand(_ => RemoveEntry(), _ => CanRemoveEntry());
            _removeAllEntriesCommand = new CustomizableCommand(_ => RemoveAllEntries(), _ => CanRemoveAllEntries());

            JournalEntries = new ObservableCollection<JournalEntry>();

            downloader.DownloadCompleted += AddEntry;
            downloader.DownloadCancelled += AddEntry;
            downloader.DownloadFailed += AddEntry;

            _journal.EntryDeleted += OnEntryDeleted;
            _journal.AllEntriesDeleted += OnAllEntriesDeleted;
            _journal.ExceptionThrowned += OnExceptionThrowed;

            Reset();
        }





        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<JournalEntry> JournalEntries { get; set; }

        public CustomizableCommand SearchCommand => _searchCommand;
        public CustomizableCommand ResetCommand => _resetCommand;
        public CustomizableCommand RemoveEntryCommand => _removeEntryCommand;
        public CustomizableCommand RemoveAllEntriesCommand => _removeAllEntriesCommand;

        public IJournal Journal => _journal;
        public JournalEntry Entry { get => _entry; set => SetProperty(ref _entry, value, nameof(Entry)); }

        public string SearchLine { get => _searchLine; set => SetProperty(ref _searchLine, value, nameof(SearchLine)); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value, nameof(IsLoading)); }





        private void AddEntry(object sender, DownloaderNotificationEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _journal.CreateEntry(_dtoMapper.DownloadToEntry(e.Download));
                Reset();
            });
        }
        private void AddEntry(object sender, DownloadFailedEventArgs e)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                e.Download.Cancelling = true;
                _journal.CreateEntry(_dtoMapper.DownloadToEntry(e.Download));
                Reset();
            });
        }

        private void OnExceptionThrowed(object sender, ExceptionThrownedEventArgs e) { _notificationPanel.AddNegativeNotification(e.Exception); }
        private void OnAllEntriesDeleted(object sender, EventArgs e) { _notificationPanel.AddPositiveNotification("All entries deleted!"); }
        private void OnEntryDeleted(object sender, EventArgs e) { _notificationPanel.AddPositiveNotification("Entry deleted!"); }



        public bool CanSearch() => true;
        public void Search()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                IsLoading = true;

                Entry = null;
                JournalEntries.Clear();
                var entries = (await _journal.GetEntries()).Where(e => e.Tags.Any(t => t.Contains(_searchLine))).ToList();
                foreach (var entry in entries)
                {
                    JournalEntries.Add(_logicLayerMapper.DtoToEntry(entry));
                }

                IsLoading = false;
            });
        }

        public bool CanReset() => true;
        public void Reset()
        {
            System.Windows.Application.Current.Dispatcher.Invoke(async () =>
            {
                IsLoading = true;

                var dtos = await _journal.GetEntries();
                var viewModels = dtos.Select(dto => _logicLayerMapper.DtoToEntry(dto));
                Entry = null;
                JournalEntries.Clear();
                foreach (var vw in viewModels) JournalEntries.Add(vw);

                IsLoading = false;
            });
        }

        public bool CanRemoveEntry() => _entry != null;
        public async Task RemoveEntry()
        {
            await _journal.DeleteEntry(_logicLayerMapper.EntryToDto(Entry));
            JournalEntries.Remove(_entry);
            Entry = null;
        }

        public bool CanRemoveAllEntries() => JournalEntries.Any();
        public async Task RemoveAllEntries()
        {
            JournalEntries.Clear();
            Entry = null;
            await _journal.DeleteAllEntries();
        }



        private void SetProperty<T>(ref T oldValue, T newValue, string propertyName)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
