using FtpDownloader.Resources.ResourceAccess;

namespace FtpDownloader.ViewModels.DataViewModels
{
    class Notification_VM
    {
        public NotificationTypesEnum Type { get; set; }
        public string Message { get; set; } = "";
    }
}
