using FtpDownloader.Services.Interfaces.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.UI.DataSources.DataTypes;
using FtpDownloader.UI.DataSources.Command;
using FtpDownloader.UI.DataSources.Accessories;

namespace FtpDownloader.UI.DataSources.ViewModels
{
    public class DownloadTab_VM : INotifyPropertyChanged
    {
        public DownloadTab_VM(NotificationPanel_VM notificationPanel, DownloadList_VM downloadList, IInfoCollector infoCollector)
        {
            _notificationPanel = notificationPanel;
            _downloadList = downloadList;
            _infoCollector = infoCollector;

            Tags = new ObservableCollection<string>();

            _addTagCommand = new AutoEventCommandBase(o => AddTag(o), _ => CanAddTag());
            _removeTagCommand = new AutoEventCommandBase(o => RemoveTag(o), _ => CanRemoveTag());
            _searchRemoteFileCommand = new AutoEventCommandBase(_ => SearchRemoteFile(), _ => CanSearchRemoteFile());
            _pickDirectoryCommand = new AutoEventCommandBase(_ => PickDirectory(), _ => CanPickDirectory());
            _startDownloadCommand = new AutoEventCommandBase(_ => StartDownload(), _ => CanStartDownload());

            _infoCollector.SearchFinished += OnSearchFinished;
            _downloadList.Downloads.CollectionChanged += OnDownloadsCollectionChanged;
            Tags.CollectionChanged += OnTagsCollectionChanged;
            PropertyChanged += OnUsernameChanged;
            PropertyChanged += OnPasswordChanged;
            PropertyChanged += OnFileNameChanged;
            PropertyChanged += OnResourceUriChanged;
        }

        private void SetProperty<T>(ref T oldValue, T newValue, string propertyName)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        // On events

        private void OnDownloadsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_downloadList.Downloads.Count >= MaxDownloads || _downloadList.Downloads.Count == MaxDownloads - 1)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DownloadsLimitReached)));
            }
        }

        private void OnResourceUriChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ResourceUrl))
            {
                ResourceCheckmark.Reset();
                FileExtension = String.Empty;
                _infoModel = null;
            }
        }

        private void OnFileNameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FileName))
            {
                if (_fileName.Length == 0 || _fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) FileNameCheckmark.Reject();
                if (_fileName.Length == 1) FileNameCheckmark.Verify();
            }
        }

        private void OnPasswordChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_useCredentials && e.PropertyName == nameof(Password))
            {
                if (_password.Length == 0 || _userName.Length == 0) CredentialsCheckmark.Reject();
                if (_password.Length == 1 && _userName.Length != 0) CredentialsCheckmark.Verify();
            }
        }

        private void OnUsernameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_useCredentials && e.PropertyName == nameof(UserName))
            {
                if (_userName.Length == 0 || _password.Length == 0) CredentialsCheckmark.Reject();
                if (_userName.Length == 1 && _password.Length != 0) CredentialsCheckmark.Verify();
            }
        }

        private void OnSearchFinished(LogicLayerInfoDto obj)
        {
            if (obj.IsExist)
            {
                ResourceCheckmark.Verify();
                FileName = Path.GetFileName(_resourceUrl);
                if (obj.Exstention == String.Empty) FileExtension = "dir";
                else FileExtension = obj.Exstention;
                _infoModel = obj;

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                _notificationPanel.AddPositiveNotification("Resource was found!"));
            }
            else
            {
                ResourceCheckmark.Reject();
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                _notificationPanel.AddNotification(NotificationTypesEnum.Negative, "Resource wasn't found!"));
            }
        }

        private void OnTagsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && Tags.Count >= MaxTags
                || e.Action == NotifyCollectionChangedAction.Remove && Tags.Count == MaxTags - 1)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(TagsLimitReached)));
            }
        }



        // Fields & properties

        private LogicLayerInfoDto _infoModel = null;
        public event PropertyChangedEventHandler PropertyChanged;
        public ObservableCollection<string> Tags { get; set; }
        public int MaxTags => 5;
        public int MaxTagLength => 8;
        public int MaxDownloads => 10;
        public int TagTextBoxWidth => MaxTagLength * 12;
        public bool TagsLimitReached => Tags.Count >= MaxTags;
        public bool DownloadsLimitReached => _downloadList.Downloads.Count >= MaxDownloads;





        public DownloadList_VM DownloadList => _downloadList;
        private readonly DownloadList_VM _downloadList;

        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        private readonly NotificationPanel_VM _notificationPanel;

        public IInfoCollector InfoCollector => _infoCollector;
        private readonly IInfoCollector _infoCollector;

        public bool StartImmediately { get => _startImmediately; set => SetProperty(ref _startImmediately, value, nameof(StartImmediately)); }
        private bool _startImmediately = true;





        public bool UseCrdentials { get => _useCredentials; set => SetProperty(ref _useCredentials, value, nameof(UseCrdentials)); }
        private bool _useCredentials = false;

        public string UserName { get => _userName; set => SetProperty(ref _userName, value, nameof(UserName)); }
        private string _userName = String.Empty;

        public string Password { get => _password; set => SetProperty(ref _password, value, nameof(Password)); }
        private string _password = String.Empty;

        public Checkmark_VM CredentialsCheckmark { get; private set; } = new Checkmark_VM();





        public string ResourceUrl { get => _resourceUrl; set => SetProperty(ref _resourceUrl, value, nameof(ResourceUrl)); }
        private string _resourceUrl = String.Empty;

        public Checkmark_VM ResourceCheckmark { get; private set; } = new Checkmark_VM();





        public string LocalDirectory { get => _localDirectory; set => SetProperty(ref _localDirectory, value, nameof(LocalDirectory)); }
        private string _localDirectory = String.Empty;

        public Checkmark_VM LocalDirectoryCheckmark { get; private set; } = new Checkmark_VM();





        public string FileName { get => _fileName; set => SetProperty(ref _fileName, value, nameof(FileName)); }
        private string _fileName = String.Empty;

        public string FileExtension { get => _fileExtension; set => SetProperty(ref _fileExtension, value, nameof(FileExtension)); }
        private string _fileExtension = String.Empty;

        public Checkmark_VM FileNameCheckmark { get; private set; } = new Checkmark_VM();




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



        // Command methods

        private bool CanSearchRemoteFile() => _resourceUrl != String.Empty && (!_useCredentials || CredentialsCheckmark.IsVerified);
        private void SearchRemoteFile()
        {
            ResourceCheckmark.Reset();
            _notificationPanel.AddNeutralNotification("Searching...");

            if (_useCredentials) _infoCollector.BeginSearch(_resourceUrl, _userName, _password);
            else _infoCollector.BeginSearch(_resourceUrl);
        }



        private bool CanPickDirectory() => true;
        private void PickDirectory()
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LocalDirectory = dialog.SelectedPath;
                LocalDirectoryCheckmark.Verify();
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



        private bool CanStartDownload() =>
            FileNameCheckmark.IsVerified
            && LocalDirectoryCheckmark.IsVerified
            && ResourceCheckmark.IsVerified
            && (!UseCrdentials || CredentialsCheckmark.IsVerified)
            && !DownloadsLimitReached;

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
                Size = _infoModel.SizeInBytes,
                To = _localDirectory,
                UseCredentials = _useCredentials,
                Tags = Tags.ToList()
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
