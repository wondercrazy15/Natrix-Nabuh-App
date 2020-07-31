using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class LiveChatPage : ContentPage
    {
        public LiveChatPage()
        {
            try
            {
                InitializeComponent();
                if (Device.RuntimePlatform == Device.iOS)
                {
                    labelLoading.IsVisible = false;
                }
                
                chatwebview.Url = "http://nabuhchat-dev.keyivr.co.uk";
            }
            catch (Exception)
            {

            }
           
        }

        private void WebviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        private void WebviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }
    }
}
