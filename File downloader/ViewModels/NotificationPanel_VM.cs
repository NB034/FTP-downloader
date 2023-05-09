using File_downloader.Command;
using File_downloader.Resources.ResourceAccess;
using File_downloader.Resources.ResourcesAccess;
using File_downloader.ViewModels.DataViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace File_downloader.ViewModels
{
    internal class NotificationPanel_VM
    {
        private readonly AutoEventCommandBase _closeNotificationCommand;
        private readonly AutoEventCommandBase _closeAllNotificatonsCommand;

        public NotificationPanel_VM()
        {
            Notifications = new ObservableCollection<Notification_VM>();
            _closeNotificationCommand = new AutoEventCommandBase(o => RemoveNotification(o), _ => true);
            _closeAllNotificatonsCommand = new AutoEventCommandBase(_ => ClearNotifications(), _ => true);
        }





        public AutoEventCommandBase CloseNotificationCommand => _closeNotificationCommand;
        public AutoEventCommandBase CloseAllNotificationsCommand => _closeAllNotificatonsCommand;
        public ObservableCollection<Notification_VM> Notifications { get; set; }
        public int MaxNotifications { get; set; } = 50;





        private void RemoveNotification(object o)
        {
            var notification = (Notification_VM)o;
            Notifications.Remove(notification);
        }

        private void ClearNotifications()
        {
            Notifications.Clear();
        }

        public void AddPositiveNotification(string message)
        {
            Notifications.Add(new Notification_VM
            {
                Image = IconsManager.PositiveIcon,
                Message = message,
            });
            CheckForOverflow();
        }

        public void AddNeutralNotification(string message)
        {
            Notifications.Add(new Notification_VM
            {
                Image = IconsManager.NeutralIcon,
                Message = message,
            });
            CheckForOverflow();
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new Notification_VM
            {
                Image = IconsManager.NegativeIcon,
                Message = exception.Message,
            });
            CheckForOverflow();
        }

        public void AddNotification(NotificationTypesEnum type, string message)
        {
            Notifications.Add(new Notification_VM
            {
                Image = IconsManager.GetIcon(type),
                Message = message
            });
            CheckForOverflow();
        }

        private void CheckForOverflow()
        {
            if (Notifications.Count > MaxNotifications)
            {
                Notifications.Remove(Notifications.First());
            }
        }
    }
}
