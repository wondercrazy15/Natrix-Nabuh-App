using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Models.Payments;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.Payments;
using NabuhEnergyMobile.Utils.Enums;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels.Base;
using NabuhEnergyMobile.Views;
using NabuhEnergyMobile.Views.Popup;
using Xamarin.Forms;


namespace NabuhEnergyMobile.ViewModels
{
    public class TopUpViewModel : BaseViewModel, ITopupViewModel
    {
        #region Fields

        private AccountDto Account;

        private double CurrentAmount;

        private int SelectedCardId;
        //  var configuration = NSUrlSessionConfiguration.CreateBackgroundSessionConfiguration("my.app.identifier");
        private TopupPaymentEnum _paymentTypeCategory;

        private TopupPaymentEnum _paymentAmountCategory;

        private TopupPaymentEnum _paymentNotificationCategory;

        private TopupPaymentEnum _paymentCardCategory;

        private string _selectedCard;

        private string _otherAmount;

        #endregion Fields

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();
        public IPaymentService PaymentServiceDep => DependencyService.Get<IPaymentService>() ?? new PaymentService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();
        public ICardService CardServiceDep => DependencyService.Get<ICardService>() ?? new CardService();

        #endregion

        public TopUpViewModel()
        {
            OtherAmount = GlobalStrings.OtherAmount;

            SelectedCard = GlobalStrings.SelectCard;
            IsEnableGas = true;
            IsEnableElectricity = true;
        }

        #region Properties

        public string SelectedCard
        {
            get => _selectedCard;

            set
            {
                _selectedCard = value;

                OnPropertyChanged("SelectedCard");
            }
        }

        public bool _isEnableElectricity;
        public bool IsEnableElectricity
        {
            get => _isEnableElectricity;

            set
            {
                _isEnableElectricity = value;

                OnPropertyChanged("IsEnableElectricity");
            }
        }

        public bool _isEnableGas;
        public bool IsEnableGas
        {
            get => _isEnableGas;

            set
            {
                _isEnableGas = value;

                OnPropertyChanged("IsEnableGas");
            }
        }

        public TopupPaymentEnum PaymentTypeCategory
        {
            get => _paymentTypeCategory;

            set
            {
                if (_paymentTypeCategory == value)
                {
                    _paymentTypeCategory = TopupPaymentEnum.NotChosen;

                    PaymentAmountCategory = TopupPaymentEnum.NotChosen;

                    OtherAmount = GlobalStrings.OtherAmount;

                    SelectedCard = GlobalStrings.SelectCard;

                    OnPropertyChanged("PaymentTypeCategory");

                    return;
                }

                if (_paymentAmountCategory == TopupPaymentEnum.Other)
                {
                    PaymentAmountCategory = TopupPaymentEnum.NotChosen;

                    OtherAmount = GlobalStrings.OtherAmount;

                    SelectedCard = GlobalStrings.SelectCard;
                }

                _paymentTypeCategory = value;

                OnPropertyChanged("PaymentTypeCategory");
            }
        }

        public TopupPaymentEnum PaymentAmountCategory
        {
            get => _paymentAmountCategory;

            set
            {
                if (_paymentAmountCategory == value || PaymentTypeCategory == TopupPaymentEnum.NotChosen)
                {
                    _paymentAmountCategory = TopupPaymentEnum.NotChosen;

                    PaymentNotificationCategory = TopupPaymentEnum.NotChosen;

                    OtherAmount = GlobalStrings.OtherAmount;

                    SelectedCard = GlobalStrings.SelectCard;

                    OnPropertyChanged("PaymentAmountCategory");

                    return;
                }

                if (_paymentAmountCategory == TopupPaymentEnum.Other)
                {
                    PaymentNotificationCategory = TopupPaymentEnum.NotChosen;
                }

                _paymentAmountCategory = value;

                OnPropertyChanged("PaymentAmountCategory");
            }
        }

