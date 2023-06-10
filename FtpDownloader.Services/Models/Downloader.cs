using FtpDownloader.Services.Accessories;
using FluentFTP;
using System.Net;
using FtpDownloader.Services.DataTypes;

namespace FtpDownloader.Services.Models
{
    public class Downloader //: IDownloader
    {
        public event Action<Download> DownloadStarted;
        public event Action<Download> DownloadCompleted;
        public event Action<Download, Exception> DownloadFailed;
        public event Action<Download> DownloadCancelled;
        public event Action<Download> DownloadedBytesNumberChanged;

        public List<Download> Downloads { get; }

        public bool CheckRemoteItem(Download downloadModel)
        {
            return true;
        }

        public void StartNewDownload(Download download)
        {
            Task.Run(() => StartNewDownloadAsync(download));
        }

        private async Task StartNewDownloadAsync(Download download)
        {
            var name = "";
            try
            {
                var distributor = new FileNamesDistributor();
                name = distributor.GetValidName(download.Name, download.To);
                var fileStream = File.Create(Path.Combine(download.To, name));

                AsyncFtpClient client = new AsyncFtpClient();
                client.Host = download.Host;
                if (download.UseCreadentials) client.Credentials = new NetworkCredential(download.Username, download.Password);

                await client.Connect();
                var ftpStream = await client.OpenRead(download.Path);
                var bytes = new byte[1024];

                using var br = new BinaryReader(ftpStream);
                using var bw = new BinaryWriter(fileStream);

                DownloadStarted?.Invoke(download);

                while (bytes.Length > 0 || download.OnPause)
                {
                    if (download.Cancelling)
                    {
                        if (File.Exists(name)) File.Delete(name);
                        DownloadCancelled?.Invoke(download);
                        return;
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
