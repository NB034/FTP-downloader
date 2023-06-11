using FtpDownloader.UI.DataSources.Accessories;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class Notification
    {
        public NotificationTypesEnum Type { get; set; } = NotificationTypesEnum.Neutral;
        public string Message { get; set; } = "";
    }
}