        public TopupPaymentEnum PaymentNotificationCategory
        {
            get => _paymentNotificationCategory;

            set
            {
                if (_paymentNotificationCategory == value || PaymentAmountCategory == TopupPaymentEnum.NotChosen)
                {
                    _paymentNotificationCategory = TopupPaymentEnum.NotChosen;

                    PaymentCardCategory = TopupPaymentEnum.NotChosen;

                    SelectedCard = GlobalStrings.SelectCard;

                    OnPropertyChanged("PaymentNotificationCategory");
                    return;
                }

                if (_paymentAmountCategory == TopupPaymentEnum.Other)
                {
                    PaymentCardCategory = TopupPaymentEnum.NotChosen;
                }

                _paymentNotificationCategory = value;

                OnPropertyChanged("PaymentNotificationCategory");
            }
        }

        public TopupPaymentEnum PaymentCardCategory
        {
            get => _paymentCardCategory;

            set
            {
                if (_paymentCardCategory == value || PaymentNotificationCategory == TopupPaymentEnum.NotChosen)
                {
                    _paymentCardCategory = TopupPaymentEnum.NotChosen;

                    OnPropertyChanged("PaymentCardCategory");

                    return;
                }

                _paymentCardCategory = value;

                OnPropertyChanged("PaymentCardCategory");
            }
        }

        public string OtherAmount
        {
            get
            {
                return _otherAmount;
            }
            set
            {
                _otherAmount = value;

                OnPropertyChanged("OtherAmount");
            }
        }

        #endregion Properties

        #region Commands


        public ICommand PaymentTypeCategoryCommand => new Command((obj) =>
        {
            PaymentTypeCategory = (TopupPaymentEnum)obj;
        });

        public ICommand PaymentAmountCommand => new Command(async (obj) =>
        {
            if (PaymentAmountCategory == TopupPaymentEnum.Other)
            {
                PaymentAmountCategory = (TopupPaymentEnum)obj;

                OtherAmount = GlobalStrings.OtherAmount;

                SelectedCard = GlobalStrings.SelectCard;

                return;
            }

            if ((TopupPaymentEnum)obj == TopupPaymentEnum.Other)
            {
                PaymentAmountCategory = (TopupPaymentEnum)obj;

                await NavigationServiceDep.ShowPopupAsync(new OtherAmountPopupView(PaymentTypeCategory, DialogServiceDep, this));

                return;
            }

            OtherAmount = GlobalStrings.OtherAmount;

            PaymentAmountCategory = (TopupPaymentEnum)obj;
        });

        public ICommand PaymentNotificationCommand => new Command((obj) =>
        {
            PaymentNotificationCategory = (TopupPaymentEnum)obj;
        });

