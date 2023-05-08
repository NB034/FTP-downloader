using File_downloader.Command;
using FileDownloader.Services.Models.DownloaderModels;
using FileDownloader.Services.Models.JournalModels;

namespace File_downloader.ViewModels
{
    partial class MainWindowViewModel
    {
        private readonly IJournal _journal;
        private readonly IDownloader _downloader;

        private readonly NotificationPanelViewModel _notificationPanel;
        private readonly DownloadTabViewModel _downloadTab;
        private readonly JournalTabViewModel _journalTab;

        private readonly AutoEventCommandBase _onStartingCommand;
        private readonly AutoEventCommandBase _onClosingCommand;

        public MainWindowViewModel()
        {
            _journal = new Journal();
            _downloader = new Downloader();

            _notificationPanel= new NotificationPanelViewModel();
            _downloadTab = new DownloadTabViewModel(_downloader,_notificationPanel, _journal);
            _journalTab = new JournalTabViewModel(_journal, _notificationPanel);

            _onStartingCommand = new AutoEventCommandBase(_ => OnStarting(), _ => true);
            _onClosingCommand = new AutoEventCommandBase(_ => OnClosing(), _ => true);
        }




        public NotificationPanelViewModel NotificationPanel => _notificationPanel;
        public DownloadTabViewModel DownloadTab => _downloadTab;
        public JournalTabViewModel JournalTab => _journalTab;

        public AutoEventCommandBase OnStartingCommand => _onStartingCommand;
        public AutoEventCommandBase OnClosingCommand => _onClosingCommand;




        private void OnStarting()
        {
            
        }

        private void OnClosing()
        {
            
        }
    }
}
