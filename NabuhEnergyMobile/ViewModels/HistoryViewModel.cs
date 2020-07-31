using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.History;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Services.Payments;
using Xamarin.Forms;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Views;

namespace NabuhEnergyMobile.ViewModels
{
    public class HistoryViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<PaymentHistory> _paymentHistory;

        public Command<PaymentHistory> DisplayReceiptTap { get; set; }

        private AccountDto Account;

        public HistoryViewModel()
        {
            DisplayReceiptTap = new Command<PaymentHistory>(DisplayReceiptTaplick);
        }

        private void DisplayReceiptTaplick(PaymentHistory obj)
        {
            openReceipt(obj.Id);
           
        }

        private async void openReceipt(int selectedId)
        {
            await DisplayReceipt(selectedId);
        }



        #endregion Fields

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();
        public IPaymentService PaymentServiceDep => DependencyService.Get<IPaymentService>() ?? new PaymentService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        #endregion

        #region Properties

        public ObservableCollection<PaymentHistory> PaymentHistory
        {
            get => _paymentHistory;

            set
            {
                _paymentHistory = value;
                OnPropertyChanged("PaymentHistory");
            }
        }

        #endregion Properties

        #region Methods

        private async Task TopupHistoryAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.GettingHistory);

                var paymentHistoryResult = await PaymentServiceDep.GetPaymentHistoryAsync(null);

                PaymentHistory = new ObservableCollection<PaymentHistory>();

                foreach (var x in paymentHistoryResult)
                {
                    PaymentHistory.Add(new PaymentHistory()
                    {
                        Id = x.Id,
                        DateCreated = x.DateCreated ?? "N/A", // .ToString("dd/MM/yyyy"),
                        EnergySource = x.EnergySource ?? "N/A",
                        UtrnCode = x.UtrnCode,
                        PaymentStatus = x.PaymentStatus?.Status
                    });
                }

                DialogServiceDep.ShowSuccess();
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
                DialogServiceDep.ShowLoading(GlobalStrings.SyncAccount);

                var account = await AccountLookupServiceDep.GetAccountInfoAsync();

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

        public async Task DisplayReceipt(int paymentId)
        {
            if (IsBusy)
            {
                return;
            }
            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.GettingHistory);

                var paymentDetails = await PaymentServiceDep.GetPaymentDetailsAsync(paymentId);

                DialogServiceDep.HideLoading();

                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<HistoryItemPage>(new ReceiptData() { Payment = paymentDetails, Account = Account });
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }
        #endregion Methods

        #region Init

        public override async Task InitializeAsync()
        {
            Debug.WriteLine("Initialize History View");

            await TopupHistoryAsync();

            if (Account is null)
            {
                Account = await SynchronizeAccount();
            }

            await base.InitializeAsync();
        }

       

        #endregion Init


    }
}
