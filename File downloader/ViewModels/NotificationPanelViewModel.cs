using File_downloader.Command;
using File_downloader.Resources.ResourceAccess;
using File_downloader.Resources.ResourcesAccess;
using File_downloader.ViewModels.DataViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace File_downloader.ViewModels
{
    internal class NotificationPanelViewModel
    {
        private readonly AutoEventCommandBase _closeNotificatonCommand;
        private readonly AutoEventCommandBase _closeAllNotificatonsCommand;

        public NotificationPanelViewModel()
        {
            //Notifications = new ObservableCollection<NotificationViewModel>();
            _closeNotificatonCommand = new AutoEventCommandBase(o => RemoveNotification(o), _ => true);
            _closeAllNotificatonsCommand = new AutoEventCommandBase(_ => ClearNotifications(), _ => true);

            Notifications = new ObservableCollection<NotificationViewModel>
            {
                new NotificationViewModel
                {
                     Image = IconsManager.PositiveIcon,
                      Message = "Program loaded!"
                }
            };

            Notifications.CollectionChanged += CheckForOverflow;
        }

        public AutoEventCommandBase CloseNotificatonCommand => _closeNotificatonCommand;
        public AutoEventCommandBase CloseAllNotificationsCommand => _closeAllNotificatonsCommand;
        public ObservableCollection<NotificationViewModel> Notifications { get; set; }
        public int MaxNotifications { get; set; } = 100;

        private void RemoveNotification(object o)
        {
            var notification = (NotificationViewModel)o;
            Notifications.Remove(notification);
        }

        private void ClearNotifications()
        {
            Notifications.Clear();
        }

        public void AddPositiveNotification(string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.PositiveIcon,
                Message = message,
            });
        }

        public void AddNeutralNotification(string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.NeutralIcon,
                Message = message,
            });
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.NegativeIcon,
                Message = exception.Message,
            });
        }

        public void AddNotification(NotificationTypesEnum type, string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.GetIcon(type),
                Message = message
            });
        }

        private void CheckForOverflow(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Notifications.Count > MaxNotifications)
            {
                Notifications.Remove(Notifications.First());
            }
        }
    }
}
