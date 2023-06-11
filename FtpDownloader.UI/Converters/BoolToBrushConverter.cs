using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace FtpDownloader.UI.Converters
{
    class BoolToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var isActive = (bool)value;
            return isActive
                ? Application.Current.Resources["MainBorderBrush"]
                : Application.Current.Resources["UnavailableElementBrush"];
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new object();
        }
    }
}
