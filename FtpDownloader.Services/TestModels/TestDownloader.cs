using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class TestDownloader : IDownloader
    {
        private readonly LogicLayerMapper _mapper;
        private List<Download> _downloads;

        public TestDownloader(LogicLayerMapper mapper)
        {
            _mapper = mapper;
            _downloads = new List<Download>();

            Seed();
        }

        public event Action<LogicLayerDownloadDto> DownloadStarted;
        public event Action<LogicLayerDownloadDto> DownloadedProgressChanged;
        public event Action<LogicLayerDownloadDto> DownloadCancelled;
        public event Action<LogicLayerDownloadDto> DownloadCompleted;
        public event Action<LogicLayerDownloadDto, Exception> DownloadFailed;
  


        public void PauseAll() => _downloads.ForEach(d => d.OnPause = true);
        public void ResumeAll() => _downloads.ForEach(d => d.OnPause = false);
        public void CancelAll() => _downloads.ForEach(d => d.Cancelling = true);



        public void Pause(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.OnPause = true;
        }

        public void Resume(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.OnPause = false;
        }

        public void Cancel(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid)
                ?? throw new ArgumentException("Request by invalid guid");
            download.Cancelling = true;
        }




        public LogicLayerDownloadDto GetDownload(Guid downloadGuid)
        {
            var download = _downloads.Where(d => d.DownloadGuid == downloadGuid).FirstOrDefault()
                ?? throw new ArgumentException("Request by invalid guid");

            return _mapper.DownloadToDto(download);
        }

        public LogicLayerDownloadDto[] GetDownloads()
        {
            var dtos = new List<LogicLayerDownloadDto>();
            foreach (var download in _downloads)
            {
                dtos.Add(_mapper.DownloadToDto(download));
            }
            return dtos.ToArray();
        }




        public async Task StartNewDownload(LogicLayerDownloadDto dto)
        {
            var download = _mapper.DtoToDownload(dto);

            _downloads.Add(download);
            await Task.Run(async () =>
            {
                DownloadStarted?.Invoke(dto);

                var counter = 0;
                while (counter < 5)
                {
                    if (download.Cancelling)
                    {
                        _downloads.Remove(download);
                        DownloadCancelled?.Invoke(dto);
                        return;
                    }

                    if (download.OnPause) SpinWait.SpinUntil(() => !download.OnPause);
                    else counter++;

                    await Task.Delay(1000);
                    download.DownloadedBytes += download.Size / 5;
                    DownloadedProgressChanged?.Invoke(dto);
                }

                _downloads.Remove(download);
                DownloadCompleted?.Invoke(dto);
            });
        }




        private void Seed()
        {
            _downloads.AddRange(new[]
            {
                new Download
                {
                     Cancelling = false,
                     DownloadDate= DateTime.Now,
                     DownloadedBytes = 0,
                     DownloadGuid = Guid.Empty,
                     From = "Some/where",
                     Name = "Test",
                     OnPause= true,
                     Password = string.Empty,
                     Size = 1024,
                     Tags = new List<string> {"test", "oneMoreTest"},
                     To = "Here",
                     UseCreadentials = false,
                     Username = "Test",
                }
            });
        }
    }
}
