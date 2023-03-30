using System.Windows;

namespace File_downloader.ResourceAccess

{
    static class NotificationIconsManager
    {
        public static string PositiveIconUri => Application.Current.Resources["CheckMarkCircle"] as string ?? "Not found";
        public static string NeutralIconUri => Application.Current.Resources["EmptyCircle"] as string ?? "Not found";
        public static string NegativeIconUri => Application.Current.Resources["CrossCircle"] as string ?? "Not found";
    }
}
