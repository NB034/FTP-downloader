using File_downloader.ViewModels;
using FileDownloader.Services.Models.DownloaderModels;
using FileDownloader.Services.Models.JournalModels;
using System.Windows;

namespace File_downloader
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            IDownloader downloader = new TestDownloader();
            IJournal journal = new TestJournal();

            NotificationPanel_VM notificationPanel = new NotificationPanel_VM();
            DownloadList_VM downloadList = new DownloadList_VM(notificationPanel, downloader);
            DownloadTab_VM downloadTab = new DownloadTab_VM(notificationPanel, downloadList);
            JournalTab_VM journalTab = new JournalTab_VM(journal, downloader, notificationPanel);

            MainWindow_VM mainViewModel = new MainWindow_VM(notificationPanel, downloadTab, journalTab);
            MainWindow window = new MainWindow { DataContext = mainViewModel };
            window.Show();
        }
    }
}
