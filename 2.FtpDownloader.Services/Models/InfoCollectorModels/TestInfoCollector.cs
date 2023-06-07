using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Services.Models.InfoCollectorModels
{
    public class TestInfoCollector : IInfoCollector
    {
        public event Action<InfoModel> SearchFinished;

        public void BeginSearch(string uri, string username = "", string password = "")
        {
            Task.Run(async () =>
            {
                await Task.Delay(1500);
                SearchFinished?.Invoke(new InfoModel { IsExist = true, SizeInBytes = 10_000, Exstention = ".txt" });
            });
        }
    }
}
