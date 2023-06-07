using FileDownloader.Services.Accessories;
using FluentFTP;

namespace FileDownloader.Services.Models.InfoCollectorModels
{
    public class InfoCollector : IInfoCollector
    {
        readonly IUniversalLogger _logger;
        readonly FtpConfig _config;

        public InfoCollector() : this(null, null) { }
        public InfoCollector(IUniversalLogger ftpLogger, FtpConfig config)
        {
            _config = config;
            _logger = ftpLogger;
        }

        public event Action<InfoModel> SearchFinished;

        public void BeginSearch(string uri, string username = "", string password = "")
        {
            var resourceUri = new Uri(uri);
            FtpClient client;

            if (username == String.Empty || password == string.Empty)
            {
                client = new FtpClient(resourceUri.Host, logger: _logger, config: _config);
            }
            else
            {
                client = new FtpClient(resourceUri.Host, username, password, logger: _logger, config: _config);
            }

            _logger.Log($"# Host: {resourceUri.Host}; Path: {resourceUri};");
            _logger.Log($"# Username: {client.Credentials.UserName}; Password: {client.Credentials.Password}") ;

            try
            {
                client.Connect();
                var info = new InfoModel { IsExist = client.FileExists(resourceUri.ToString()) };
                if (info.IsExist)
                {
                    info.Exstention = Path.GetExtension(resourceUri.ToString());
                    info.SizeInBytes = (Int32)client.GetFileSize(resourceUri.ToString());
                }

                SearchFinished?.Invoke(info);
                client.Dispose();
            }
            catch (Exception ex)
            {
                _logger?.Log("# Exception: " + ex.Message + ";");
                client.Dispose();
                SearchFinished?.Invoke(new InfoModel());
            }
        }
    }
}