        public ICommand PaymentCardCommand => new Command(async (obj) =>
        {
            if (PaymentCardCategory == TopupPaymentEnum.SelectCard)
            {
                PaymentCardCategory = (TopupPaymentEnum)obj;

                SelectedCard = GlobalStrings.SelectCard;

                return;
            }

            PaymentCardCategory = (TopupPaymentEnum)obj;

            try
            {
                DialogServiceDep.ShowLoading();

                var attachedCards = await CardServiceDep.GetListOfCardsAsync();

                ObservableCollection<AttachedCards> attachedCardsList = new ObservableCollection<AttachedCards>();

                attachedCards.ForEach(x =>
                {
                    attachedCardsList.Add(new AttachedCards
                    {
                        CardNumber = x.Last4Digits,
                        Id = x.Id,
                        IsPreferred = x.Preferred
                    });
                });

                DialogServiceDep.HideLoading();

                await NavigationServiceDep.ShowPopupAsync(new SelectCardPopupView(attachedCardsList, DialogServiceDep, this));
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                PaymentCardCategory = TopupPaymentEnum.NotChosen;

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        });

        public ICommand TopupCommand => new Command(async () =>
        {
            await NavigationServiceDep.ShowPopupAsync(new CodePopupView(DialogServiceDep, this));
        });

        public ICommand PayInStoreCommand => new Command(async () =>
        {
            await NavigationServiceDep.NavigateToAsync<PayInStorePage>(Account);
        });

        #endregion Commands

        #region Methods

        public async Task MakeTopup(string CVC)
        {
            try
            {
                if (Account is null)
                {
                    DialogServiceDep.ShowLoading(GlobalStrings.ReSyncAccount);

                    Account = await SynchronizeAccount();
                }

                var createTokenisedPayment = new CreateTokenisedDto
                {
                    Amount = GetCurrentAmount(),
                    Reference = PaymentTypeCategory == TopupPaymentEnum.Gas ? Account.Reference2 : Account.Reference3,
                    TokenisedCard = new TokenisedCard
                    {
                        Id = SelectedCardId,
                        CVC = CVC
                    },
                    EmailReceipt = PaymentNotificationCategory == TopupPaymentEnum.Email,
                    SmsReceipt = PaymentNotificationCategory == TopupPaymentEnum.SMS
                };

                DialogServiceDep.ShowLoading(GlobalStrings.ProccessingPayment);

                var topupResult = new TopupResult()
                {
                    TokenisedPaymentId = await PaymentServiceDep.CreateTokenisedPaymentAsync(createTokenisedPayment),
                    PaymentType = PaymentTypeCategory == TopupPaymentEnum.Electricity ? "Electricity" : "Gas",
                    Amount = GetCurrentAmount(),
                    isSuccess = true
                };

                DialogServiceDep.HideLoading();

                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<TopupProcessingPage>(topupResult);
            }
            catch (Exception)
            {
                var topupResult = new TopupResult()
                {
                    PaymentType = PaymentTypeCategory == TopupPaymentEnum.Electricity ? "Electricity" : "Gas",
                    Amount = GetCurrentAmount(),
                    isSuccess = false
                };
                DialogServiceDep.HideLoading();
                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<TopupProcessingPage>(topupResult);
                //Debug.WriteLine(ex.Message);

                // var exceptionMessage = await HandleExceptionAsync(ex, true);

                // DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }

        public void OtherAmountChoosen(double otherAmount)
        {
            OtherAmount = $"£{otherAmount}";

            CurrentAmount = otherAmount;
        }

        public void ResetOtherAmount() => PaymentAmountCategory = TopupPaymentEnum.NotChosen;

        public void ResetSelectedCard()
        {
            SelectedCard = GlobalStrings.SelectCard;

            PaymentCardCategory = TopupPaymentEnum.NotChosen;
        }

        public void ApplySelectedCard(AttachedCards attachedCard)
        {
            SelectedCard = attachedCard.CardNumber;

            SelectedCardId = attachedCard.Id;
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

        private double GetCurrentAmount()
        {
            switch (PaymentAmountCategory)
            {
                case TopupPaymentEnum.FiveGBP:
                    return (double)TopupPaymentEnum.FiveGBP;

                case TopupPaymentEnum.TenGBP:
                    return (double)TopupPaymentEnum.TenGBP;

                case TopupPaymentEnum.FifteenGBP:
                    return (double)TopupPaymentEnum.FifteenGBP;

                case TopupPaymentEnum.TwentyGBP:
                    return (double)TopupPaymentEnum.TwentyGBP;

                case TopupPaymentEnum.TwentyFiveGBP:
                    return (double)TopupPaymentEnum.TwentyFiveGBP;

                case TopupPaymentEnum.Other:
                    return CurrentAmount;

                default:
                    return 0d;
            }
        }

        #endregion Methods

        #region Init

        public override async Task InitializeAsync(object navigationData)
        {
            Debug.WriteLine("Initialize TopupViewModel");

            if (navigationData != null && navigationData is LoginResultDto)
            {
                Account = (navigationData as LoginResultDto).Account;
            }

            if (navigationData is string)
            {
                PaymentTypeCategory = ((string)navigationData == Enum.GetName(typeof(TopupPaymentEnum), TopupPaymentEnum.Gas)) ? TopupPaymentEnum.Gas : TopupPaymentEnum.Electricity;
            }

            if (navigationData != null && navigationData is AccountDto)
            {
                Account = navigationData as AccountDto;
            }

            if (Account is null)
            {
                Account = await SynchronizeAccount();
            }

            if (string.IsNullOrEmpty(Account.Reference2) && string.IsNullOrEmpty(Account.Reference3))
            {
                IsEnableGas = false;
                IsEnableElectricity = false;
            }
            else if (string.IsNullOrEmpty(Account.Reference2))
            {
                IsEnableGas = false;
            }
            else if (string.IsNullOrEmpty(Account.Reference3))
            {
                IsEnableElectricity = false;
            }

                await base.InitializeAsync(navigationData);
        }

        #endregion Init
    }
}
