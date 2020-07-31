using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NabuhEnergyMobile.Models.History;
using NabuhEnergyMobile.ViewModels;
using NabuhEnergyMobile.Views.Base;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class HistoryPage : BaseTabView
    {
        private readonly HistoryViewModel historyViewModel;

        public HistoryPage()
        {
            InitializeComponent();

            BindingContext = historyViewModel = new HistoryViewModel();
        }


        async void ListItemClick(object sender, SelectedItemChangedEventArgs args)
        {
            var selectedId = (args.SelectedItem as PaymentHistory).Id;

            await historyViewModel.DisplayReceipt(selectedId);
        }

    }
}
