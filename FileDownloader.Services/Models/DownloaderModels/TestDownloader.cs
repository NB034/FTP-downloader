namespace FileDownloader.Services.Models.DownloaderModels
{
    public class TestDownloader : IDownloader
    {
        public event Action<DownloadModel> DownloadCancelled;
        public event Action<DownloadModel> DownloadCompleted;
        public event Action<DownloadModel> DownloadedBytesNumberChanged;
        public event Action<DownloadModel, Exception> DownloadFailed;
        public event Action<DownloadModel> DownloadStarted;

        public bool CheckRemoteItem(DownloadModel downloadModel)
        {
            return true;
        }

        public void StartNewDownload(DownloadModel download)
        {
            Task.Run(() =>
            {
                DownloadStarted?.Invoke(download);

                var counter = 0;
                while (counter < 100)
                {
                    if (download.Cancelling)
                    {
                        DownloadCancelled?.Invoke(download);
                        return;
                    }

                    if (download.OnPause) break;
                    else counter++;

                    Task.Delay(1000).Wait();
                    download.DownloadedBytes += download.Size / 100;
                    DownloadedBytesNumberChanged?.Invoke(download);
                }

                DownloadCompleted?.Invoke(download);
            });
        }
    }
}
