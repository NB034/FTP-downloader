using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Services.Interfaces.DTO
{
    public class LogicLayerInfoDto
    {
        public bool IsExist { get; set; } = false;
        public Int32 SizeInBytes { get; set; } = 0;
        public string Exstention { get; set; } = string.Empty;
    }
}
