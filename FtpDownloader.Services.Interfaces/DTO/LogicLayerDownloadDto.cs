using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FtpDownloader.Services.Interfaces.DTO
{
    public class LogicLayerDownloadDto
    {
        public string Name { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public double Size { get; set; }
        public double DownloadedBytes { get; set; }
        public bool OnPause { get; set; }
        public bool Cancelling { get; set; }
        public bool UseCreadentials { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime DownloadDate { get; set; }
        public Guid DownloadGuid { get; set; }
        public List<string> Tags { get; set; } = new();
    }
}
