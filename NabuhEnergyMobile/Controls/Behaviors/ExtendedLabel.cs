using Xamarin.Forms;

namespace NabuhEnergyMobile.Controls.Behaviors
{
    public class ExtendedLabel : Label
    {
        public static BindableProperty IsDefaultCardProperty =
            BindableProperty.Create(nameof(IsDefaultCard), typeof(bool), typeof(ExtendedLabel), default(bool),
                                    BindingMode.OneWay);

        public bool IsDefaultCard
        {
            get { return (bool)GetValue(IsDefaultCardProperty); }
            set { SetValue(IsDefaultCardProperty, value); }
        }
    }
}

