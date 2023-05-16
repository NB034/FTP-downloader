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

        public void BeginSearch(Uri uri)
        {
            throw new NotImplementedException();
        }
    }
}
