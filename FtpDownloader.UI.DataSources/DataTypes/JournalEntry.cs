using FtpDownloader.UI.DataSources.Accessories;

namespace FtpDownloader.UI.DataSources.DataTypes
{
    public class JournalEntry
    {
        public int Id { get; set; } = 0;
        public string FileName { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public string RemotePath { get; set; } = string.Empty;
        public string DownloadDate { get; set; } = string.Empty;
        public double FileSize { get; set; } = 0;
        public NotificationTypesEnum Result { get; set; } = NotificationTypesEnum.Neutral;
        public List<string> Tags { get; set; } = new();
    }
}
