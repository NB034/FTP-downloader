
namespace FtpDownloader.Services.Models.DownloaderModels
{
    public class TestDownloader : IDownloader
    {
        public event Action<DownloadModel> DownloadStarted;
        public event Action<DownloadModel> DownloadedProgressChanged;

        public event Action<DownloadModel> DownloadCancelled;
        public event Action<DownloadModel> DownloadCompleted;
        public event Action<DownloadModel, Exception> DownloadFailed;

        public List<DownloadModel> Downloads { get; } = new List<DownloadModel>();

        public bool CheckRemoteItem(DownloadModel downloadModel) => true;

        public async void StartNewDownload(DownloadModel download)
        {
            Downloads.Add(download);
            await Task.Run(async () =>
            {
                DownloadStarted?.Invoke(download);

                var counter = 0;
                while (counter < 5)
                {
                    if (download.Cancelling)
                    {
                        Downloads.Remove(download);
                        DownloadCancelled?.Invoke(download);
                        return;
                    }

                    if (download.OnPause) SpinWait.SpinUntil(() => !download.OnPause);
                    else counter++;

                    await Task.Delay(1000);
                    download.DownloadedBytes += download.Size / 5;
                    DownloadedProgressChanged?.Invoke(download);
                }

                Downloads.Remove(download);
                DownloadCompleted?.Invoke(download);
            });
        }

        public DownloadModel GetDownload(Guid downloadGuid)
        {
            return Downloads.Where(d => d.DownloadGuid == downloadGuid).FirstOrDefault()
                ?? throw new ArgumentException("Request by invalid guid");
        }

        public void PauseAll() => Downloads.ForEach(d => d.OnPause = true);
        public void ResumeAll() => Downloads.ForEach(d => d.OnPause = false);
        public void CancelAll() => Downloads.ForEach(d => d.Cancelling = true);

        public void Pause(Guid downloadGuid)
        {
            var download = Downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.OnPause = true;
        }

        public void Resume(Guid downloadGuid)
        {
            var download = Downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.OnPause = false;
        }

        public void Cancel(Guid downloadGuid)
        {
            var download = Downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.Cancelling = true;
        }
    }
}
