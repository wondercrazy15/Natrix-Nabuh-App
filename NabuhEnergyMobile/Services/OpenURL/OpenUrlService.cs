using System;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Services.OpenURL
{
    public class OpenUrlService : IOpenUrlService
    {
        public async void OpenUrl(string url)
        {
           await Launcher.OpenAsync(new Uri(url));
        }
    }
}
