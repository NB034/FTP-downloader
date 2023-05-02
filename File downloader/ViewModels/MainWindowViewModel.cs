using File_downloader.Command;
using File_downloader.Resources.ResourcesAccess;
using FileDownloader.Services.Mappers;
using FileDownloader.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    // Common
    partial class MainWindowViewModel
    {
        private readonly Journal _journal;
        private readonly Downloader _downloader;

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
