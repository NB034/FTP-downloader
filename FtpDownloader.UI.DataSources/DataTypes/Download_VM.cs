using System.ComponentModel;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class Download_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name = "";
        private string _from = "";
        private string _to = "";
        private double _size = 0;
        private double _downloadedMegaBytes = 0;
        private bool _onPause = false;
        private bool _cancelling = false;
        private bool _useCreadentials = false;
        private string _username = "";
        private string _password = "";

        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value, nameof(Name));
        }

        public string From
        {
            get => _from;
            set => SetProperty(ref _from, value, nameof(From));
        }

        public string To
        {
            get => _to;
            set => SetProperty(ref _to, value, nameof(To));
        }

        public double Size
        {
            get => _size;
            set => SetProperty(ref _size, value, nameof(Size));
        }

        public double DownloadedBytes
        {
            get => _downloadedMegaBytes;
            set
            {
                _downloadedMegaBytes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Percent)));
            }
        }

        public bool OnPause
        {
            get => _onPause;
            set => SetProperty(ref _onPause, value, nameof(OnPause));
        }

        public bool Cancelling
        {
            get => _cancelling;
            set => SetProperty(ref _cancelling, value, nameof(Cancelling));
        }

        public bool UseCredentials
        {
            get => _useCreadentials;
            set => SetProperty(ref _useCreadentials, value, nameof(UseCredentials));
        }

        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, nameof(Username));
        }

        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public Guid DownloadGuid { get; set; } = Guid.NewGuid();

        public double Percent => DownloadedBytes * 100.0 / Size;

        public List<string> Tags { get; set; }

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
