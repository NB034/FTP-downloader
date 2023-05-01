using File_downloader.Command;
using File_downloader.Resources.ResourcesAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    // Common
    partial class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindowViewModel()
        {
            InitializeDownloadTab();
            InitializeViewTab();
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



    // Download tab
    partial class MainWindowViewModel
    {
        public void InitializeDownloadTab()
        {
            Tags = new ObservableCollection<string>
            {
                "game", "office", "music", "picture", "book"
            };
            Downloads = new ObservableCollection<DownloadViewModel>();
        }

        public int MaxTags => 5;
        public int MaxTagLength => 8;
        public int MaxDownloads => 10;
        public int TagTextBoxWidth => MaxTagLength * 12;

        // Properties
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

        public bool UseCrdentials
        {
            get => _useCredentials;
            set => SetProperty(ref _useCredentials, value, nameof(UseCrdentials));
        }

        public bool StartImmediately
        {
            get => _startImmediately;
            set => SetProperty(ref _startImmediately, value, nameof(StartImmediately));
        }

        public bool TagsLimitReached
        {
            get => _tagsLimitReached;
            set => SetProperty(ref _tagsLimitReached, value, nameof(TagsLimitReached));
        }

        public bool DownloadsLimitReached
        {
            get => _downloadsLimitReached;
            set => SetProperty(ref _downloadsLimitReached, value, nameof(DownloadsLimitReached));
        }

        public bool ResourceVerified
        {
            get => _resourceVerified;
            set => SetProperty(ref _resourceVerified, value, nameof(ResourceVerified));
        }

        public bool DirectoryVerified
        {
            get => _directoryVerified;
            set => SetProperty(ref _directoryVerified, value, nameof(DirectoryVerified));
        }

        public bool FileNameVerified
        {
            get => _fileNameVerified;
            set => SetProperty(ref _fileNameVerified, value, nameof(FileNameVerified));
        }

        public string UserName
        {
            get => _userName;
            set => SetProperty(ref _userName, value, nameof(UserName));
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public string ResourceUrl
        {
            get => _resourceUrl;
            set => SetProperty(ref _resourceUrl, value, nameof(ResourceUrl));
        }

        public string LocalDirectory
        {
            get => _localDirectory;
            set => SetProperty(ref _localDirectory, value, nameof(LocalDirectory));
        }

        public string FileName
        {
            get => _fileName;
            set => SetProperty(ref _fileName, value, nameof(FileName));
        }

        public string FileExtension
        {
            get => _fileExtension;
            set => SetProperty(ref _fileExtension, value, nameof(FileExtension));
        }

        // Collections
        public ObservableCollection<string> Tags { get; set; }
        public ObservableCollection<DownloadViewModel> Downloads { get; set; }

        // Commands
        private AutoEventCommandBase _searchRemoteFileCommand;
        private AutoEventCommandBase _pickDirectoryCommand;
        private AutoEventCommandBase _addTagCommand;
        private AutoEventCommandBase _removeTagCommand;
        private AutoEventCommandBase _startDownloadCommand;
        private AutoEventCommandBase _resumeCommand;
        private AutoEventCommandBase _pauseCommand;
        private AutoEventCommandBase _cancelCommand;
        private AutoEventCommandBase _resumeAllCommand;
        private AutoEventCommandBase _pauseAllCommand;
        private AutoEventCommandBase _cancelAllCommand;

        public AutoEventCommandBase SearchRemoteFileCommand => _searchRemoteFileCommand;
        public AutoEventCommandBase PickDirectoryCommand => _pickDirectoryCommand;
        public AutoEventCommandBase AddTagCommand => _addTagCommand;
        public AutoEventCommandBase RemoveTagCommand => _removeTagCommand;
        public AutoEventCommandBase StartDownloadCommand => _startDownloadCommand;
        public AutoEventCommandBase ResumeCommand => _resumeCommand;
        public AutoEventCommandBase PauseCommand => _pauseCommand;
        public AutoEventCommandBase CancelCommand => _cancelCommand;
        public AutoEventCommandBase ResumeAllCommand => _resumeAllCommand;
        public AutoEventCommandBase PauseAllCommand => _pauseAllCommand;
        public AutoEventCommandBase CancelAllCommand => _cancelAllCommand;

        private void SearchRemoteFile()
        {

        }

        private bool CanSearchRemoteFile() => _resourceUrl != String.Empty;

        private void PickDirectory()
        {

        }

        private bool CanPickDirectory() => true;

        private void AddTag()
        {

        }

        private bool CanAddTag() => Tags.Count < MaxTags;

        private void RemoveTag()
        {

        }

        private bool CanRemoveTag() => Tags.Count > 0;

        private void StartDownload()
        {

        }

        //private bool CanStartDownload() =>

        private void Resume()
        {

        }

        //private bool CanResume() =>

        private void Pause()
        {

        }

        //private bool CanPause() =>

        private void Cancel()
        {

        }

        //private bool CanCancel() =>

        private void ResumeAll()
        {

        }

        //private bool CanResumeAll() =>

        private void PauseAll()
        {

        }

        //private bool CanPauseAll() =>

        private void CancelAll()
        {

        }

        //private bool CanCancelAll() =>
    }
}
