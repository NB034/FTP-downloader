using FtpDownloader.Resources.ResourceAccess;
using System;
using System.Globalization;
using System.Windows.Data;

namespace FtpDownloader.Converters
{
    public class EnumToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var notificcationType = (NotificationTypesEnum)value;
            return IconsManager.GetIcon(notificcationType);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new();
        }
    }
}
