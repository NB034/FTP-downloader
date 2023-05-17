using FluentFTP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.Services.Models.InfoCollectorModels
{
    public class TestInfoCollector : IInfoCollector
    {
        public event Action<InfoModel> SearchFinished;

        public async void BeginSearch(string uri)
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1500);
                SearchFinished?.Invoke(new InfoModel { IsExist = true, SizeInBytes = 10_000, Exstention = ".txt" });
            });
        }
    }
}
