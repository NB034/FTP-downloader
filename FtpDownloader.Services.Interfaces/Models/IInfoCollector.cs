using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IInfoCollector
    {
        event Action<LogicLayerInfoDto> SearchFinished;
        void BeginSearch(string uri, string username = "", string password = "");
    }
}
