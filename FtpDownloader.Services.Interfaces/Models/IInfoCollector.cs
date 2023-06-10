using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IInfoCollector
    {
        event Action<LogicLayerInfoDto> SearchFinished;
        event Action<Exception> SearchFailed;
        void BeginSearch(string host, string path, string username = "", string password = "");
    }
}
