using FluentFTP;

namespace FtpDownloader.Services.Accessories
{
    public class FtpTxtLogger : IUniversalLogger
    {
        protected readonly string _path;

        public FtpTxtLogger(string path)
        {
            _path = path;
        }

        public void Log(FtpLogEntry entry)
        {
            var line = string.Format("Severity: {0}; Message: {1}; Exception: {2};",
                                      entry.Severity, entry.Message, entry.Exception?.Message ?? "null");

            File.AppendAllLines(_path, new[] { line });
        }

        public void Log(string message)
        {
            File.AppendAllLines(_path, new[] { message });
        }
    }
}
