namespace FileDownloader.Services.Models.DownloaderModels
{
    public interface IDownloader
    {
        event Action<DownloadModel> DownloadStarted;
        event Action<DownloadModel> DownloadedProgressChanged;

        event Action<DownloadModel> DownloadCancelled;
        event Action<DownloadModel> DownloadCompleted;
        event Action<DownloadModel, Exception> DownloadFailed;

        List<DownloadModel> Downloads { get; }

        bool CheckRemoteItem(DownloadModel downloadModel);
        void StartNewDownload(DownloadModel download);
        DownloadModel GetDownload(Guid downloadGuid);

        void PauseAll();
        void ResumeAll();
        void CancelAll();

        void Pause(Guid downloadGuid);
        void Resume(Guid downloadGuid);
        void Cancel(Guid downloadGuid);
    }
}