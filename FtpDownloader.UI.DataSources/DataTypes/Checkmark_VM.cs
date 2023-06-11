using FtpDownloader.UI.DataSources.Accessories;
using System.ComponentModel;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class Checkmark_VM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsVerified { get; private set; } = false;
        public NotificationTypesEnum Icon { get; private set; } = NotificationTypesEnum.Neutral;



        public void Verify() { IsVerified = true; Icon = NotificationTypesEnum.Positive; Notify(); }
        public void Reject() { IsVerified = false; Icon = NotificationTypesEnum.Negative; Notify(); }
        public void Reset() { IsVerified = false; Icon = NotificationTypesEnum.Neutral; Notify(); }
        private void Notify() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
    }
}
