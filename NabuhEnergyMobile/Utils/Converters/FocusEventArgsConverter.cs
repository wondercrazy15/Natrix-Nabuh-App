using System;
using System.Globalization;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Utils.Converters
{
	public class FocusEventArgsConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var eventArgs = value as FocusEventArgs;

			if (eventArgs == null)
			{
				throw new ArgumentException("Expected FocusEventArgs as value", "value");
			}

			return eventArgs.IsFocused;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
