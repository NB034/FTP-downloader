using FtpDownloader.Services.Accessories;
using FluentFTP;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.Models
{
    public class SmallFilesDownloader : IDownloader
    {
        private readonly LogicLayerMapper _mapper;
        private readonly FileNamesDistributor _distributor;
        private List<Download> _downloads;

        public SmallFilesDownloader(LogicLayerMapper mapper, FileNamesDistributor namesDistributor)
        {
            _mapper = mapper;
            _distributor = namesDistributor;
            _downloads = new List<Download>();
        }

        public event Action<LogicLayerDownloadDto> DownloadStarted;
        public event Action<LogicLayerDownloadDto> DownloadProgressChanged;
        public event Action<LogicLayerDownloadDto> DownloadCancelled;
        public event Action<LogicLayerDownloadDto> DownloadCompleted;
        public event Action<LogicLayerDownloadDto, Exception> DownloadFailed;
        public event Action<Exception> ExceptionThrowned;

        public void PauseAll() => _downloads.ForEach(d => d.OnPause = true);
        public void ResumeAll() => _downloads.ForEach(d => d.OnPause = false);
        public void CancelAll() => _downloads.ForEach(d => d.Cancelling = true);



        public void Pause(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                ExceptionThrowned?.Invoke(new ArgumentException("Request by invalid guid"));
                return;
            }
            download.OnPause = true;
        }

        public void Resume(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                ExceptionThrowned?.Invoke(new ArgumentException("Request by invalid guid"));
                return;
            }
            download.OnPause = false;
        }

        public void Cancel(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                ExceptionThrowned?.Invoke(new ArgumentException("Request by invalid guid"));
                return;
            }
            download.Cancelling = true;
        }



        public LogicLayerDownloadDto GetDownload(Guid downloadGuid)
        {
            var download = _downloads.FirstOrDefault(d => d.DownloadGuid == downloadGuid);
            if (download == null)
            {
                ExceptionThrowned?.Invoke(new ArgumentException("Request by invalid guid"));
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
            if (dto.Size >= 10_485_760) // = (10 MB)
            {
                DownloadFailed?.Invoke(dto, new Exception("Files larger than 10 MB are not supported"));
                return;
            }

            var download = _mapper.DtoToDownload(dto);
            _downloads.Add(download);

            AsyncFtpClient client = new();
            string validName = "";

            Task.Run(async () =>
            {
                try
                {
                    validName = _distributor.GetValidName(download.Name, download.To);
                    download.ValidFullPath = Path.Combine(download.To, validName);
                    using var fileStream = File.Create(download.ValidFullPath);

                    if (download.UseCredentials) client = new AsyncFtpClient(download.Host, download.Username, download.Password);
                    else client = new AsyncFtpClient(download.Host);

                    await client.Connect();
                    var ftpStream = await client.OpenRead(download.Path);
                    var bytes = new byte[1024];

                    using var br = new BinaryReader(ftpStream);
                    using var bw = new BinaryWriter(fileStream);

                    DownloadStarted?.Invoke(_mapper.DownloadToDto(download));

                    while (bytes.Length > 0 || download.OnPause)
                    {
                        if (download.Cancelling)
                        {
                            if (File.Exists(Path.Combine(download.To, validName))) File.Delete(Path.Combine(download.To, validName));
                            DownloadCancelled?.Invoke(_mapper.DownloadToDto(download));
                            client.Dispose();
                            return;
                        }

                        if (download.OnPause) { await Task.Delay(100); continue; }

                        bytes = br.ReadBytes(bytes.Length);
                        bw.Write(bytes, 0, bytes.Length);
                        download.DownloadedBytes += bytes.Length;

                        DownloadProgressChanged?.Invoke(_mapper.DownloadToDto(download));
                    }

                    download.DownloadDate = DateTime.Now;
                    DownloadCompleted?.Invoke(_mapper.DownloadToDto(download));
                }
                catch (Exception ex)
                {
                    download.DownloadDate = DateTime.Now;
                    DownloadFailed?.Invoke(_mapper.DownloadToDto(download), ex);
                    await GarbageCollector(download.ValidFullPath);
                }
                finally
                {
                    client.Dispose();
                }
            });
        }



        public async Task FinalizeDownloads()
        {
            _downloads.ForEach(d => d.Cancelling = true);
            foreach (var download in _downloads.ToArray())
            {
                await GarbageCollector(download.ValidFullPath);
                _downloads.Remove(download);
            }
        }



        private async Task GarbageCollector(string path)
        {
            while (File.Exists(path))
            {
                try { File.Delete(path); }
                catch { }
                await Task.Delay(500);
            }
        }
    }
}
