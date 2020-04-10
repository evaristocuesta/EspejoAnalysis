using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace EspejoAnalysis.Converter
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((bool)value) ? ColorTrue : ColorFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public Brush ColorTrue { get; set; }

        public Brush ColorFalse { get; set; }
    }
}
