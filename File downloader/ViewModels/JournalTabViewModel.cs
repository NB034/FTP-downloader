using File_downloader.Command;
using File_downloader.Mappers;
using File_downloader.Resources.ResourcesAccess;
using FileDownloader.Services.Mappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace File_downloader.ViewModels
{
    internal class JournalTabViewModel : INotifyPropertyChanged
    {
        private readonly AutoEventCommandBase _searchCommand;
        private readonly AutoEventCommandBase _resetCommand;
        private readonly AutoEventCommandBase _removeEntryCommand;
        private readonly AutoEventCommandBase _removeAllEntriesCommand;
        private readonly Journal _journal;
        private readonly NotificationPanelViewModel _notificationPanel;
        private string _searchLine = "";
        private JournalEntryViewModel _entry = null;
        private bool _isLoading = false;
        private JournalEntryMapper _mapper;


        public JournalTabViewModel(Journal journal, NotificationPanelViewModel notificationPanel)
        {
            _mapper = new JournalEntryMapper();
            _journal = journal;
            _searchCommand = new AutoEventCommandBase(_ => Search(), _ => CanSearch());
            _resetCommand = new AutoEventCommandBase(_ => Reset(), _ => CanReset());
            _removeEntryCommand = new AutoEventCommandBase(_ => RemoveEntry(), _ => CanRemoveEntry());
            _removeAllEntriesCommand = new AutoEventCommandBase(_ => RemoveAllEntries(), _ => CanRemoveAllEntries());

            //JournalEntries = new ObservableCollection<JournalEntryViewModel>();

            JournalEntries = new ObservableCollection<JournalEntryViewModel>
            {
                new JournalEntryViewModel
                {
                     DownloadDate = "02.05.2023",
                      FileName = "Some game",
                       FileSize = 50000,
                        LocalPath = "Some path",
                         RemotePath = "Some server",
                          Result = IconsManager.PositiveIcon,
                           Tags = new List<string>{"game", "shooter", "old version", "favourite", "some tag idk"}
                },
                new JournalEntryViewModel
                {
                     DownloadDate = "02.05.2023",
                      FileName = "Some game",
                       FileSize = 50000,
                        LocalPath = "Some path",
                         RemotePath = "Some server",
                          Result = IconsManager.NegativeIcon,
                           Tags = new List<string>{"game", "shooter", "old version", "favourite", "some tag idk"}
                },
                new JournalEntryViewModel
                {
                     DownloadDate = "02.05.2023",
                      FileName = "Some game",
                       FileSize = 50000,
                        LocalPath = "Some path",
                         RemotePath = "Some server",
                          Result = IconsManager.PositiveIcon,
                           Tags = new List<string>{"game", "shooter", "old version", "favourite", "some tag idk"}
                }
            };

            _notificationPanel = notificationPanel;
            _journal.EntriesLoaded += OnEntriesLoaded;
            _journal.EntryCreated += OnEntryCreated;
            _journal.EntryDeleted += OnEntryDeleted;
            _journal.AllEntriesDeleted += OnAllEntriesDeleted;
            _journal.ExceptionThrowned += OnExceptionThrowed;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<JournalEntryViewModel> JournalEntries { get; set; }
        public AutoEventCommandBase SearchCommand => _searchCommand;
        public AutoEventCommandBase ResetCommand => _resetCommand;
        public AutoEventCommandBase RemoveEntryCommand => _removeEntryCommand;
        public AutoEventCommandBase RemoveAllEntriesCommand => _removeAllEntriesCommand;
        public Journal Journal => _journal;
        public string SearchLine { get => _searchLine; set => SetProperty(ref _searchLine, value, nameof(SearchLine)); }
        public JournalEntryViewModel Entry { get => _entry; set => SetProperty(ref _entry, value, nameof(Entry)); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value, nameof(IsLoading)); }





        private void OnExceptionThrowed(Exception obj) { _notificationPanel.AddNegativeNotification(obj); }
        private void OnAllEntriesDeleted() { _notificationPanel.AddPositiveNotification("All entries deleted!"); }
        private void OnEntryDeleted() { _notificationPanel.AddPositiveNotification("Entry deleted!"); }
        private void OnEntryCreated() { _notificationPanel.AddPositiveNotification("Download complete!"); }
        private void OnEntriesLoaded() { _notificationPanel.AddPositiveNotification("Entries loaded!"); }

        public void Search()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                Entry = null;
                JournalEntries.Clear();
                var entries = (await _journal.GetEntries()).Where(e => e.Tags.Any(t => t.Contains(_searchLine))).ToList();
                foreach (var entry in entries)
                {
                    JournalEntries.Add(_mapper.ModelToVm(entry));
                }
            });
        }

        public bool CanSearch() => true;

        public void Reset()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                Entry = null;
                JournalEntries.Clear();
                foreach (var entry in await _journal.GetEntries())
                {
                    JournalEntries.Add(_mapper.ModelToVm(entry));
                }
            });
        }

        public bool CanReset() => true;

        public async void RemoveEntry()
        {
            JournalEntries.Remove(_entry);
            Entry = null;
            await _journal.DeleteEntry(_mapper.VmToModel(Entry));
        }

        public bool CanRemoveEntry() => _entry != null;

        public async void RemoveAllEntries()
        {
            JournalEntries.Clear();
            Entry = null;
            await _journal.DeleteAllEntries();
        }

        public bool CanRemoveAllEntries() => JournalEntries.Any();

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
