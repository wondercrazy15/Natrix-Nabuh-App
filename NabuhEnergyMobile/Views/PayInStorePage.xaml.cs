using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class PayInStorePage : ContentPage
    {
        private readonly PayInStoreViewModel payInStoreViewModel;

        public PayInStorePage()
        {
            InitializeComponent();

           

            BindingContext = payInStoreViewModel = new PayInStoreViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
