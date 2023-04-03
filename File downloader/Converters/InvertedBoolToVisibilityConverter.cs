using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace File_downloader.Converters
{
    internal class InvertedBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converter = new BooleanToVisibilityConverter();
            return converter.Convert(!(bool)value, null, null, null);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new();
        }
    }
}
