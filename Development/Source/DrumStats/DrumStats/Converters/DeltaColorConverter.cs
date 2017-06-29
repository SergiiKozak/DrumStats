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

            if (delta >= (decimal)0.001)
                return Color.FromHex("13BA21");
            else if (delta <= (decimal)-0.001)
                return Color.FromHex("EB4F3D");
            else return Color.Transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
