using FtpDownloader.Services.Accessories;
using FluentFTP;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Mappers;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;

namespace FtpDownloader.Services.TestModels
{
    public class Test_InfoCollectorWithLogger : IInfoCollector
    {
        readonly IAdvancedFtpLogger _logger;
        readonly FtpConfig _config;
        readonly LogicLayerMapper _mapper;

        public Test_InfoCollectorWithLogger(LogicLayerMapper mapper) : this(mapper, null, null) { }
        public Test_InfoCollectorWithLogger(LogicLayerMapper mapper, IAdvancedFtpLogger ftpLogger, FtpConfig config)
        {
            _config = config;
            _logger = ftpLogger;
            _mapper = mapper;
        }

        public event EventHandler<InfoCollectorNotificationEventArgs> SearchFinished;
        public event EventHandler<ExceptionThrownedEventArgs> SearchFailed;

        public void BeginSearch(string host, string path, string username = "", string password = "")
        {
            FtpClient client = new();
            try
            {
                client = new FtpClient(host, username, password, logger: _logger, config: _config);

                _logger?.Log($"# Host: {host}; Path: {path};");
                _logger?.Log($"# Username: {client.Credentials.UserName}; Password: {client.Credentials.Password}");
            }
            catch (Exception ex)
            {
                _logger?.Log("# Exception: " + ex.Message + ";");
                client.Dispose();
                _logger?.Log("------------------------------------");
                SearchFailed?.Invoke(this, new ExceptionThrownedEventArgs(ex));
                return;
            }


            Task.Run(() =>
            {
                try
                {
                    client.Connect();
                    var info = new Info { IsExist = client.FileExists(path) };
                    if (info.IsExist)
                    {
                        info.Exstention = Path.GetExtension(path);
                        info.SizeInBytes = (int)client.GetFileSize(path);
                    }

                    SearchFinished?.Invoke(this, new InfoCollectorNotificationEventArgs(_mapper.InfoToDto(info)));
                    client.Dispose();
                }
                catch (Exception ex)
                {
                    _logger?.Log("# Exception: " + ex.Message + ";");
                    client.Dispose();
                    _logger?.Log("------------------------------------");
                    SearchFailed?.Invoke(this, new ExceptionThrownedEventArgs(ex));
                }
            });
        }
    }
}
