using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class DownloaderNotificationEventArgs : EventArgs
    {
        private readonly LogicLayerDownloadDto _downloadDto;

        public DownloaderNotificationEventArgs(LogicLayerDownloadDto downloadDto)
        {
            _downloadDto = downloadDto;
        }

        public LogicLayerDownloadDto DownloadDto => _downloadDto;
    }
}
