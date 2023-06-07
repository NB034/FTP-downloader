using FtpDownloader.Command;
using FtpDownloader.Mappers;
using FtpDownloader.ViewModels.DataViewModels;
using FtpDownloader.Services.Mappers;
using FtpDownloader.Services.Models.DownloaderModels;
using FtpDownloader.Services.Models.JournalModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace FtpDownloader.ViewModels
{
    internal class JournalTab_VM : INotifyPropertyChanged
    {
        private readonly AutoEventCommandBase _searchCommand;
        private readonly AutoEventCommandBase _resetCommand;
        private readonly AutoEventCommandBase _removeEntryCommand;
        private readonly AutoEventCommandBase _removeAllEntriesCommand;

        private readonly IJournal _journal;
        private readonly NotificationPanel_VM _notificationPanel;
        private JournalEntry_VM _entry = null;
        private JournalEntryMapper _entriesMapper;
        private DownloadToEntryMapper _downloadToEntryMapper;

        private string _searchLine = "";
        private bool _isLoading = false;


        public JournalTab_VM(IJournal journal, IDownloader downloader, NotificationPanel_VM notificationPanel)
        {
            _entriesMapper = new JournalEntryMapper();
            _downloadToEntryMapper = new DownloadToEntryMapper();
            _journal = journal;
            _notificationPanel = notificationPanel;

            _searchCommand = new AutoEventCommandBase(_ => Search(), _ => CanSearch());
            _resetCommand = new AutoEventCommandBase(_ => Reset(), _ => CanReset());
            _removeEntryCommand = new AutoEventCommandBase(_ => RemoveEntry(), _ => CanRemoveEntry());
            _removeAllEntriesCommand = new AutoEventCommandBase(_ => RemoveAllEntries(), _ => CanRemoveAllEntries());

            JournalEntries = new ObservableCollection<JournalEntry_VM>();

            downloader.DownloadCompleted += AddEntry;
            downloader.DownloadCancelled += AddEntry;
            downloader.DownloadFailed += AddEntry;

            _journal.EntriesLoaded += OnEntriesLoaded;
            _journal.EntryDeleted += OnEntryDeleted;
            _journal.AllEntriesDeleted += OnAllEntriesDeleted;
            _journal.ExceptionThrowned += OnExceptionThrowed;

            Reset();
        }





        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<JournalEntry_VM> JournalEntries { get; set; }

        public AutoEventCommandBase SearchCommand => _searchCommand;
        public AutoEventCommandBase ResetCommand => _resetCommand;
        public AutoEventCommandBase RemoveEntryCommand => _removeEntryCommand;
        public AutoEventCommandBase RemoveAllEntriesCommand => _removeAllEntriesCommand;

        public IJournal Journal => _journal;
        public JournalEntry_VM Entry { get => _entry; set => SetProperty(ref _entry, value, nameof(Entry)); }

        public string SearchLine { get => _searchLine; set => SetProperty(ref _searchLine, value, nameof(SearchLine)); }
        public bool IsLoading { get => _isLoading; set => SetProperty(ref _isLoading, value, nameof(IsLoading)); }





        private void AddEntry(DownloadModel obj)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _journal.CreateEntry(_downloadToEntryMapper.DownloadToEntry(obj));
                Reset();
            });
        }
        private void AddEntry(DownloadModel obj, Exception ex)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                obj.Cancelling = true;
                _journal.CreateEntry(_downloadToEntryMapper.DownloadToEntry(obj));
                Reset();
            });
        }

        private void OnExceptionThrowed(Exception obj) { _notificationPanel.AddNegativeNotification(obj); }
        private void OnAllEntriesDeleted() { _notificationPanel.AddPositiveNotification("All entries deleted!"); }
        private void OnEntryDeleted() { _notificationPanel.AddPositiveNotification("Entry deleted!"); }
        private void OnEntriesLoaded() { _notificationPanel.AddPositiveNotification("Entries loaded!"); }



        public bool CanSearch() => true;
        public void Search()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                IsLoading = true;
                Entry = null;
                JournalEntries.Clear();
                var entries = (await _journal.GetEntries()).Where(e => e.Tags.Any(t => t.Contains(_searchLine))).ToList();
                foreach (var entry in entries)
                {
                    JournalEntries.Add(_entriesMapper.ModelToVm(entry));
                }
            });
        }

        public bool CanReset() => true;
        public void Reset()
        {
            Application.Current.Dispatcher.Invoke(async () =>
            {
                IsLoading = true;

                var models = await _journal.GetEntries();
                var viewModels = models.Select(m => _entriesMapper.ModelToVm(m));
                Entry = null;
                JournalEntries.Clear();
                foreach(var vw in viewModels) JournalEntries.Add(vw);

                IsLoading = false;
            });
        }

        public bool CanRemoveEntry() => _entry != null;
        public async void RemoveEntry()
        {
            JournalEntries.Remove(_entry);
            Entry = null;
            await _journal.DeleteEntry(_entriesMapper.VmToModel(Entry));
        }

        public bool CanRemoveAllEntries() => JournalEntries.Any();
        public async void RemoveAllEntries()
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
