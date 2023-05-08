using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels.DataViewModels
{
    class DownloadViewModel : INotifyPropertyChanged
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

        public double DownloadedMegaBytes
        {
            get => _downloadedMegaBytes;
            set => SetProperty(ref _downloadedMegaBytes, value, nameof(DownloadedMegaBytes));
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

        private bool UseCredentials
        {
            get => _useCreadentials;
            set => SetProperty(ref _useCreadentials, value, nameof(UseCredentials));
        }

        private string Username
        {
            get => _username;
            set => SetProperty(ref _username, value, nameof(Username));
        }

        private string Password
        {
            get => _password;
            set => SetProperty(ref _password, value, nameof(Password));
        }

        public double Percent => DownloadedMegaBytes * 100.0 / Size;

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
