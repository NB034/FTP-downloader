using System.ComponentModel;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class Download_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name = "";
        private string _host = "";
        private string _path = "";
        private string _to = "";
        private string _username = "";
        private string _password = "";
        private double _size = 0;
        private double _downloadedMegaBytes = 0;
        private bool _onPause = false;
        private bool _cancelling = false;
        private bool _useCreadentials = false;

        public string Name { get => _name; set => SetProperty(ref _name, value, nameof(Name)); }
        public string Host { get => _host; set => SetProperty(ref _host, value, nameof(Host)); }
        public string Path { get => _path; set => SetProperty(ref _path, value, nameof(Path)); }
        public string To { get => _to; set => SetProperty(ref _to, value, nameof(To)); }
        public string Username { get => _username; set => SetProperty(ref _username, value, nameof(Username)); }
        public string Password { get => _password; set => SetProperty(ref _password, value, nameof(Password)); }
        public double Size { get => _size; set => SetProperty(ref _size, value, nameof(Size)); }
        public bool OnPause { get => _onPause; set => SetProperty(ref _onPause, value, nameof(OnPause)); }
        public bool Cancelling { get => _cancelling; set => SetProperty(ref _cancelling, value, nameof(Cancelling)); }
        public bool UseCredentials { get => _useCreadentials; set => SetProperty(ref _useCreadentials, value, nameof(UseCredentials)); }
        public double DownloadedBytes
        {
            get => _downloadedMegaBytes;
            set
            {
                _downloadedMegaBytes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Percent)));
            }
        }



        public double Percent => DownloadedBytes / Size * 100;
        public Guid DownloadGuid { get; set; } = Guid.NewGuid();
        public List<string> Tags { get; set; } = new();



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
