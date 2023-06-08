using FtpDownloader.Resources.ResourceAccess;
using System.Collections.Generic;

namespace FtpDownloader.ViewModels.DataViewModels
{
    internal class JournalEntry_VM
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string LocalPath { get; set; }
        public string RemotePath { get; set; }
        public string DownloadDate { get; set; }
        public double FileSize { get; set; }
        public NotificationTypesEnum Result { get; set; }

        public List<string> Tags { get; set; }
    }
}
