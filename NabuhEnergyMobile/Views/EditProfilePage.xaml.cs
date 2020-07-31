using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using NabuhEnergyMobile.Views.Base;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class EditProfilePage : BaseTabView
    {
        private readonly EditProfileViewModel editProfileViewModel;

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();

        public EditProfilePage()
        {
            
            InitializeComponent();


            base.BindingContext = editProfileViewModel = new EditProfileViewModel();
        }

        async void UpdateButton_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editProfileViewModel.EditProfile.NewPassword) || string.IsNullOrEmpty(editProfileViewModel.EditProfile.CurrentPassword) ||
                 string.IsNullOrEmpty(editProfileViewModel.EditProfile.ConfirmNewPassword))
            {
               await editProfileViewModel.DialogServiceDep.ShowAlertAsync("You need to fill the fields to update your password", "Missing fields", GlobalStrings.OkButton);
            }
            else
            {
                await editProfileViewModel.SaveChangesAsync();
                if (editProfileViewModel.IsUpdate)
                {
                    CurrentPwd.Text = string.Empty;
                    NewPwd.Text = string.Empty;
                    ConfirmPwd.Text = string.Empty;
                }
            }
        }
        private async void info_clicked(object sender, EventArgs e)
        {
            await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordInfo, "Password requirements", GlobalStrings.OkButton);
        }
        async void SavedCardButton_Clicked(object sender, EventArgs e)
        {
            await editProfileViewModel.AddCardAsync();
        }
    }
}
