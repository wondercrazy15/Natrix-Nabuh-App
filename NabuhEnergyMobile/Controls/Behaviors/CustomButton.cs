using NabuhEnergyMobile.Utils.Enums;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Controls.Behaviors
{
    public class CustomButton : Button
    {
        public static BindableProperty IsSelectedEnumProperty =
            BindableProperty.Create(nameof(IsSelectedEnum), typeof(TopupPaymentEnum), typeof(CustomButton), default(TopupPaymentEnum),
                                    BindingMode.TwoWay, propertyChanged: null);

        public static BindableProperty IsSelectedProperty =
            BindableProperty.Create(nameof(IsSelected), typeof(bool), typeof(CustomButton), default(bool),
                                    BindingMode.OneWay, propertyChanged: IsSelectedStateChanged);

        public static BindableProperty EnabledColorProperty =
            BindableProperty.Create(nameof(EnabledColor), typeof(Color), typeof(CustomButton), default(Color),
                                    BindingMode.TwoWay);// propertyChanged: IsSelectedStateChanged);

        public static readonly BindableProperty ClickedBackgroundColorProperty =
            BindableProperty.Create(
                "ClickedBackgroundColor", typeof(Color), typeof(CustomButton),
                defaultValue: Color.FromRgb(76, 175, 80));


        public TopupPaymentEnum IsSelectedEnum
        {
            get { return (TopupPaymentEnum)GetValue(ClickedBackgroundColorProperty); }
            set { SetValue(ClickedBackgroundColorProperty, value); }
        }

        public Color EnabledColor
        {
            get { return (Color)GetValue(ClickedBackgroundColorProperty); }
            set { SetValue(ClickedBackgroundColorProperty, value); }
        }

        public Color ClickedBackgroundColor
        {
            get { return (Color)GetValue(ClickedBackgroundColorProperty); }
            set { SetValue(ClickedBackgroundColorProperty, value); }
        }

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public CustomButton()
        {

        }

        private static void IsSelectedStateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (CustomButton)bindable;

            if (control.IsSelected)
            {
                control.BackgroundColor = Color.FromHex("#B1D237");

                return;
            }

           

            control.BackgroundColor = control.EnabledColor; //Color.FromHex("#01ACDD");
        }
    }
}
