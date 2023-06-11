using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IDownloader
    {
        event EventHandler<DownloaderNotificationEventArgs> DownloadStarted;
        event EventHandler<DownloaderNotificationEventArgs> DownloadProgressChanged;
        event EventHandler<DownloaderNotificationEventArgs> DownloadCancelled;
        event EventHandler<DownloaderNotificationEventArgs> DownloadCompleted;
        event EventHandler<DownloadFailedEventArgs> DownloadFailed;
        event EventHandler<ExceptionThrownedEventArgs> ExceptionThrowned;

        void StartNewDownload(LogicLayerDownloadDto dto);
        LogicLayerDownloadDto GetDownload(Guid downloadGuid);
        LogicLayerDownloadDto[] GetDownloads();

        void PauseAll();
        void ResumeAll();
        void CancelAll();

        void Pause(Guid downloadGuid);
        void Resume(Guid downloadGuid);
        void Cancel(Guid downloadGuid);

        Task FinalizeDownloads();
    }
}