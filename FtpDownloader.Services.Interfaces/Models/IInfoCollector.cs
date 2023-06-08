using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Models.InfoCollectorModels
{
    public interface IInfoCollector
    {
        event Action<InfoDto> SearchFinished;
        void BeginSearch(string uri, string username = "", string password = "");
    }
}
