using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class DownloadFailedEventArgs : DownloaderNotificationEventArgs
    {
        private readonly Exception _exception;

        public DownloadFailedEventArgs(LogicLayerDownloadDto downloadDto, Exception exception) : base(downloadDto)
        {
            _exception = exception;
        }

        public Exception Exception => _exception;
    }
}
