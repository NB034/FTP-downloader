using File_downloader.Command;
using FileDownloader.Services.Mappers;
using FileDownloader.Services.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    internal class DownloadListViewModel
    {
        private readonly NotificationPanelViewModel _notificationPanel;
        private readonly Downloader _downloader;
        private readonly Journal _journal;

        private readonly AutoEventCommandBase _resumeCommand;
        private readonly AutoEventCommandBase _pauseCommand;
        private readonly AutoEventCommandBase _cancelCommand;
        private readonly AutoEventCommandBase _resumeAllCommand;
        private readonly AutoEventCommandBase _pauseAllCommand;
        private readonly AutoEventCommandBase _cancelAllCommand;

        public DownloadListViewModel(NotificationPanelViewModel notificationPanel, Downloader downloader, Journal journal)
        {
            _notificationPanel = notificationPanel;
            _downloader = downloader;
            _journal = journal;

            //Downloads = new ObservableCollection<DownloadViewModel>();

            Downloads = new ObservableCollection<DownloadViewModel>
            {
                new DownloadViewModel
                {
                     DownloadedMegaBytes = 40000,
                      From = "Some site",
                       Name = "Something",
                        Size = 200000,
                         To = "My computer"
                },
                new DownloadViewModel
                {
                     DownloadedMegaBytes = 40000,
                      From = "Some site",
                       Name = "Something",
                        Size = 40001,
                         To = "My computer"
                },
                new DownloadViewModel
                {
                     DownloadedMegaBytes = 1,
                      From = "Some site",
                       Name = "Something",
                        Size = 2,
                         To = "My computer"
                },
                new DownloadViewModel
                {
                     DownloadedMegaBytes = 125346,
                      From = "Some site",
                       Name = "Something",
                        Size = 1257543235,
                         To = "My computer"
                }
            };

            downloader.DownloadFailed += OnDownloadFailed;
            downloader.DownloadCancelled += OnDownloadCancelled;
            downloader.DownloadCompleted += OnDownloadCompleted;
            downloader.DownloadedBytesNumberChanged += OnDownloadProgress;
            downloader.DownloadStarted += OnDownloadStarted;

           // _resumeCommand = new AutoEventCommandBase(o => Resume(o),);
        }

        public NotificationPanelViewModel NotificationPanel => _notificationPanel;
        public Downloader Downloader => _downloader;
        public Journal Journal => _journal;


        public ObservableCollection<DownloadViewModel> Downloads { get; set; }
        public AutoEventCommandBase ResumeCommand => _resumeCommand;
        public AutoEventCommandBase PauseCommand => _pauseCommand;
        public AutoEventCommandBase CancelCommand => _cancelCommand;
        public AutoEventCommandBase ResumeAllCommand => _resumeAllCommand;
        public AutoEventCommandBase PauseAllCommand => _pauseAllCommand;
        public AutoEventCommandBase CancelAllCommand => _cancelAllCommand;





        private void OnDownloadStarted(DownloadModel obj)
        {
            throw new NotImplementedException();
        }

        private void OnDownloadProgress(DownloadModel obj)
        {
            throw new NotImplementedException();
        }

        private void OnDownloadCompleted(DownloadModel obj)
        {
            throw new NotImplementedException();
        }

        private void OnDownloadCancelled(DownloadModel obj)
        {
            throw new NotImplementedException();
        }

        private void OnDownloadFailed(DownloadModel arg1, Exception arg2)
        {
            throw new NotImplementedException();
        }





        //private bool CanResume() =>
        private void Resume(object o)
        {

        }

        //private bool CanPause() =>
        private void Pause(object o)
        {

        }

        //private bool CanCancel() =>
        private void Cancel(object o)
        {

        }

        //private bool CanResumeAll() =>
        private void ResumeAll()
        {

        }

        //private bool CanPauseAll() =>
        private void PauseAll()
        {

        }

        //private bool CanCancelAll() =>
        private void CancelAll()
        {

        }
    }
}
