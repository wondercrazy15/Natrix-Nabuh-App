using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views.Base
{
    public class BaseTabView : ContentPage
    {
        public async void InitViewModel()
        {
            await(BindingContext as BaseViewModel)?.InitializeAsync();
        }
    }
}
