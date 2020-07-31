using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly RegisterViewModel registerViewModel;

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();

        public RegisterPage()
        {
            InitializeComponent();

            BindingContext = registerViewModel = new RegisterViewModel();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private async void info_clicked(object sender, EventArgs e)
        {
            await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordInfo, "Password requirements", GlobalStrings.OkButton);
        }
    }
}
