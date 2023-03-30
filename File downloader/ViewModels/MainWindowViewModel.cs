using File_downloader.Command;
using File_downloader.ResourceAccess;
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
            InitializeNotificationPanel();
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

        }

        public static int MaxTags => 5;
        public static int MaxTagLength => 5;

        // Properties
        private bool _useCredentials = false;
        private bool _resourceVerified = false;

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

        public bool ResourceVerified
        {
            get => _resourceVerified;
            set => SetProperty(ref _resourceVerified, value, nameof(ResourceVerified));
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

        private bool CanStartDownload() =>

        private void Resume()
        {

        }

        private bool CanResume() =>

        private void Pause()
        {

        }

        private bool CanPause() =>

        private void Cancel()
        {

        }

        private bool CanCancel() =>

        private void ResumeAll()
        {

        }

        private bool CanResumeAll() =>

        private void PauseAll()
        {

        }

        private bool CanPauseAll() =>

        private void CancelAll()
        {

        }

        private bool CanCancelAll() =>
    }

    // View tab
    partial class MainWindowViewModel
    {
        public void InitializeViewTab()
        {

        }


    }

    // Notification panel
    partial class MainWindowViewModel
    {
        private void InitializeNotificationPanel()
        {
            Notifications = new ObservableCollection<NotificationViewModel>();
            _closeNotificatonCommand = new AutoEventCommandBase(o => RemoveNotification(o), _ => true);
            _closeAllNotificatonsCommand = new AutoEventCommandBase(_ => ClearNotifications(), _ => true);
        }

        private AutoEventCommandBase _closeNotificatonCommand;
        private AutoEventCommandBase _closeAllNotificatonsCommand;

        public AutoEventCommandBase CloseNotificatonCommand => _closeNotificatonCommand;
        public AutoEventCommandBase CloseAllNotificationsCommand => _closeAllNotificatonsCommand;
        public ObservableCollection<NotificationViewModel> Notifications { get; set; }

        private void RemoveNotification(object o)
        {
            var notification = (NotificationViewModel)o;
            Notifications.Remove(notification);
        }

        private void ClearNotifications()
        {
            Notifications.Clear();
        }

        public void AddPositiveNotification(string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                ImageUri = NotificationIconsManager.PositiveIconUri,
                Message = message,
            });
        }

        public void AddNeutralNotification(string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                ImageUri = NotificationIconsManager.NeutralIconUri,
                Message = message,
            });
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new NotificationViewModel
            {
                ImageUri = NotificationIconsManager.NegativeIconUri,
                Message = exception.Message,
            });
        }
    }
}
