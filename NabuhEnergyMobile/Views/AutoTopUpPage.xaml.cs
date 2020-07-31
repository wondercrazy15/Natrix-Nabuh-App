using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class AutoTopUpPage : ContentPage
    {
        private readonly AutoTopUpViewModel autoTopUpViewModel;

        public AutoTopUpPage()
        {
            InitializeComponent();

            BindingContext = autoTopUpViewModel = new AutoTopUpViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
