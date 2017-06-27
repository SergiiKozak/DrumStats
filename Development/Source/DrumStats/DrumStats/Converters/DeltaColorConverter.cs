using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace DrumStats.Converters
{
    public class DeltaColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {

            var delta = value is decimal ? (decimal)value : (int)value;

            if (delta > 0)
                return Color.Green;
            else if (delta < 0)
                return Color.Red;
            else return Color.Default;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
