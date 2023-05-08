using File_downloader.Command;
using File_downloader.ViewModels.DataViewModels;
using FileDownloader.Services.Mappers;
using FileDownloader.Services.Models.DownloaderModels;
using FileDownloader.Services.Models.JournalModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    internal class DownloadList_VM
    {
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly IDownloader _downloader;
        private readonly IJournal _journal;

        private readonly AutoEventCommandBase _resumeCommand;
        private readonly AutoEventCommandBase _pauseCommand;
        private readonly AutoEventCommandBase _cancelCommand;
        private readonly AutoEventCommandBase _resumeAllCommand;
        private readonly AutoEventCommandBase _pauseAllCommand;
        private readonly AutoEventCommandBase _cancelAllCommand;

        public DownloadList_VM(NotificationPanel_VM notificationPanel, IDownloader downloader)
        {
            _notificationPanel = notificationPanel;
            _downloader = downloader;

            //Downloads = new ObservableCollection<DownloadViewModel>();

            Downloads = new ObservableCollection<Download_VM>
            {
                new Download_VM
                {
                     DownloadedMegaBytes = 40000,
                      From = "Some site",
                       Name = "Something",
                        Size = 200000,
                         To = "My computer"
                },
                new Download_VM
                {
                     DownloadedMegaBytes = 40000,
                      From = "Some site",
                       Name = "Something",
                        Size = 40001,
                         To = "My computer"
                },
                new Download_VM
                {
                     DownloadedMegaBytes = 1,
                      From = "Some site",
                       Name = "Something",
                        Size = 2,
                         To = "My computer"
                },
                new Download_VM
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

        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public IDownloader Downloader => _downloader;
        public IJournal Journal => _journal;


        public ObservableCollection<Download_VM> Downloads { get; set; }
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
