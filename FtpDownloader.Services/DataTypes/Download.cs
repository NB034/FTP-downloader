namespace FtpDownloader.Services.DataTypes
{
    public class Download
    {
        public string Name { get; set; }
        public string Host { get; set; }
        public string Path { get; set; }
        public string ValidFullPath { get; set; }
        public string To { get; set; }
        public double Size { get; set; }
        public double DownloadedBytes { get; set; }
        public bool OnPause { get; set; }
        public bool Cancelling { get; set; }
        public bool UseCredentials { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DownloadDate { get; set; }
        public Guid DownloadGuid { get; set; }
        public List<string> Tags { get; set; }
    }
}
