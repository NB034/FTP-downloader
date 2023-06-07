
namespace FileDownloader.Services.Models.InfoCollectorModels
{
    public class InfoModel
    {
        public bool IsExist { get; set; } = false;
        public Int32 SizeInBytes { get; set; } = 0;
        public string Exstention { get; set; } = string.Empty;
    }
}
