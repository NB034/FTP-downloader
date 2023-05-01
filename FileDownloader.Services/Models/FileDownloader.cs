using FileDownloader.Services.Accessories;
using FluentFTP;
using System.Net;

namespace FileDownloader.Services.Models
{
    public class FileDownloader
    {
        public event Action<DownloadModel> DownloadStarted;
        public event Action<DownloadModel> DownloadCompleted;
        public event Action<DownloadModel, Exception> DownloadFailed;
        public event Action<DownloadModel> DownloadCancelled;
        public event Action<DownloadModel> DownloadedBytesNumberChanged;

        public void StartNewDownload(DownloadModel download)
        {
            Task.Run(() => StartNewDownloadAsync(download));
        }

        private async Task StartNewDownloadAsync(DownloadModel download)
        {
            var name = "";
            try
            {
                var distributor = new FileNamesDistributor();
                name = distributor.GetValidName(download.Name, download.To);
                var fileStream = File.Create(Path.Combine(download.To, name));

                AsyncFtpClient client = new AsyncFtpClient();
                client.Host = new Uri(download.From).Host;
                if (download.UseCreadentials) client.Credentials = new NetworkCredential(download.Username, download.Password);

                await client.Connect();
                var ftpStream = await client.OpenRead(download.From);
                var bytes = new byte[1024];

                using var br = new BinaryReader(ftpStream);
                using var bw = new BinaryWriter(fileStream);

                DownloadStarted?.Invoke(download);

                while (bytes.Length > 0 || download.OnPause)
                {
                    if(download.Cancelling)
                    {
                        if (File.Exists(name)) File.Delete(name);
                        break;
                    }

                    if (download.OnPause) continue;

                    bytes = br.ReadBytes(bytes.Length);
                    bw.Write(bytes, 0, bytes.Length);
                    download.DownloadedBytes += bytes.Length;

                    DownloadedBytesNumberChanged?.Invoke(download);
                }

                DownloadCompleted?.Invoke(download);
            }
            catch (Exception ex)
            {
                if (File.Exists(name)) File.Delete(name);

                DownloadFailed?.Invoke(download, ex);
            }
        }
    }
}
