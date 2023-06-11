using FtpDownloader.UI.DataSources.Accessories;
using FtpDownloader.UI.DataSources.Command;
using FtpDownloader.UI.DataSources.DataTypes;
using System.Collections.ObjectModel;

namespace FtpDownloader.UI.DataSources.ViewModels
{
    public class NotificationPanel_VM
    {
        private readonly CustomizableCommand _closeNotificationCommand;
        private readonly CustomizableCommand _closeAllNotificatonsCommand;

        public NotificationPanel_VM()
        {
            Notifications = new ObservableCollection<Notification>();
            _closeNotificationCommand = new CustomizableCommand(o => RemoveNotification(o), _ => true);
            _closeAllNotificatonsCommand = new CustomizableCommand(_ => ClearNotifications(), _ => true);
        }





        public CustomizableCommand CloseNotificationCommand => _closeNotificationCommand;
        public CustomizableCommand CloseAllNotificationsCommand => _closeAllNotificatonsCommand;
        public ObservableCollection<Notification> Notifications { get; set; }
        public int MaxNotifications { get; set; } = 50;





        private void RemoveNotification(object o)
        {
            var notification = (Notification)o;
            Notifications.Remove(notification);
        }

        private void ClearNotifications()
        {
            Notifications.Clear();
        }

        public void AddPositiveNotification(string message)
        {
            Notifications.Add(new Notification
            {
                Type = NotificationTypesEnum.Positive,
                Message = message,
            });
            CheckForOverflow();
        }

        public void AddNeutralNotification(string message)
        {
            Notifications.Add(new Notification
            {
                Type = NotificationTypesEnum.Neutral,
                Message = message,
            });
            CheckForOverflow();
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new Notification
            {
                Type = NotificationTypesEnum.Negative,
                Message = exception.Message,
            });
            CheckForOverflow();
        }

        public void AddNotification(NotificationTypesEnum type, string message)
        {
            Notifications.Add(new Notification
            {
                Type = type,
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
