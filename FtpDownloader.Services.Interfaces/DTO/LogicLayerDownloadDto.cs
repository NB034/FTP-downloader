
namespace FtpDownloader.Services.Interfaces.DTO
{
    public class LogicLayerDownloadDto
    {
        public string Name { get; set; } = string.Empty;
        public string Host { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string To { get; set; } = string.Empty;
        public double Size { get; set; }
        public double DownloadedBytes { get; set; }
        public bool OnPause { get; set; }
        public bool Cancelling { get; set; }
        public bool UseCredentials { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime DownloadDate { get; set; }
        public Guid DownloadGuid { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
