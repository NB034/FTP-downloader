using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Models.DownloaderModels
{
    public interface IDownloader
    {
        event Action<DownloadDto> DownloadStarted;
        event Action<DownloadDto> DownloadedProgressChanged;

        event Action<DownloadDto> DownloadCancelled;
        event Action<DownloadDto> DownloadCompleted;
        event Action<DownloadDto, Exception> DownloadFailed;

        List<DownloadDto> Downloads { get; }

        bool CheckRemoteItem(DownloadDto download);
        void StartNewDownload(DownloadDto download);
        DownloadDto GetDownload(Guid downloadGuid);

        void PauseAll();
        void ResumeAll();
        void CancelAll();

        void Pause(Guid downloadGuid);
        void Resume(Guid downloadGuid);
        void Cancel(Guid downloadGuid);
    }
}