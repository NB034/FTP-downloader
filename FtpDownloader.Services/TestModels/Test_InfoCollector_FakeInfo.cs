using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class Test_InfoCollector_FakeInfo : IInfoCollector
    {
        private readonly LogicLayerMapper _mapper;

        public Test_InfoCollector_FakeInfo(LogicLayerMapper mapper)
        {
            _mapper = mapper;
        }

        public event EventHandler<InfoCollectorNotificationEventArgs> SearchFinished;
        public event EventHandler<ExceptionThrownedEventArgs> SearchFailed;



        public void BeginSearch(string host, string path, string username = "", string password = "")
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);
                SearchFinished?.Invoke(this, new InfoCollectorNotificationEventArgs(_mapper.InfoToDto(new Info
                {
                    IsExist = true,
                    SizeInBytes = 10_000,
                    Exstention = ".txt"
                })));
            });
        }
    }
}
