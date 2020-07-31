using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class ForgottenPage : ContentPage
    {
        private readonly ForgottenViewModel forgottenViewModel;

        public ForgottenPage()
        {
            InitializeComponent();

            BindingContext = forgottenViewModel = new ForgottenViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
