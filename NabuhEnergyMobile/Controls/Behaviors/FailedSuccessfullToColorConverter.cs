using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Controls.Behaviors
{
    public class FailedSuccessfullToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (!string.Equals(value?.ToString().ToLower(), "success"))
            {
                return Color.Red;
            }

            return Color.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            // You probably don't need this, this is used to convert the other way around
            // so from color to yes no or maybe
            throw new NotImplementedException();
        }
    }
}
