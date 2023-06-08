using FluentFTP;

namespace FtpDownloader.Services.Accessories
{
    public interface IUniversalLogger : IFtpLogger
    {
        void Log(string message);
    }
}
