using FtpDownloader.UI.DataSources.Accessories;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class Notification_VM
    {
        public NotificationTypesEnum Type { get; set; }
        public string Message { get; set; } = "";
    }
}
