using File_downloader.Command;

namespace File_downloader.ViewModels
{
    partial class MainWindow_VM
    {
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly DownloadTab_VM _downloadTab;
        private readonly JournalTab_VM _journalTab;

        private readonly AutoEventCommandBase _onStartingCommand;
        private readonly AutoEventCommandBase _onClosingCommand;

        public MainWindow_VM(NotificationPanel_VM notificationPanel, DownloadTab_VM downloadTab, JournalTab_VM journalTab)
        {
            _notificationPanel = notificationPanel;
            _downloadTab = downloadTab;
            _journalTab = journalTab;

            _onStartingCommand = new AutoEventCommandBase(_ => OnStarting(), _ => true);
            _onClosingCommand = new AutoEventCommandBase(_ => OnClosing(), _ => true);
        }




        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public DownloadTab_VM DownloadTab => _downloadTab;
        public JournalTab_VM JournalTab => _journalTab;

        public AutoEventCommandBase OnStartingCommand => _onStartingCommand;
        public AutoEventCommandBase OnClosingCommand => _onClosingCommand;




        private void OnStarting()
        {
            _notificationPanel.AddPositiveNotification("Program loaded!");
        }

        private void OnClosing()
        {
            _notificationPanel.AddNeutralNotification("All unfinished downloads cancelled!");
        }
    }
}
