using FtpDownloader.Services.Interfaces.DTO;

namespace FtpDownloader.Services.Interfaces.ServicesEventArgs
{
    public class InfoCollectorNotificationEventArgs : EventArgs
    {
        private readonly LogicLayerInfoDto _info;

        public InfoCollectorNotificationEventArgs(LogicLayerInfoDto infoDto)
        {
            _info = infoDto;
        }

        public LogicLayerInfoDto Info => _info;
    }
}
