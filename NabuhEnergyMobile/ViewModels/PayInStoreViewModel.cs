using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Utils.Enums;
using NabuhEnergyMobile.Values;
using Net.ConnectCode.Barcode;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class PayInStoreViewModel : BaseViewModel
    {
        #region Fields

        private TopupPaymentEnum _paymentTypeCategory;

        private bool _isQrCodeVisible, _isMsgVisible;

        private string _selectedEnergyTypeText;

        private string _generateOtherButtonText;

        private string _qrCodeValue;

        private AccountDto Account;

        #endregion Fields

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();

        #endregion

        public PayInStoreViewModel()
        {
            IsQrCodeVisible = false;
            IsMsgVisible = false;
            IsElectricityVisible = true;
            IsGasVisible = true;
            IsOtherButtonVisible = false;
            MsgText = string.Empty;
            InfoMsg = "Please select the energy you wish to top-up to generate the Barcode";
        }

        #region Properties

        public TopupPaymentEnum PaymentTypeCategory
        {
            get => _paymentTypeCategory;

            set
            {
                _paymentTypeCategory = value;
                OnPropertyChanged("PaymentTypeCategory");
            }
        }

        public bool IsQrCodeVisible
        {
            get => _isQrCodeVisible;

            set
            {
                _isQrCodeVisible = value;
                OnPropertyChanged("IsQrCodeVisible");
            }
        }

        public bool _isGasVisible;
        public bool IsGasVisible
        {
            get => _isGasVisible;

            set
            {
                _isGasVisible = value;
                OnPropertyChanged("IsGasVisible");
            }
        }

        public bool _isElectricityVisible;
        public bool IsElectricityVisible
        {
            get => _isElectricityVisible;

            set
            {
                _isElectricityVisible = value;
                OnPropertyChanged("IsElectricityVisible");
            }
        }

        public bool _isOtherButtonVisible;
        public bool IsOtherButtonVisible
        {
            get => _isOtherButtonVisible;

            set
            {
                _isOtherButtonVisible = value;
                OnPropertyChanged("IsOtherButtonVisible");
            }
        }

        public bool IsMsgVisible
        {
            get => _isMsgVisible;

            set
            {
                _isMsgVisible = value;
                OnPropertyChanged("IsMsgVisible");
            }
        }

        public string SelectedEnergyTypeText
        {
            get => _selectedEnergyTypeText;

            set
            {
                _selectedEnergyTypeText = value;
                OnPropertyChanged("SelectedEnergyTypeText");
            }
        }

        private string _msgText;
        public string MsgText
        {
            get => _msgText;

            set
            {
                _msgText = value;
                OnPropertyChanged("MsgText");
            }
        }

        private string _infoMsg;
        public string InfoMsg
        {
            get => _infoMsg;

            set
            {
                _infoMsg = value;
                OnPropertyChanged("InfoMsg");
            }
        }

        public string GenerateOtherButtonText
        {
            get => _generateOtherButtonText;

            set
            {
                _generateOtherButtonText = value;
                OnPropertyChanged("GenerateOtherButtonText");
            }
        }

        public string QrCodeValue
        {
            get => _qrCodeValue;

            set
            {
                _qrCodeValue = value;
                OnPropertyChanged("QrCodeValue");
            }
        }

        #endregion Properties

        #region Commands

        public ICommand SelectEnergyTypeCommand => new Command((obj) =>
        {
            PaymentTypeCategory = (TopupPaymentEnum)obj;
            SelectEnergyType();
        });

        public ICommand SelectOtherEnergyTypeCommand => new Command(() =>
        {
            switch (PaymentTypeCategory)
            {
                case TopupPaymentEnum.Gas:
                    PaymentTypeCategory = TopupPaymentEnum.Electricity;
                    break;

                case TopupPaymentEnum.Electricity:
                    PaymentTypeCategory = TopupPaymentEnum.Gas;
                    break;
            }

            SelectEnergyType();
        });

        #endregion Commands

        #region Methods

        private void SelectEnergyType()
        {
            

            SelectedEnergyTypeText = "Barcode for: ";
            switch (PaymentTypeCategory)
            {
                case TopupPaymentEnum.Gas:
                    SelectedEnergyTypeText += "Gas";
                    break;

                case TopupPaymentEnum.Electricity:
                    SelectedEnergyTypeText += "Electricity";
                    break;
            }

            GenerateOtherButtonText = "Generate Barcode for ";
            switch (PaymentTypeCategory)
            {
                case TopupPaymentEnum.Gas:
                    if (string.IsNullOrEmpty(Account.Reference3))
                    {
                        IsOtherButtonVisible = false;
                    }
                    else
                    {
                        IsOtherButtonVisible = true;
                    }
                    GenerateOtherButtonText += "Electricity";
                    break;

                case TopupPaymentEnum.Electricity:
                    if (string.IsNullOrEmpty(Account.Reference2))
                    {
                        IsOtherButtonVisible = false;
                    }
                    else
                    {
                        IsOtherButtonVisible = true;
                    }
                    GenerateOtherButtonText += "Gas";
                    break;
            }

            GenerateQrCode();
        }

        private void GenerateQrCode()
        {
            switch (PaymentTypeCategory)
            {
                case TopupPaymentEnum.Gas:

                    if (!string.IsNullOrEmpty(Account.Reference2))
                    {
                        IsQrCodeVisible = true;
                        IsMsgVisible = false;
                        IsGasVisible = false;
                        IsElectricityVisible = false;
                        QrCodeValue = $"{ Account.Reference2 ?? string.Empty}";
                    }
                    else
                    {
                        IsMsgVisible = true;
                        IsQrCodeVisible = false;
                       // IsGasVisible = true;
                        MsgText = "A Gas Meter doesn't exist on this account";
                    }
                    

                    break;

                case TopupPaymentEnum.Electricity:

                    
                    if (!string.IsNullOrEmpty(Account.Reference3))
                    {
                        IsQrCodeVisible = true;
                        IsMsgVisible = false;
                        IsElectricityVisible = false;
                        IsGasVisible = false;
                        QrCodeValue = $"{ Account.Reference3 ?? string.Empty}";
                    }
                    else
                    {
                        IsMsgVisible = true;
                        IsQrCodeVisible = false;
                        
                        MsgText = "A Electricity Meter doesn't exist on this account";
                    }
                    //QrCodeValue = $"{ Account.Reference3 ?? string.Empty}";
                    break;
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

        #endregion Methods

        #region Init

        public override async Task InitializeAsync(object navigationData)
        {
            Debug.WriteLine("Initialize Pay in Store View");

            if (navigationData != null && navigationData is AccountDto)
            {
                Account = navigationData as AccountDto;
            }

            if (Account is null)
            {
                Account = await SynchronizeAccount();
            }

            if (Account != null)
            {
                if (string.IsNullOrEmpty(Account.Reference2) && string.IsNullOrEmpty(Account.Reference3))
                {
                    IsGasVisible = false;
                    IsElectricityVisible = false;
                    InfoMsg = "There is no Code for Gas or Electricity for this account";
                }
                if (string.IsNullOrEmpty(Account.Reference2))
                {
                    IsGasVisible = false;
                }
                else if (string.IsNullOrEmpty(Account.Reference3))
                {
                    IsElectricityVisible = false;
                }
            }

            await base.InitializeAsync(navigationData);
        }

        #endregion Init
    }
}
