using FtpDownloader.Command;
using System.Linq;
using System.Threading.Tasks;

namespace FtpDownloader.ViewModels
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

        private async void OnClosing()
        {
            _notificationPanel.AddNeutralNotification("Cancelling...");
            var command = _downloadTab.DownloadList.CancelAllCommand;
            if (command.CanExecute(new()))
            {
                command.Execute(new());
                while (_downloadTab.DownloadList.Downloader.Downloads.Any()) { await Task.Delay(2000); }
            }
        }
    }
}
