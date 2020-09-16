using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.ViewModels;
using NabuhEnergyMobile.Views.Base;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class MyAccountPage : BaseTabView
    {
        private readonly MyAccountViewModel myAccountViewModel;

        bool isClick;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            isClick = false;
        }

        public MyAccountPage()
        {
            try
            {
                InitializeComponent();

                BindingContext = myAccountViewModel = new MyAccountViewModel();
            }
            catch (Exception ex)
            {

            }
        }

        async void LogoutButton_Clicked(object sender, EventArgs e)
        {
            if (myAccountViewModel.IsLogoutClick==false)
            {
                myAccountViewModel.IsLogoutClick = true;
                await myAccountViewModel.SingOutAsync();
            }
        }

        async void TopUpCommand(object sender, EventArgs e)
        {
            if (isClick == false)
            {
                isClick = true;
                await myAccountViewModel.LoadTopUp();
            }
        }

        async void AutoTopUpCommand(object sender, EventArgs e) {
            if (isClick == false)
            {
                isClick = true;
                await myAccountViewModel.LoadAutoTopUp();
            }
        }
    }
}
