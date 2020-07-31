using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class ResetPasswordPage : ContentPage
    {
        private readonly ResetPasswordViewModel resetPasswordViewModel;

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();

        public ResetPasswordPage()
        {
            InitializeComponent();

            BindingContext = resetPasswordViewModel = new ResetPasswordViewModel();
        }

        protected override bool OnBackButtonPressed()
        {
            BacktoHome();
            return true;
        }

        private async void BacktoHome()
        {
            await resetPasswordViewModel.NavigationServiceDep.PopModalAsync();

           // await resetPasswordViewModel.NavigationServiceDep.CloseAllModalsAsync();
        }

        private async void info_clicked(object sender, EventArgs e)
        {
            await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordInfo, "Password requirements", GlobalStrings.OkButton);
        }

        void Cancel_Clicked(object sender, EventArgs e)
        {
            BacktoHome();
        }
    }
}
