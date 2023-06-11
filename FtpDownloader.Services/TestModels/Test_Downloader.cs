using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class Test_Downloader : IDownloader
    {
        private readonly LogicLayerMapper _mapper;
        private readonly List<Download> _downloads;

        public Test_Downloader(LogicLayerMapper mapper)
        {
            _mapper = mapper;
            _downloads = new List<Download>();
            //Seed();
        }

        public event EventHandler<DownloaderNotificationEventArgs> DownloadStarted;
        public event EventHandler<DownloaderNotificationEventArgs> DownloadProgressChanged;
        public event EventHandler<DownloaderNotificationEventArgs> DownloadCancelled;
        public event EventHandler<DownloaderNotificationEventArgs> DownloadCompleted;
        public event EventHandler<DownloadFailedEventArgs> DownloadFailed;
        public event EventHandler<ExceptionThrownedEventArgs> ExceptionThrowned;



        public void PauseAll() => _downloads.ForEach(d => d.OnPause = true);
        public void ResumeAll() => _downloads.ForEach(d => d.OnPause = false);
        public void CancelAll() => _downloads.ForEach(d => d.Cancelling = true);



        public void Pause(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                HandleException(new ArgumentException("Request by invalid guid"));
                return;
            }
            download.OnPause = true;
        }

        public void Resume(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                HandleException(new ArgumentException("Request by invalid guid")); return;
            }
            download.OnPause = false;
        }

        public void Cancel(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                HandleException(new ArgumentException("Request by invalid guid"));
                return;
            }
            download.Cancelling = true;
        }



        public LogicLayerDownloadDto GetDownload(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                HandleException(new ArgumentException("Request by invalid guid"));
                return new();
            }
            return _mapper.DownloadToDto(download);
        }

        public LogicLayerDownloadDto[] GetDownloads()
        {
            var dtos = new List<LogicLayerDownloadDto>();
            if (!_downloads.Any()) return dtos.ToArray();
            foreach (var download in _downloads)
            {
                dtos.Add(_mapper.DownloadToDto(download));
            }
            return dtos.ToArray();
        }




        public void StartNewDownload(LogicLayerDownloadDto dto)
        {
            var download = _mapper.DtoToDownload(dto);

            _downloads.Add(download);
            Task.Run(async () =>
            {
                DownloadStarted?.Invoke(this, new DownloaderNotificationEventArgs(_mapper.DownloadToDto(download)));

                var counter = 0;
                while (counter < 5)
                {
                    if (download.Cancelling)
                    {
                        download.DownloadDate = DateTime.Now;
                        _downloads.Remove(download);
                        DownloadStarted?.Invoke(this, new DownloaderNotificationEventArgs(_mapper.DownloadToDto(download)));
                        return;
                    }

                    if (download.OnPause) continue;
                    else counter++;

                    await Task.Delay(1000);
                    download.DownloadedBytes += download.Size / 5;
                    DownloadStarted?.Invoke(this, new DownloaderNotificationEventArgs(_mapper.DownloadToDto(download)));
                }

                download.DownloadDate = DateTime.Now;
                _downloads.Remove(download);
                DownloadStarted?.Invoke(this, new DownloaderNotificationEventArgs(_mapper.DownloadToDto(download)));
            });
        }

        public async Task FinalizeDownloads()
        {
            _downloads.ForEach(d => d.Cancelling = true);
        }

        

        private void HandleException(Exception ex)
        {
            var args = new ExceptionThrownedEventArgs(ex);
            ExceptionThrowned?.Invoke(this, args);
        }

        private void Seed()
        {
            StartNewDownload(new LogicLayerDownloadDto
            {
                Cancelling = false,
                DownloadDate = DateTime.Now,
                DownloadedBytes = 0,
                DownloadGuid = Guid.NewGuid(),
                Host = "127.0.0.1",
                Path = "Some/where",
                Name = "Test",
                OnPause = true,
                Password = string.Empty,
                Size = 1024,
                Tags = new List<string> { "test", "oneMoreTest" },
                To = "Here",
                UseCredentials = false,
                Username = "Test",
            });
        }
    }
}
