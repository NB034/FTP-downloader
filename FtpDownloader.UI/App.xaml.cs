using FtpDownloader.Services.Accessories;
using FtpDownloader.Services.Interfaces.Models;
using FluentFTP;
using System;
using System.IO;
using System.Windows;
using FtpDownloader.Services.TestModels;
using FtpDownloader.Services.Models;
using FtpDownloader.UI.DataSources.ViewModels;
using FtpDownloader.UI.Windows;
using FtpDownloader.UI.DataSources.Mappers;

namespace FtpDownloader.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Services.Mappers.LogicLayerMapper servicesLogicLayerMapper = new Services.Mappers.LogicLayerMapper();
            DataSources.Mappers.LogicLayerMapper dataSourcesLogicLayerMapper = new DataSources.Mappers.LogicLayerMapper();
            DownloadDtoToEntryDtoMapper dtoMapper = new DownloadDtoToEntryDtoMapper();

            IInfoCollector infoCollector = new TestInfoCollector(servicesLogicLayerMapper); //ConstructInfoCollector();
            IDownloader downloader = new TestDownloader(servicesLogicLayerMapper);
            IJournal journal = new TestJournal(servicesLogicLayerMapper);

            NotificationPanel_VM notificationPanel = new NotificationPanel_VM();
            DownloadList_VM downloadList = new DownloadList_VM(notificationPanel, downloader, dtoMapper);
            DownloadTab_VM downloadTab = new DownloadTab_VM(notificationPanel, downloadList, infoCollector);
            JournalTab_VM journalTab = new JournalTab_VM(journal, downloader, notificationPanel, dataSourcesLogicLayerMapper);

            MainWindow_VM mainViewModel = new MainWindow_VM(notificationPanel, downloadTab, journalTab);
            MainWindow window = new MainWindow { DataContext = mainViewModel };
            window.Show();
        }

        protected IInfoCollector ConstructInfoCollector()
        {
            string fileName = "Log.txt";
            var folder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string path = Path.Combine(folder, fileName);

            IUniversalLogger logger = new FtpTxtLogger(path);

            var config = new FtpConfig
            {
                LogHost = true,
                LogUserName = true,
                LogPassword = true,
            };

            IInfoCollector infoCollector = new InfoCollector(logger, config);

            return infoCollector;
        }
    }
}
