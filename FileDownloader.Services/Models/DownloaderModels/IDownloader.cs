namespace FileDownloader.Services.Models.DownloaderModels
{
    public interface IDownloader
    {
        event Action<DownloadModel> DownloadCancelled;
        event Action<DownloadModel> DownloadCompleted;
        event Action<DownloadModel> DownloadedBytesNumberChanged;
        event Action<DownloadModel, Exception> DownloadFailed;
        event Action<DownloadModel> DownloadStarted;

        void StartNewDownload(DownloadModel download);
    }
}