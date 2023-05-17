using FluentFTP;

namespace FileDownloader.Services.Accessories
{
    public interface IUniversalLogger : IFtpLogger
    {
        void Log(string message);
    }
}
