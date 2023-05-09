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
                         To = "My computer",
                          OnPause = true,
                           Cancelling = false
                },
                new Download_VM
                {
                     DownloadedMegaBytes = 40000,
                      From = "Some site",
                       Name = "Something",
                        Size = 40001,
                         To = "My computer",
                         OnPause = true,
                           Cancelling = false
                },
                new Download_VM
                {
                     DownloadedMegaBytes = 1,
                      From = "Some site",
                       Name = "Something",
                        Size = 2,
                         To = "My computer",
                         OnPause = true,
                           Cancelling = false
                },
                new Download_VM
                {
                     DownloadedMegaBytes = 125346,
                      From = "Some site",
                       Name = "Something",
                        Size = 1257543235,
                         To = "My computer",
                         OnPause = true,
                           Cancelling = false
                }
            };

            downloader.DownloadFailed += OnDownloadFailed;
            downloader.DownloadCancelled += OnDownloadCancelled;
            downloader.DownloadCompleted += OnDownloadCompleted;
            downloader.DownloadedBytesNumberChanged += OnDownloadProgress;
            downloader.DownloadStarted += OnDownloadStarted;

            _resumeCommand = new AutoEventCommandBase(o => Resume(o), o => CanResume(o));
            _pauseCommand = new AutoEventCommandBase(o => Pause(o), o => CanPause(o));
            _cancelCommand = new AutoEventCommandBase(o => Cancel(o), o => CanCancel(o));
            _resumeAllCommand = new AutoEventCommandBase(_ => ResumeAll(), _ => CanResumeAll());
            _pauseAllCommand = new AutoEventCommandBase(_ => PauseAll(), _ => CanPauseAll());
            _cancelAllCommand = new AutoEventCommandBase(_ => CancelAll(), _ => CanCancelAll());
        }

        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public IDownloader Downloader => _downloader;

        public ObservableCollection<Download_VM> Downloads { get; set; }
        public AutoEventCommandBase ResumeCommand => _resumeCommand;
        public AutoEventCommandBase PauseCommand => _pauseCommand;
        public AutoEventCommandBase CancelCommand => _cancelCommand;
        public AutoEventCommandBase ResumeAllCommand => _resumeAllCommand;
        public AutoEventCommandBase PauseAllCommand => _pauseAllCommand;
        public AutoEventCommandBase CancelAllCommand => _cancelAllCommand;





        public void StartNewDownload(Download_VM download)
        {
            //_downloader.StartNewDownload();
        }

        private void OnDownloadStarted(DownloadModel obj)
        {
            _notificationPanel.AddPositiveNotification("Download has atarted!");
        }

        private void OnDownloadProgress(DownloadModel obj)
        {

        }

        private void OnDownloadCompleted(DownloadModel obj)
        {

        }

        private void OnDownloadCancelled(DownloadModel obj)
        {

        }

        private void OnDownloadFailed(DownloadModel arg1, Exception arg2)
        {

        }





        private bool CanResume(object o) => (o as DownloadModel).OnPause;
        private void Resume(object o) => (o as DownloadModel).OnPause = false;

        private bool CanPause(object o) => !(o as DownloadModel).OnPause;
        private void Pause(object o) => (o as DownloadModel).OnPause = true;

        private bool CanCancel(object o) => true;
        private void Cancel(object o) => (o as DownloadModel).Cancelling = true;

        private bool CanResumeAll() => Downloads.Any(d => d.OnPause);
        private void ResumeAll()
        {
            foreach (var download in Downloads)
            {
                if (download.OnPause) download.OnPause = false;
            }
        }

        private bool CanPauseAll() => Downloads.Any(d => !d.OnPause);
        private void PauseAll()
        {
            foreach (var download in Downloads)
            {
                if (!download.OnPause) download.OnPause = true;
            }
        }

        private bool CanCancelAll() => Downloads.Any();
        private void CancelAll()
        {
            foreach (var download in Downloads)
            {
                if (!download.Cancelling) download.Cancelling = true;
            }
        }
    }
}
