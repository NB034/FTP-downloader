using FtpDownloader.UI.DataSources.Command;

namespace FtpDownloader.UI.DataSources.ViewModels
{
    public class MainWindow_VM
    {
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly DownloadTab_VM _downloadTab;
        private readonly JournalTab_VM _journalTab;

        private readonly CustomizableCommand _onStartingCommand;
        private readonly CustomizableCommand _onClosingCommand;

        public MainWindow_VM(NotificationPanel_VM notificationPanel, DownloadTab_VM downloadTab, JournalTab_VM journalTab)
        {
            _notificationPanel = notificationPanel;
            _downloadTab = downloadTab;
            _journalTab = journalTab;

            _onStartingCommand = new CustomizableCommand(_ => OnStarting(), _ => true);
            _onClosingCommand = new CustomizableCommand(_ => OnClosing(), _ => true);
        }




        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public DownloadTab_VM DownloadTab => _downloadTab;
        public JournalTab_VM JournalTab => _journalTab;

        public CustomizableCommand OnStartingCommand => _onStartingCommand;
        public CustomizableCommand OnClosingCommand => _onClosingCommand;




        private void OnStarting()
        {
            _notificationPanel.AddPositiveNotification("Program loaded!");
        }

        private async void OnClosing()
        {
            _notificationPanel.AddNeutralNotification("Cancelling...");
            await _downloadTab.DownloadList.Downloader.FinalizeDownloads();    
        }
    }
}
