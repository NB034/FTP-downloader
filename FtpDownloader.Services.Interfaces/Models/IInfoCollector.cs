using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IInfoCollector
    {
        event EventHandler<InfoCollectorNotificationEventArgs> SearchFinished;
        event EventHandler<ExceptionThrownedEventArgs> SearchFailed;

        void BeginSearch(string host, string path, string username = "", string password = "");
    }
}
