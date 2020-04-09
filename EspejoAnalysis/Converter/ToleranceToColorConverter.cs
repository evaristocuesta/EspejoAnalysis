using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EspejoAnalysis.Converter
{
    public class ToleranceToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? Brushes.LimeGreen : Brushes.Red;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
