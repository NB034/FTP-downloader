using System.Collections.Generic;
using System.Windows.Media;

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
        public ImageBrush Result { get; set; }

        public List<string> Tags { get; set; }
    }
}
