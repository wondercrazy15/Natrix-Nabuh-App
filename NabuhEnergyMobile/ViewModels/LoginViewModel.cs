using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Values;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services

        public IOpenUrlService OpenUrlServiceDep => DependencyService.Get<IOpenUrlService>() ?? new OpenUrlService();
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();
        public ILoginService LoginServiceDep => DependencyService.Get<ILoginService>() ?? new LoginService();

        #endregion

        #region Properties

        public string AccountNumber { get; set; }

        public string Password { get; set; }

        #endregion

        #region Methods

        public async Task SignInAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            bool isValid = await Validate();

            if (isValid)
            {
                try
                {
                    DialogServiceDep.ShowLoading();

                    var loginResult = await LoginServiceDep.LoginUserAsync(new LoginDto { AccountNumber = AccountNumber, Password = Password, ApiInstanceId = GlobalService.ApiInstanceId });

                    DialogServiceDep.HideLoading();

                    await NavigationServiceDep.RemoveBackStackAsync();

                    NavigationServiceDep.NavigateToMain();
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    var exceptionMessage = await HandleExceptionAsync(ex);

                    DialogServiceDep.ShowError(exceptionMessage, 2500);
                }
            }

            IsBusy = false;
        }

        private async Task<bool> Validate()
        {
            if (String.IsNullOrEmpty(AccountNumber) && String.IsNullOrEmpty(Password))
            {
                await DialogServiceDep.ShowAlertAsync("Please enter account number and password", "Missing fields", GlobalStrings.OkButton);

                return false;
            }

            bool isValidLogin = !String.IsNullOrEmpty(AccountNumber);

            if (!isValidLogin)
            {
                await DialogServiceDep.ShowAlertAsync("Account number is required", "Missing account number", GlobalStrings.OkButton);

                return isValidLogin;
            }

            bool isValidPassword = !String.IsNullOrEmpty(Password);

            if (!isValidPassword)
            {
                await DialogServiceDep.ShowAlertAsync("Password number is required", "Missing fields", GlobalStrings.OkButton);

                return isValidPassword;
            }

            return true;
        }

        #endregion

        #region Commands

        public ICommand OpenUrlCommand => new Command<string>((urlToOpen) =>
        {
            OpenUrlServiceDep.OpenUrl(urlToOpen);
        });

        #endregion
    }
}
