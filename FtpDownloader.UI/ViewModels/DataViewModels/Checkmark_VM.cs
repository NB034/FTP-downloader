using FtpDownloader.Resources.ResourcesAccess;
using System.ComponentModel;
using System.Windows.Media;

namespace FtpDownloader.ViewModels.DataViewModels
{
    internal class Checkmark_VM : INotifyPropertyChanged
    {
        public bool IsVerified { get; private set; } = false;
        public ImageBrush Icon { get; private set; } = IconsManager.NeutralIcon;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Verify() { IsVerified = true; Icon = IconsManager.PositiveIcon; Notify(); }
        public void Reject() { IsVerified = false; Icon = IconsManager.NegativeIcon; Notify(); }
        public void Reset() { IsVerified = false; Icon = IconsManager.NeutralIcon; Notify(); }

        private void Notify() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Icon)));
    }
}
