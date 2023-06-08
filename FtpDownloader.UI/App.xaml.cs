using FtpDownloader.Services.Accessories;
using FtpDownloader.Services.Interfaces.Models;
using FluentFTP;
using System;
using System.IO;
using System.Windows;
using FtpDownloader.Services.TestModels;
using FtpDownloader.Services.Models;
using FtpDownloader.Services.Mappers;
using FtpDownloader.UI.DataSources.ViewModels;
using FtpDownloader.UI.Views;

namespace FtpDownloader.UI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            
            LogicLayerMapper logicLayerMapper = new LogicLayerMapper();

            IInfoCollector infoCollector = new TestInfoCollector(logicLayerMapper); //ConstructInfoCollector();
            IDownloader downloader = new TestDownloader(logicLayerMapper);
            IJournal journal = new TestJournal(logicLayerMapper);

            NotificationPanel_VM notificationPanel = new NotificationPanel_VM();
            DownloadList_VM downloadList = new DownloadList_VM(notificationPanel, downloader);
            DownloadTab_VM downloadTab = new DownloadTab_VM(notificationPanel, downloadList, infoCollector);
            JournalTab_VM journalTab = new JournalTab_VM(journal, downloader, notificationPanel);

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
