namespace FtpDownloader.Services.DataTypes
{
    public class Info
    {
        public bool IsExist { get; set; } = false;
        public int SizeInBytes { get; set; } = 0;
        public string Exstention { get; set; } = string.Empty;
    }
}
