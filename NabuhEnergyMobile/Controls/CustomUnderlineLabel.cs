using System;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Controls
{
	public class CustomUnderlineLabel : Label
	{
		public static readonly BindableProperty IsUnderlinedProperty = BindableProperty.Create("IsUnderlined", typeof(bool), typeof(CustomUnderlineLabel), false);

		public bool IsUnderlined
		{
			get { return (bool)GetValue(IsUnderlinedProperty); }
			set { SetValue(IsUnderlinedProperty, value); }
		}
	}
}
