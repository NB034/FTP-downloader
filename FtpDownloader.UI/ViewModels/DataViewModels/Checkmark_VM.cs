using FtpDownloader.Resources.ResourceAccess;
using System.ComponentModel;
using System.Windows.Media;

namespace FtpDownloader.ViewModels.DataViewModels
{
    internal class Checkmark_VM : INotifyPropertyChanged
    {
        public bool IsVerified { get; private set; } = false;
        public NotificationTypesEnum Icon { get; private set; } = NotificationTypesEnum.Neutral;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Verify() { IsVerified = true; Icon = NotificationTypesEnum.Neutral; Notify(); }
        public void Reject() { IsVerified = false; Icon = NotificationTypesEnum.Neutral; Notify(); }
        public void Reset() { IsVerified = false; Icon = NotificationTypesEnum.Neutral; Notify(); }

        private void Notify() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
    }
}
