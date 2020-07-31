using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.History;
using NabuhEnergyMobile.Models.Payments;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Payments;
using NabuhEnergyMobile.Values;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class HistoryItemViewModel : BaseViewModel
    {
        #region Services

        public IPaymentService PaymentServiceDep => DependencyService.Get<IPaymentService>() ?? new PaymentService();
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();

        #endregion

        #region Fields

        private ReceiptData _receiptData;

        private AccountDto _account;

        private PaymentRecordDto _paymentRecord;

        private Nullable<double> _debt;

        #endregion Fields


        #region Properties

        public AccountDto Account
        {
            get => _account;

            set
            {
                _account = value;
                OnPropertyChanged("Account");
            }
        }

        public PaymentRecordDto PaymentRecord
        {
            get => _paymentRecord;

            set
            {
                _paymentRecord = value;
                OnPropertyChanged("PaymentRecord");
            }
        }

        public Nullable<double> Debt
        {
            get => _debt;

            set
            {
                _debt = value;
                OnPropertyChanged("Debt");
            }
        }

        #endregion Properties

        #region Init

        public override async Task InitializeAsync(object navigationData)
        {
            Debug.WriteLine("Initialize HistoryItem View");

            if (navigationData != null && navigationData is ReceiptData)
            {
                Debt = null;
                _receiptData = (navigationData as ReceiptData);
                Account = _receiptData.Account;
                PaymentRecord = _receiptData.Payment;
                await GetPaymentDetails();
            }

            await base.InitializeAsync(navigationData);
        }

        #endregion Init

        #region Methods
        private async Task GetPaymentDetails()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.GettingHistory);

                var PaymentRecord = await PaymentServiceDep.GetPaymentDetailsAsync(_receiptData.Payment.Id.Value);

                if (PaymentRecord.PaymentStatus != null)
                {
                    var tempDebt = PaymentRecord.PaymentStatus.ResponseMessage;

                   // dynamic data = JObject.Parse(tempDebt);

                    // var jSONObject = new JSONObject(tempDebt);

                   var Debts = JObject.Parse(tempDebt)["OutstandingDebt"];
                    Debt = Double.Parse(Debts.ToString());
                }
                
                DialogServiceDep.HideLoading();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }
        #endregion
    }
}
