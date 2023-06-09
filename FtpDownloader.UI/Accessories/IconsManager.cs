﻿using FtpDownloader.UI.DataSources.Accessories;
using System.Windows;
using System.Windows.Media;

namespace File_downloader.Accessories

{
    static class IconsManager
    {
        public static ImageBrush PositiveIcon => Application.Current.Resources["CheckMarkCircle"] as ImageBrush ?? new ImageBrush();
        public static ImageBrush NeutralIcon => Application.Current.Resources["EmptyCircle"] as ImageBrush ?? new ImageBrush();
        public static ImageBrush NegativeIcon => Application.Current.Resources["CrossCircle"] as ImageBrush ?? new ImageBrush();

        public static ImageBrush GetIcon(NotificationTypesEnum type) =>
            type switch
            {
                NotificationTypesEnum.Negative => NegativeIcon,
                NotificationTypesEnum.Positive => PositiveIcon,
                NotificationTypesEnum.Neutral => NeutralIcon,
                _ => new ImageBrush()
            };
    }
}
