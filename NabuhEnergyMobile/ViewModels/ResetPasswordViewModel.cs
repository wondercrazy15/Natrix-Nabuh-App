using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Services.Password;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        #region Fields

        private ResetPassword _resetPassword;

        private string _accountNumber;

        #endregion

        #region Services

        public IPasswordService PasswordServiceDep => DependencyService.Get<IPasswordService>() ?? new PasswordService();
        public IOpenUrlService OpenUrlServiceDep => DependencyService.Get<IOpenUrlService>() ?? new OpenUrlService();
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService(); 

        #endregion

        #region Constructors

        public ResetPasswordViewModel()
        {
            ResetPassword = new ResetPassword();
        }

        #endregion

        #region Properties

        public ResetPassword ResetPassword
        {
            get => _resetPassword;
            set
            {
                _resetPassword = value;

                OnPropertyChanged("ResetPassword");
            }
        }

        #endregion

        #region Commands

        public ICommand OpenUrlCommand => new Command(() =>
        {
            OpenUrlServiceDep.OpenUrl(GlobalUrls.NabuhURL);
        });

        public ICommand UpdateCommand => new Command(async () => await UpdateAsync());

        #endregion

        #region Methods

        private async Task UpdateAsync()
        {

             if (IsBusy)
             {
                 return;
             }

             IsBusy = true;

             bool isValid = await Validate();

             if (isValid)
             {
                 DialogServiceDep.ShowLoading();

                 try
                 {
                     var resetPassword = new ResetPasswordDto
                     {
                         AccountNumber = _accountNumber,
                         ApiInstanceId = GlobalService.ApiInstanceId,
                         NewPassword = ResetPassword.NewPassword,
                         PinCode = ResetPassword.ResetCode
                     };

                     await PasswordServiceDep.UpdateNewPassowrdAsync(resetPassword);

                      await NavigationServiceDep.RemoveBackStackAsync();

                      await NavigationServiceDep.NavigateToLogin();

                     DialogServiceDep.ShowSuccess(GlobalStrings.PasswordReseted, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                 }
                 catch (Exception ex)
                 {
                     Debug.WriteLine(ex.Message);

                     var exceptionMessage = await HandleExceptionAsync(ex);

                     DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                 }
             }

             IsBusy = false;
        }

        private async Task<bool> Validate()
        {
            if (String.IsNullOrEmpty(ResetPassword.ResetCode) || String.IsNullOrEmpty(ResetPassword.NewPassword) ||
              String.IsNullOrEmpty(ResetPassword.ConfirmNewPassword))
            {
                await DialogServiceDep.ShowAlertAsync("Please fill all fields", "Missing fields", GlobalStrings.OkButton);

                return false;
            }

            bool isValidResetCode = !(String.IsNullOrEmpty(ResetPassword.ResetCode));

            if (!isValidResetCode)
            {
                await DialogServiceDep.ShowAlertAsync("Please fill reset code", "Reset code", GlobalStrings.OkButton);

                return isValidResetCode;
            }

            bool validatePassword = RegExHelper.IsValidPassword(ResetPassword.NewPassword);

            if (!validatePassword)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordDoesntMatch, "Incorrect format password", GlobalStrings.OkButton);

                return validatePassword;
            }

            bool isValidPasswords = !String.IsNullOrEmpty(ResetPassword.NewPassword) &&
                                           !String.IsNullOrEmpty(ResetPassword.ConfirmNewPassword) &&
                                           ResetPassword.NewPassword == ResetPassword.ConfirmNewPassword;

            if (!isValidPasswords)
            {
                await DialogServiceDep.ShowAlertAsync("Passwords not equal or empty!", "Password discrepancy", GlobalStrings.OkButton);

                return isValidPasswords;
            }

            return true;
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData != null)
            {
                _accountNumber = navigationData.ToString();
            }
            await base.InitializeAsync(navigationData);
        }

       

        #endregion
    }
}
