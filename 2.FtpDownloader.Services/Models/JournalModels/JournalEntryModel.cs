
namespace FtpDownloader.Services.Models.JournalModels
{
    public class JournalEntryModel
    {
        public int Id { get; set; }
        public string RemotePath { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public DateTime DownloadDate { get; set; }
        public int FileSize { get; set; }
        public bool WasSuccessful { get; set; }

        public virtual List<string> Tags { get; set; }
    }
}
