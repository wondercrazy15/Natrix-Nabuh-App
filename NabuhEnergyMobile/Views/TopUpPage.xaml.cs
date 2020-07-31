using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class TopUpPage : ContentPage
    {
        private readonly TopUpViewModel topUpViewModel;

        public TopUpPage()
        {
            InitializeComponent();

            BindingContext = topUpViewModel = new TopUpViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
