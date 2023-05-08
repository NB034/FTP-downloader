using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace File_downloader.ViewModels.DataViewModels
{
    class NotificationViewModel
    {
        public ImageBrush Image { get; set; }
        public string Message { get; set; } = "";
    }
}
