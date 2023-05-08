using File_downloader.Command;
using File_downloader.Resources.ResourcesAccess;
using FileDownloader.Services.Models.DownloaderModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Media;

namespace File_downloader.ViewModels
{
    internal class DownloadTab_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public DownloadTab_VM(NotificationPanel_VM notificationPanel, DownloadList_VM downloadList)
        {
            //Tags = new ObservableCollection<string>();

            Tags = new ObservableCollection<string>()
            {
                "game",
                "office",
                "music",
                "picture",
                "archive"
            };

            _notificationPanel = notificationPanel;
            _downloadList = downloadList;
        }

        public int MaxTags => 5;
        public int MaxTagLength => 8;
        public int MaxDownloads => 10;
        public int TagTextBoxWidth => MaxTagLength * 12;

        // Properties
        private IDownloader _downloader;
        private DownloadList_VM _downloadList;
        private NotificationPanel_VM _notificationPanel;

        private bool _useCredentials = false;
        private bool _startImmediately = true;
        private bool _tagsLimitReached = false;
        private bool _downloadsLimitReached = false;
        private bool _resourceVerified = false;
        private bool _directoryVerified = false;
        private bool _fileNameVerified = false;

        private string _userName = String.Empty;
        private string _password = String.Empty;
        private string _resourceUrl = String.Empty;
        private string _localDirectory = String.Empty;
        private string _fileName = String.Empty;
        private string _fileExtension = String.Empty;

        private ImageBrush _resourceUrlStatus = IconsManager.NeutralIcon;
        private ImageBrush _localDirectoryStatus = IconsManager.NeutralIcon;
        private ImageBrush _fileNameStatus = IconsManager.NeutralIcon;



        public IDownloader Downloader => _downloader;
        public DownloadList_VM DownloadList => _downloadList;
        public NotificationPanel_VM NotificationPanel => _notificationPanel;

        public bool UseCrdentials { get => _useCredentials; set => SetProperty(ref _useCredentials, value, nameof(UseCrdentials)); }
        public bool StartImmediately { get => _startImmediately; set => SetProperty(ref _startImmediately, value, nameof(StartImmediately)); }
        public bool TagsLimitReached { get => _tagsLimitReached; set => SetProperty(ref _tagsLimitReached, value, nameof(TagsLimitReached)); }
        public bool DownloadsLimitReached { get => _downloadsLimitReached; set => SetProperty(ref _downloadsLimitReached, value, nameof(DownloadsLimitReached)); }
        public bool ResourceVerified { get => _resourceVerified; set => SetProperty(ref _resourceVerified, value, nameof(ResourceVerified)); }
        public bool DirectoryVerified { get => _directoryVerified; set => SetProperty(ref _directoryVerified, value, nameof(DirectoryVerified)); }
        public bool FileNameVerified { get => _fileNameVerified; set => SetProperty(ref _fileNameVerified, value, nameof(FileNameVerified)); }

        public string UserName { get => _userName; set => SetProperty(ref _userName, value, nameof(UserName)); }
        public string Password { get => _password; set => SetProperty(ref _password, value, nameof(Password)); }
        public string ResourceUrl { get => _resourceUrl; set => SetProperty(ref _resourceUrl, value, nameof(ResourceUrl)); }
        public string LocalDirectory { get => _localDirectory; set => SetProperty(ref _localDirectory, value, nameof(LocalDirectory)); }
        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value, nameof(FileName)); }
        public string FileExtension { get => _fileExtension; set => SetProperty(ref _fileExtension, value, nameof(FileExtension)); }

        public ImageBrush ResourceUrlStatus { get => _resourceUrlStatus; set => SetProperty(ref _resourceUrlStatus, value, nameof(ResourceUrlStatus)); }
        public ImageBrush LocalDirectoryStatus { get => _localDirectoryStatus; set => SetProperty(ref _localDirectoryStatus, value, nameof(LocalDirectoryStatus)); }
        public ImageBrush FileNameStatus { get => _fileNameStatus; set => SetProperty(ref _fileNameStatus, value, nameof(FileNameStatus)); }

        // Collections
        public ObservableCollection<string> Tags { get; set; }

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

        }

        private bool CanAddTag() => Tags.Count < MaxTags;
        private void AddTag()
        {

        }

        private bool CanRemoveTag() => Tags.Count > 0;
        private void RemoveTag()
        {

        }

        //private bool CanStartDownload() =>
        private void StartDownload()
        {

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
