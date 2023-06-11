using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class InfoCollectorNotificationEventArgs : EventArgs
    {
        private readonly LogicLayerInfoDto _infoDto;

        public InfoCollectorNotificationEventArgs(LogicLayerInfoDto infoDto)
        {
            _infoDto = infoDto;
        }

        public LogicLayerInfoDto InfoDto => _infoDto;
    }
}
