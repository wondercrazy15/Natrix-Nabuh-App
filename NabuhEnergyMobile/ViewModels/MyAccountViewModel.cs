using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Token;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class MyAccountViewModel : BaseViewModel
    {
        public MyAccountViewModel()
        {
            Title = "My Account";
            ImgGas = "balance_gas.png";
            ImgElectricity = "balance_electricity.png";
        }

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public ILoginService LoginServiceDep => DependencyService.Get<ILoginService>() ?? new LoginService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();

        #endregion

        #region Fields

        private AccountDto Account;

        private string _balanceGas;
        private string _balanceElectricity;
        private string _tariffName;
        private string _imgGas;
        private string _imgElectricity;
        public bool _isLogoutClick;
        #endregion Fields

        #region Properties

        public string ImgGas
        {
            get => _imgGas;

            set
            {
                _imgGas = value;
                OnPropertyChanged("ImgGas");
            }
        }

        public string ImgElectricity
        {
            get => _imgElectricity;

            set
            {
                _imgElectricity = value;
                OnPropertyChanged("ImgElectricity");
            }
        }

        public string BalanceGas
        {
            get => _balanceGas;

            set
            {
                _balanceGas = value;
                OnPropertyChanged("BalanceGas");
            }
        }

        public string BalanceElectricity
        {
            get => _balanceElectricity;

            set
            {
                _balanceElectricity = value;
                OnPropertyChanged("BalanceElectricity");
            }
        }

        public string TariffName
        {
            get => _tariffName;

            set
            {
                _tariffName = value;
                OnPropertyChanged("TariffName");
            }
        }

        public bool IsLogoutClick
        {
            get => _isLogoutClick;

            set
            {
                _isLogoutClick = value;
                OnPropertyChanged("IsLogoutClick");
            }
        }

        #endregion Properties

        #region Methods

        public async Task LoadTopUp()
        {
            try
            {
                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<TopUpPage>(Account);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }

        public async Task LoadAutoTopUp()
        {
            try
            {
                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<AutoTopUpPage>(Account);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }

        private async Task<AccountDto> SynchronizeAccount()
        {
            try
            {
                //DialogServiceDep.ShowLoading(GlobalStrings.SyncAccount);

                var account = await AccountLookupServiceDep.GetAccountInfoAsync();

                if (string.IsNullOrEmpty(account.Reference2) && string.IsNullOrEmpty(account.Reference3))
                {
                    BalanceElectricity = "N/A";
                    ImgElectricity = "gray_balance_electricity.png";
                    BalanceGas = "N/A";
                    ImgGas = "gray_balance_gas.png";
                    TariffName = $"Tariff: { account.TariffName ?? "" + "N/A" + " / " + "N/A" }";
                }
                else if (string.IsNullOrEmpty(account.Reference2))
                {
                    BalanceGas = "N/A";
                    BalanceElectricity = $"£{ account.MeterBalanceElec ?? 0 }";
                    ImgGas = "gray_balance_gas.png";
                    TariffName = $"Tariff: { account.TariffName ?? "" + account.Tariff_Name_Energy + " / " + "N/A" }";
                }
                else if (string.IsNullOrEmpty(account.Reference3))
                {
                    BalanceElectricity = "N/A";
                    ImgElectricity = "gray_balance_electricity.png";
                    BalanceGas = $"£{ account.MeterBalanceGas ?? 0 }";
                    TariffName = $"Tariff: { account.TariffName ?? "" + "N/A" + " / " + account.Tariff_Name_Gas }";
                }
                else
                {
                    BalanceGas = $"£{ account.MeterBalanceGas ?? 0 }";
                    BalanceElectricity = $"£{ account.MeterBalanceElec ?? 0 }";
                    TariffName = $"Tariff: { account.TariffName ?? "" + account.Tariff_Name_Energy + " / " + account.Tariff_Name_Gas }";
                }

                    
                //DialogServiceDep.ShowSuccess(GlobalStrings.SyncAccountSuccess);
                try
                {
                    //DialogServiceDep.ShowLoading(GlobalStrings.Updating);
                    if (!string.IsNullOrEmpty(UserSettings.AccesToken))
                    {
                        var updateToken = new UpdateTokenDto
                        {
                            fireBaseToken = UserSettings.AccesToken
                        };
                        await AccountLookupServiceDep.UpdateToken(updateToken);

                    }
                
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    var exceptionMessage = await HandleExceptionAsync(ex, true);

                    DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                }

                DialogServiceDep.ShowSuccess(GlobalStrings.SyncAccountSuccess);

                return account;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));

                return null;
            }
        }




        public async Task SingOutAsync()
        {
            var isLogoutConfirmed = await DialogServiceDep.ShowConfirmAsync($"Are you sure want to logout?", "Logout User", GlobalStrings.OkButton, GlobalStrings.CancelButton);

            if (isLogoutConfirmed)
            {
                DialogServiceDep.ShowLoading("Logout...");

                await LoginServiceDep.LogoutUserAsync();

                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToLogin();

                DialogServiceDep.HideLoading();
            }
            else
            {
                IsLogoutClick = false;
            }
        }

        #endregion Methods

        #region Init

        public override async Task InitializeAsync()
        {
            Debug.WriteLine("Initialize My Account View");

            if (Account is null)
            {
                Account = await SynchronizeAccount();
            }

            await base.InitializeAsync();
        }

        #endregion Init

    }
}
