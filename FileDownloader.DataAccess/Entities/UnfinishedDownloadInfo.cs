using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.DataAccess.Entities
{
    internal class UnfinishedDownloadInfo
    {
        public string remotePath = "";
        public string localDirectory = "";
        public string fileName = "";
        public string fileLastChangeDate = "";
        public int downloadedBytes = 0;
    }
}
