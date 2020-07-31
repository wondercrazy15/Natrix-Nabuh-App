using System;
using System.Globalization;
using NabuhEnergyMobile.Utils.Enums;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Utils.Converters
{
	public class TopupEnumToBoolConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var current = (TopupPaymentEnum)value;

			var obj = (TopupPaymentEnum)parameter;

			return current == obj;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}

