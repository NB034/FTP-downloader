using FtpDownloader.UI.DataSources.DataTypes;
using FtpDownloader.Services.Interfaces.DTO;
using FtpDownloader.Services.Interfaces.Models;
using System.Collections.ObjectModel;
using FtpDownloader.UI.DataSources.Mappers;
using FtpDownloader.UI.DataSources.Command;
using FtpDownloader.UI.DataSources.Accessories;

namespace FtpDownloader.UI.DataSources.ViewModels
{
    public class DownloadList_VM
    {
        private readonly NotificationPanel_VM _notificationPanel;
        private readonly LogicLayerMapper _mapper;
        private readonly IDownloader _downloader;

        private readonly CustomizableCommand _resumeCommand;
        private readonly CustomizableCommand _pauseCommand;
        private readonly CustomizableCommand _cancelCommand;
        private readonly CustomizableCommand _resumeAllCommand;
        private readonly CustomizableCommand _pauseAllCommand;
        private readonly CustomizableCommand _cancelAllCommand;

        public DownloadList_VM(NotificationPanel_VM notificationPanel, IDownloader downloader, LogicLayerMapper mapper)
        {
            _notificationPanel = notificationPanel;
            _mapper = mapper;
            _downloader = downloader;

            Downloads = new ObservableCollection<Download_VM>();

            downloader.DownloadFailed += OnDownloadFailed;
            downloader.DownloadCancelled += OnDownloadCancelled;
            downloader.DownloadCompleted += OnDownloadCompleted;
            downloader.DownloadProgressChanged += OnDownloadProgress;
            downloader.DownloadStarted += OnDownloadStarted;
            downloader.ExceptionThrowned += OnExceptionThrowned;

            _resumeCommand = new CustomizableCommand(o => Resume(o), o => CanResume(o));
            _pauseCommand = new CustomizableCommand(o => Pause(o), o => CanPause(o));
            _cancelCommand = new CustomizableCommand(o => Cancel(o), o => CanCancel(o));
            _resumeAllCommand = new CustomizableCommand(_ => ResumeAll(), _ => CanResumeAll());
            _pauseAllCommand = new CustomizableCommand(_ => PauseAll(), _ => CanPauseAll());
            _cancelAllCommand = new CustomizableCommand(_ => CancelAll(), _ => CanCancelAll());
        }

        public NotificationPanel_VM NotificationPanel => _notificationPanel;
        public IDownloader Downloader => _downloader;

        public ObservableCollection<Download_VM> Downloads { get; set; }
        public CustomizableCommand ResumeCommand => _resumeCommand;
        public CustomizableCommand PauseCommand => _pauseCommand;
        public CustomizableCommand CancelCommand => _cancelCommand;
        public CustomizableCommand ResumeAllCommand => _resumeAllCommand;
        public CustomizableCommand PauseAllCommand => _pauseAllCommand;
        public CustomizableCommand CancelAllCommand => _cancelAllCommand;





        public void StartNewDownload(Download_VM download)
        {
            _downloader.StartNewDownload(_mapper.DownloadToDto(download));
        }

        private void OnDownloadStarted(LogicLayerDownloadDto obj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Downloads.Add(_mapper.DtoToDownload(obj));
                _notificationPanel.AddPositiveNotification($"Download of {obj.Name} has started!");
                CustomizableCommand.RaiseCanExecuteChanged();
            });
        }

        private void OnDownloadProgress(LogicLayerDownloadDto obj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var download = Downloads.First(d => d.DownloadGuid == obj.DownloadGuid);
                download.DownloadedBytes = obj.DownloadedBytes;
            });
        }

        private void OnDownloadCompleted(LogicLayerDownloadDto obj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Downloads.Remove(Downloads.First(d => d.DownloadGuid == obj.DownloadGuid));
                _notificationPanel.AddPositiveNotification($"Download of {obj.Name} completed!");
            });
        }

        private void OnDownloadCancelled(LogicLayerDownloadDto obj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                Downloads.Remove(Downloads.First(d => d.DownloadGuid == obj.DownloadGuid));
                _notificationPanel.AddPositiveNotification($"Download of {obj.Name} cancelled!");
            });
        }

        private void OnDownloadFailed(LogicLayerDownloadDto arg1, Exception arg2)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                var download = Downloads.FirstOrDefault(d => d.DownloadGuid == arg1.DownloadGuid); 
                if(download != null) Downloads.Remove(download);
                _notificationPanel.AddNotification(NotificationTypesEnum.Negative, $"Download of {arg1.Name} failed: {arg2.Message}");
            });
        }

        private void OnExceptionThrowned(Exception obj)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {   
                _notificationPanel.AddNotification(NotificationTypesEnum.Negative, obj.Message);
            });
        }





        private bool CanResume(object o) => (o as Download_VM).OnPause;
        private void Resume(object o)
        {
            var vm = (Download_VM)o;
            vm.OnPause = false;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.Resume(vm.DownloadGuid);
        }

        private bool CanPause(object o) => !(o as Download_VM).OnPause;
        private void Pause(object o)
        {
            var vm = (Download_VM)o;
            vm.OnPause = true;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.Pause(vm.DownloadGuid);
        }

        private bool CanCancel(object o) => true;
        private void Cancel(object o)
        {
            var vm = (Download_VM)o;
            vm.Cancelling = true;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.Cancel(vm.DownloadGuid);
        }





        private bool CanResumeAll() => Downloads.Any(d => d.OnPause);
        private void ResumeAll()
        {
            foreach (var download in Downloads) if (download.OnPause) download.OnPause = false;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.ResumeAll();
        }

        private bool CanPauseAll() => Downloads.Any() && Downloads.Any(d => !d.OnPause);
        private void PauseAll()
        {
            foreach (var download in Downloads) if (!download.OnPause) download.OnPause = true;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.PauseAll();
        }

        private bool CanCancelAll() => Downloads.Any();
        private void CancelAll()
        {
            foreach (var download in Downloads) if (!download.Cancelling) download.Cancelling = true;
            CustomizableCommand.RaiseCanExecuteChanged();
            _downloader.CancelAll();
        }
    }
}
