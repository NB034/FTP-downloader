using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.TestModels
{
    public class Test_FakeInfoCollector : IInfoCollector
    {
        private readonly LogicLayerMapper _mapper;

        public Test_FakeInfoCollector(LogicLayerMapper mapper)
        {
            _mapper = mapper;
        }

        public event Action<LogicLayerInfoDto> SearchFinished;
        public event Action<Exception> SearchFailed;

        public void BeginSearch(string host, string path, string username = "", string password = "")
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);
                SearchFinished?.Invoke(_mapper.InfoToDto(new Info { IsExist = true, SizeInBytes = 10_000, Exstention = ".txt" }));
            });
        }
    }
}
