
namespace FtpDownloader.Services.Interfaces.DTO
{
    public class LogicLayerInfoDto
    {
        public bool IsExist { get; set; } = false;
        public int SizeInBytes { get; set; } = 0;
        public string Exstention { get; set; } = string.Empty;
    }
}
