using System;
using System.Collections.Generic;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class TopupProcessingPage : ContentPage
    {
        private readonly TopupProcessingViewModel topupProcessingViewModel;

        public TopupProcessingPage()
        {
            InitializeComponent();

            BindingContext = topupProcessingViewModel = new TopupProcessingViewModel();
        }

       
    }
}
