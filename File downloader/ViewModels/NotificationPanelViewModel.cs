using File_downloader.Command;
using File_downloader.Resources.ResourcesAccess;
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

            CheckPanelOverflow();
        }

        public void AddNeutralNotification(string message)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.NeutralIcon,
                Message = message,
            });

            CheckPanelOverflow();
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new NotificationViewModel
            {
                Image = IconsManager.NegativeIcon,
                Message = exception.Message,
            });

            CheckPanelOverflow();
        }

        private void CheckPanelOverflow()
        {
            if (Notifications.Count > MaxNotifications)
            {
                Notifications.Remove(Notifications.First());
            }
        }
    }
}
