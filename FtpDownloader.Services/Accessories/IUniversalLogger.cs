using FluentFTP;

namespace FtpDownloader.Services.Accessories
{
    public interface IAdvancedFtpLogger : IFtpLogger
    {
        void Log(string message);
    }
}
