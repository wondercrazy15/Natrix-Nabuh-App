using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginViewModel loginViewModel;

        bool isClick;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            isClick = false;
        }

        public LoginPage()
        {
            InitializeComponent();

            BindingContext = loginViewModel = new LoginViewModel();
        }

        async void LoginButton_Clicked(object sender, EventArgs e)
        {
            
           await loginViewModel.SignInAsync();
            
        }

        async void RegisterButton_Clicked(object sender, EventArgs e)
        {
            if (isClick == false)
            {
                isClick = true;
                await Navigation.PushModalAsync(new NavigationPage(new RegisterPage()));
            }
        }

        async void ForgottenButton_Clicked(object sender, EventArgs e)
        {
            if (isClick == false)
            {
                isClick = true;
                await Navigation.PushModalAsync(new NavigationPage(new ForgottenPage()));
            }
        }
    }
}
