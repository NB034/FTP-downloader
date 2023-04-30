using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    class DownloadViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string _name = "";
        private string _from = "";
        private string _to = "";
        private double _size = 0;
        private double _downloadedMegaBytes = 0;
        private bool _onPause = false;

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
