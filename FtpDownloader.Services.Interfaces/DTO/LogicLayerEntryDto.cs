
namespace FtpDownloader.Services.Interfaces.DTO
{
    public class LogicLayerEntryDto
    {
        public int Id { get; set; } = 0;
        public string RemotePath { get; set; } = string.Empty;
        public string LocalPath { get; set; } = string.Empty;
        public DateTime DownloadDate { get; set; }
        public int FileSize { get; set; }
        public bool WasSuccessful { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
