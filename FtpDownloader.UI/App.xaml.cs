using FtpDownloader.Services.Interfaces.Models;
using System;
using System.Windows;
using FtpDownloader.Services.TestModels;
using FtpDownloader.UI.DataSources.ViewModels;
using FtpDownloader.UI.Windows;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FtpDownloader.DataAccess.Interfaces.Repositories;
using FtpDownloader.DataAccess.Repositories;
using System.IO;
using FtpDownloader.Services.Accessories;
using FluentFTP;
using FtpDownloader.Services.Models;
using FtpDownloader.DataAccess.Contexts;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace FtpDownloader.UI
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Settings"));
                    configBuilder.AddJsonFile("appsettings.json", false);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<MainWindow>();

                    services.AddSingleton<MainWindow_VM>();
                    services.AddSingleton<DownloadList_VM>();
                    services.AddSingleton<DownloadTab_VM>();
                    services.AddSingleton<JournalTab_VM>();
                    services.AddSingleton<NotificationPanel_VM>();

                    services.AddSingleton<IDownloader, SmallFilesDownloader>();
                    services.AddSingleton<FileNamesDistributor>();

                    services.AddSingleton<IInfoCollector, InfoCollector>();

                    services.AddSingleton<IJournal, Journal>();
                    services.AddSingleton<IJournalRepository, JournalRepository>();
                    services.AddDbContext<FtpDownloaderDbContext>();
                    services.AddSingleton<DbContextOptions>(new DbContextOptionsBuilder()
                        .UseSqlite(context.Configuration.GetConnectionString("sqliteConnectionString"))
                        .Options);

                    services.AddSingleton<UI.DataSources.Mappers.DownloadDtoToEntryDtoMapper>();
                    services.AddSingleton<UI.DataSources.Mappers.LogicLayerMapper>();
                    services.AddSingleton<Services.Mappers.LogicLayerMapper>();
                    services.AddSingleton<Services.Mappers.DataLayerMapper>();
                    services.AddSingleton<DataAccess.Mappers.DataLayerMapper>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.StopAsync();
            _host.Dispose();

            base.OnExit(e);
        }




        private void AddTestInfoCollectorWithLogger(IServiceCollection services)
        {
            services.AddSingleton<IInfoCollector, Test_InfoCollectorWithLogger>();
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
