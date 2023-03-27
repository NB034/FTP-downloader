using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader.DataAccess.DataTypes
{
    internal class DownloadedFileInfo : BaseFileInfo
    {
        public string localPath = "";
        public string downloadDate = "";
    }
}
