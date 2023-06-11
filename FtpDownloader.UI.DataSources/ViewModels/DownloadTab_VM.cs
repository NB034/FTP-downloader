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

            _addTagCommand = new CustomizableCommand(o => AddTag(o), _ => CanAddTag());
            _removeTagCommand = new CustomizableCommand(o => RemoveTag(o), _ => CanRemoveTag());
            _searchRemoteFileCommand = new CustomizableCommand(_ => SearchRemoteFile(), _ => CanSearchRemoteFile());
            _pickDirectoryCommand = new CustomizableCommand(_ => PickDirectory(), _ => CanPickDirectory());
            _startDownloadCommand = new CustomizableCommand(_ => StartDownload(), _ => CanStartDownload());

            _infoCollector.SearchFinished += OnSearchFinished;
            _infoCollector.SearchFailed += OnSearchFailed;
            _downloadList.Downloads.CollectionChanged += OnDownloadsCollectionChanged;
            Tags.CollectionChanged += OnTagsCollectionChanged;

            PropertyChanged += OnFileNameChanged;
            PropertyChanged += OnResourceUriChanged;
            PropertyChanged += OnCredentialsChanged;
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
            if (e.PropertyName == nameof(Host) || e.PropertyName == nameof(FilePath))
            {
                ResourceCheckmark.Reset();
                FileNameCheckmark.Reset();
                FileExtension = String.Empty;
                _infoModel = null;
            }
        }

        private void OnFileNameChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FileName))
            {
                CheckFileName();
            }
        }

        private void CheckFileName()
        {
            if (_fileName.Length == 0 || _fileName.IndexOfAny(Path.GetInvalidFileNameChars()) != -1) FileNameCheckmark.Reject();
            else FileNameCheckmark.Verify();
        }

        private void OnCredentialsChanged(object sender, PropertyChangedEventArgs e)
        {
            if (_useCredentials && (e.PropertyName == nameof(UserName) || e.PropertyName == nameof(Password)))
            {
                if (_userName.Length == 0) CredentialsCheckmark.Reject();
                else CredentialsCheckmark.Verify();
            }
        }

        private void OnSearchFinished(LogicLayerInfoDto obj)
        {
            if (obj.IsExist)
            {
                ResourceCheckmark.Verify();
                FileName = Path.GetFileNameWithoutExtension(_filePath);
                FileExtension = obj.Exstention;
                CheckFileName();
                _infoModel = obj;

                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                {
                    _notificationPanel.AddPositiveNotification("Resource was found!");
                    CustomizableCommand.RaiseCanExecuteChanged();
                });
            }
            else
            {
                ResourceCheckmark.Reject();
                System.Windows.Application.Current.Dispatcher.Invoke(() =>
                _notificationPanel.AddNotification(NotificationTypesEnum.Negative, "Resource wasn't found!"));
            }
        }

        private void OnSearchFailed(Exception obj)
        {
            ResourceCheckmark.Reject();
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            _notificationPanel.AddNotification(NotificationTypesEnum.Negative, obj.Message));
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
        private bool _useCredentials = true;

        private string _userName = String.Empty;
        public string UserName { get => _userName; set { SetProperty(ref _userName, value, nameof(UserName)); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUserNameEmpty))); } }
        public bool IsUserNameEmpty => _userName == String.Empty;

        private string _password = String.Empty;
        public string Password { get => _password; set { SetProperty(ref _password, value, nameof(Password)); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsPasswordEmpty))); } }
        public bool IsPasswordEmpty => _password == String.Empty;

        public Checkmark_VM CredentialsCheckmark { get; private set; } = new Checkmark_VM();





        private string _host = String.Empty;
        public string Host { get => _host; set { SetProperty(ref _host, value, nameof(Host)); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsHostEmpty))); } }
        public bool IsHostEmpty => _host == String.Empty;

        private string _filePath = String.Empty;
        public string FilePath { get => _filePath; set { SetProperty(ref _filePath, value, nameof(FilePath)); PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsFilePathEmpty))); } }
        public bool IsFilePathEmpty => _filePath == String.Empty;

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

        private readonly CustomizableCommand _searchRemoteFileCommand;
        private readonly CustomizableCommand _pickDirectoryCommand;
        private readonly CustomizableCommand _addTagCommand;
        private readonly CustomizableCommand _removeTagCommand;
        private readonly CustomizableCommand _startDownloadCommand;

        public CustomizableCommand SearchRemoteFileCommand => _searchRemoteFileCommand;
        public CustomizableCommand PickDirectoryCommand => _pickDirectoryCommand;
        public CustomizableCommand AddTagCommand => _addTagCommand;
        public CustomizableCommand RemoveTagCommand => _removeTagCommand;
        public CustomizableCommand StartDownloadCommand => _startDownloadCommand;



        // Command methods

        private bool CanSearchRemoteFile() => _host != String.Empty && (!_useCredentials || CredentialsCheckmark.IsVerified);
        private void SearchRemoteFile()
        {
            ResourceCheckmark.Reset();
            _notificationPanel.AddNeutralNotification("Searching...");

            if (_useCredentials) _infoCollector.BeginSearch("ftp://" + _host, _filePath, _userName, _password);
            else _infoCollector.BeginSearch("ftp://" + _host, _filePath);
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
            && !DownloadsLimitReached;

        private void StartDownload()
        {
            var download = new Download_VM
            {
                Name = _fileName + _fileExtension,
                Cancelling = false,
                DownloadedBytes = 0,
                DownloadGuid = Guid.NewGuid(),
                Host = "ftp://" + _host,
                Path = _filePath,
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
