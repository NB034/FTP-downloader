using File_downloader.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace File_downloader.ViewModels
{
    // First tab
    partial class MainWindowViewModel
    {

    }

    // Second tab
    partial class MainWindowViewModel
    {

    }

    // Notification panel
    partial class MainWindowViewModel
    {
        private readonly AutoEventCommandBase closeNotificatonCommand;
        private readonly AutoEventCommandBase closeAllNotificatonsCommand;

        public AutoEventCommandBase CloseNotificatonCommand => closeNotificatonCommand;
        public AutoEventCommandBase CloseAllNotificationsCommand => closeAllNotificatonsCommand;
        public ObservableCollection<Notification> Notifications { get; set; }

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
                ImageUri = "",
                Message = message,
            });
        }

        public void AddNegativeNotification(Exception exception)
        {
            Notifications.Add(new Notification
            {
                ImageUri = "",
                Message= exception.Message,
            });
        }
    }
}
