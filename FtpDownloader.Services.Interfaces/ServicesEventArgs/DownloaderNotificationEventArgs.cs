using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class DownloaderNotificationEventArgs : EventArgs
    {
        private readonly LogicLayerDownloadDto _download;

        public DownloaderNotificationEventArgs(LogicLayerDownloadDto download)
        {
            _download = download;
        }

        public LogicLayerDownloadDto Download => _download;
    }
}
