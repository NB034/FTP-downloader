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

        private string _from = "";
        private string _to = "";
        private int _size = 0;
        private int _downloadedBytes = 0;
        private bool _onPause = false;

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

        public int Size
        {
            get => _size;
            set => SetProperty(ref _size, value, nameof(Size));
        }

        public int DownloadedBytes
        {
            get => _downloadedBytes;
            set => SetProperty(ref _downloadedBytes, value, nameof(DownloadedBytes));
        }

        public bool OnPause
        {
            get => _onPause;
            set => SetProperty(ref _onPause, value, nameof(OnPause));
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
