using File_downloader.Command;
using File_downloader.Resources.ResourcesAccess;
using File_downloader.ViewModels.DataViewModels;
using FileDownloader.Services.Models.InfoCollectorModels;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Media;

namespace File_downloader.ViewModels
{
    internal class DownloadTab_VM : INotifyPropertyChanged
    {
        public DownloadTab_VM(NotificationPanel_VM notificationPanel, DownloadList_VM downloadList, IInfoCollector infoCollector)
        {
            _notificationPanel = notificationPanel;
            _downloadList = downloadList;
            _infoCollector = infoCollector;

            Tags = new ObservableCollection<string>();
            Tags.CollectionChanged += OnTagsCollectionChanged;

            _addTagCommand = new AutoEventCommandBase(o => AddTag(o), _ => CanAddTag());
            _removeTagCommand = new AutoEventCommandBase(o => RemoveTag(o), _ => CanRemoveTag());
            _searchRemoteFileCommand = new AutoEventCommandBase(_ => SearchRemoteFile(), _ => CanSearchRemoteFile());
            _pickDirectoryCommand = new AutoEventCommandBase(_ => PickDirectory(), _ => CanPickDirectory());
            _startDownloadCommand = new AutoEventCommandBase(_ => StartDownload(), _ => CanStartDownload());
        }

        private void OnTagsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && Tags.Count >= MaxTags
                || e.Action == NotifyCollectionChangedAction.Remove && Tags.Count == MaxTags - 1)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagsLimitReached)));
            }
        }

        private void SetProperty<T>(ref T oldValue, T newValue, string propertyName)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        // Properties
        private readonly DownloadList_VM _downloadList;
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly IInfoCollector _infoCollector;

        private bool _useCredentials = false;
        private bool _startImmediately = true;

        private string _userName = String.Empty;
        private string _password = String.Empty;
        private string _resourceUrl = String.Empty;
        private string _localDirectory = String.Empty;
        private string _fileName = String.Empty;
        private string _fileExtension = String.Empty;

        private ImageBrush _credentialsStatus = IconsManager.NeutralIcon;
        private ImageBrush _resourceUrlStatus = IconsManager.NeutralIcon;
        private ImageBrush _localDirectoryStatus = IconsManager.NeutralIcon;
        private ImageBrush _fileNameStatus = IconsManager.NeutralIcon;



        public DownloadList_VM DownloadList => _downloadList;
        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public IInfoCollector InfoCollector => _infoCollector;

        public ObservableCollection<string> Tags { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public int MaxTags => 5;
        public int MaxTagLength => 8;
        public int MaxDownloads => 10;
        public int TagTextBoxWidth => MaxTagLength * 12;
        public bool TagsLimitReached => Tags.Count >= MaxTags;
        public bool DownloadsLimitReached => _downloadList.Downloads.Count <= MaxDownloads;


        public bool UseCrdentials { get => _useCredentials; set => SetProperty(ref _useCredentials, value, nameof(UseCrdentials)); }
        public bool StartImmediately { get => _startImmediately; set => SetProperty(ref _startImmediately, value, nameof(StartImmediately)); }

        public string UserName { get => _userName; set => SetProperty(ref _userName, value, nameof(UserName)); }
        public string Password { get => _password; set => SetProperty(ref _password, value, nameof(Password)); }
        public string ResourceUrl { get => _resourceUrl; set => SetProperty(ref _resourceUrl, value, nameof(ResourceUrl)); }
        public string LocalDirectory { get => _localDirectory; set => SetProperty(ref _localDirectory, value, nameof(LocalDirectory)); }
        public string FileExtension { get => _fileExtension; set => SetProperty(ref _fileExtension, value, nameof(FileExtension)); }
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value, nameof(FileName)); }

        public ImageBrush CredentialsStatus { get => _credentialsStatus; set => SetProperty(ref _credentialsStatus, value, nameof(CredentialsStatus)); }
        public ImageBrush ResourceUrlStatus { get => _resourceUrlStatus; set => SetProperty(ref _resourceUrlStatus, value, nameof(ResourceUrlStatus)); }
        public ImageBrush LocalDirectoryStatus { get => _localDirectoryStatus; set => SetProperty(ref _localDirectoryStatus, value, nameof(LocalDirectoryStatus)); }
        public ImageBrush FileNameStatus { get => _fileNameStatus; set => SetProperty(ref _fileNameStatus, value, nameof(FileNameStatus)); }




        // Commands
        private readonly AutoEventCommandBase _searchRemoteFileCommand;
        private readonly AutoEventCommandBase _pickDirectoryCommand;
        private readonly AutoEventCommandBase _addTagCommand;
        private readonly AutoEventCommandBase _removeTagCommand;
        private readonly AutoEventCommandBase _startDownloadCommand;

        public AutoEventCommandBase SearchRemoteFileCommand => _searchRemoteFileCommand;
        public AutoEventCommandBase PickDirectoryCommand => _pickDirectoryCommand;
        public AutoEventCommandBase AddTagCommand => _addTagCommand;
        public AutoEventCommandBase RemoveTagCommand => _removeTagCommand;
        public AutoEventCommandBase StartDownloadCommand => _startDownloadCommand;



        private bool CanSearchRemoteFile() => _resourceUrl != String.Empty;
        private void SearchRemoteFile()
        {

        }

        private bool CanPickDirectory() => true;
        private void PickDirectory()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LocalDirectory = dialog.SelectedPath;
                LocalDirectoryStatus = IconsManager.PositiveIcon;
            }
        }

        private bool CanAddTag() => Tags.Count < MaxTags;
        private void AddTag(object o)
        {
            var tag = (string)o;
            Tags.Add(tag);
        }

        private bool CanRemoveTag() => Tags.Count > 0;
        private void RemoveTag(object o)
        {
            var tag = (string)o;
            Tags.Remove(tag);
        }

        private bool CanStartDownload() => true;
        private void StartDownload()
        {
            var download = new Download_VM
            {
                Name = _fileName,
                Cancelling = false,
                DownloadedBytes = 0,
                DownloadGuid = Guid.NewGuid(),
                From = _resourceUrl,
                OnPause = !_startImmediately,
                Size = 0,
                To = _localDirectory,
                UseCredentials = _useCredentials
            };

            if (_useCredentials)
            {
                download.Username = _userName;
                download.Password = _password;
            }

            _downloadList.StartNewDownload(download);
        }
    }
}
