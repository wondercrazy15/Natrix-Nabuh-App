using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class HistoryItemPage : ContentPage
    {
        private readonly HistoryItemViewModel historyItemViewModel;

        public HistoryItemPage()
        {
            InitializeComponent();

            base.BindingContext = historyItemViewModel = new HistoryItemViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
