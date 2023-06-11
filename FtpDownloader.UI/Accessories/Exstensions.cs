using FluentFTP;
using FtpDownloader.Services.Accessories;
using FtpDownloader.Services.Interfaces.Models;
using FtpDownloader.Services.TestModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;

namespace FtpDownloader.UI.Accessories
{
    internal static class Exstensions
    {
        public static void AddTestInfoCollectorWithLogger(this IServiceCollection services)
        {
            services.AddSingleton<IInfoCollector, Test_InfoCollector_WithLogging>();
            services.AddSingleton<IAdvancedFtpLogger, FtpTxtLogger>(_ => new FtpTxtLogger(Path
                .Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FtpLog.txt")));
            services.AddSingleton<FtpConfig>(new FtpConfig
            {
                LogHost = true,
                LogUserName = true,
                LogPassword = true,
            });
        }
    }
}
