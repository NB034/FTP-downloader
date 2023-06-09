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

namespace FtpDownloader.UI
{
    public partial class App : Application
    {
        private readonly IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
            .ConfigureServices((services) =>
            {
                services.AddSingleton<MainWindow>();

                services.AddSingleton<MainWindow_VM>();
                services.AddSingleton<DownloadList_VM>();
                services.AddSingleton<DownloadTab_VM>();
                services.AddSingleton<JournalTab_VM>();
                services.AddSingleton<NotificationPanel_VM>();

                services.AddSingleton<IDownloader, TestDownloader>();
                services.AddSingleton<IInfoCollector, TestInfoCollector>();
                services.AddSingleton<IJournal, TestJournal>();

                services.AddSingleton<IJournalRepository, JournalRepository>();

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









        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    base.OnStartup(e);

        //    Services.Mappers.LogicLayerMapper servicesLogicLayerMapper = new Services.Mappers.LogicLayerMapper();
        //    DataSources.Mappers.LogicLayerMapper dataSourcesLogicLayerMapper = new DataSources.Mappers.LogicLayerMapper();
        //    DownloadDtoToEntryDtoMapper dtoMapper = new DownloadDtoToEntryDtoMapper();

        //    IInfoCollector infoCollector = new TestInfoCollector(servicesLogicLayerMapper); //ConstructInfoCollector();
        //    IDownloader downloader = new TestDownloader(servicesLogicLayerMapper);
        //    IJournal journal = new TestJournal(servicesLogicLayerMapper);

        //    NotificationPanel_VM notificationPanel = new NotificationPanel_VM();
        //    DownloadList_VM downloadList = new DownloadList_VM(notificationPanel, downloader, dataSourcesLogicLayerMapper);
        //    DownloadTab_VM downloadTab = new DownloadTab_VM(notificationPanel, downloadList, infoCollector);
        //    JournalTab_VM journalTab = new JournalTab_VM(journal, downloader, notificationPanel, dataSourcesLogicLayerMapper, dtoMapper);

        //    MainWindow_VM mainViewModel = new MainWindow_VM(notificationPanel, downloadTab, journalTab);
        //    MainWindow window = new MainWindow(mainViewModel);
        //    window.Show();
        //}

        //protected IInfoCollector ConstructInfoCollector()
        //{
        //    string fileName = "Log.txt";
        //    var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        //    string path = Path.Combine(folder, fileName);

        //    IUniversalLogger logger = new FtpTxtLogger(path);

        //    var config = new FtpConfig
        //    {
        //        LogHost = true,
        //        LogUserName = true,
        //        LogPassword = true,
        //    };

        //    IInfoCollector infoCollector = new InfoCollector(logger, config);

        //    return infoCollector;
        //}
    }
}
