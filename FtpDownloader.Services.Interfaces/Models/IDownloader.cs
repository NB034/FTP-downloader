using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.Models
{
    public interface IDownloader
    {
        event Action<LogicLayerDownloadDto> DownloadStarted;
        event Action<LogicLayerDownloadDto> DownloadedProgressChanged;
        event Action<LogicLayerDownloadDto> DownloadCancelled;
        event Action<LogicLayerDownloadDto> DownloadCompleted;
        event Action<LogicLayerDownloadDto, Exception> DownloadFailed;

        Task StartNewDownload(LogicLayerDownloadDto dto);
        LogicLayerDownloadDto GetDownload(Guid downloadGuid);
        LogicLayerDownloadDto[] GetDownloads();

        void PauseAll();
        void ResumeAll();
        void CancelAll();

        void Pause(Guid downloadGuid);
        void Resume(Guid downloadGuid);
        void Cancel(Guid downloadGuid);
    }
}