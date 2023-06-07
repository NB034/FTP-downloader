﻿
namespace FtpDownloader.Services.Models.InfoCollectorModels
{
    public interface IInfoCollector
    {
        event Action<InfoModel> SearchFinished;
        void BeginSearch(string uri, string username = "", string password = "");
    }
}