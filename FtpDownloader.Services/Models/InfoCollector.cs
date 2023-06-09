﻿using FluentFTP;
using FtpDownloader.Services.DataTypes;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.Interfaces.ServicesEventArgs;
using FtpDownloader.Services.Mappers;

namespace FtpDownloader.Services.Models
{
    public class InfoCollector : IInfoCollector
    {
        readonly LogicLayerMapper _mapper;

        public InfoCollector(LogicLayerMapper mapper)
        {
            _mapper = mapper;
        }

        public event EventHandler<InfoCollectorNotificationEventArgs> SearchFinished;
        public event EventHandler<ExceptionThrownedEventArgs> SearchFailed;



        public void BeginSearch(string host, string path, string username = "", string password = "")
        {
            FtpClient client = new();
            try
            {
                if (username == "") client = new FtpClient(host);
                else client = new FtpClient(host, username, password);
            }
            catch (Exception ex)
            {
                client.Dispose();
                SearchFailed?.Invoke(this, new ExceptionThrownedEventArgs(ex));
                return;
            }

            Task.Run(() =>
            {
                try
                {
                    client.Connect();
                    var info = new Info { IsExist = client.FileExists(path) };
                    if (info.IsExist)
                    {
                        info.Exstention = Path.GetExtension(path);
                        info.SizeInBytes = (int)client.GetFileSize(path);
                    }
                    SearchFinished?.Invoke(this, new InfoCollectorNotificationEventArgs(_mapper.InfoToDto(info)));
                }
                catch (Exception ex)
                {
                    SearchFailed?.Invoke(this, new ExceptionThrownedEventArgs(ex));
                }
                finally
                {
                    client.Dispose();
                }
            });
        }
    }
}
