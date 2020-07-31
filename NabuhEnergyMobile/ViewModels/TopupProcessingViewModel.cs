using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Payments;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Services.Payments;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class TopupProcessingViewModel : BaseViewModel
    {
        #region Fields

        private bool _isVisible;

        private string _text, _meterText,_Amount, _msgText;

        #endregion Fields

        #region Services

        public IOpenUrlService OpenUrlServiceDev => DependencyService.Get<IOpenUrlService>() ?? new OpenUrlService();
        public IPaymentService PaymentServiceDev => DependencyService.Get<IPaymentService>() ?? new PaymentService();
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        #endregion

        #region Constructor

        public TopupProcessingViewModel()
        {
            IsVisible = false;
            MsgText = "Thank you";
        }

        #endregion

        #region Properties

        public bool IsVisible
        {
            get => _isVisible;

            set
            {
                _isVisible = value;

                OnPropertyChanged("IsVisible");
            }
        }

        public string Text
        {
            get => _text;

            set
            {
                _text = value;

                OnPropertyChanged("Text");
            }
        }

        public string MsgText
        {
            get => _msgText;

            set
            {
                _msgText = value;

                OnPropertyChanged("MsgText");
            }
        }

        public string meterText
        {
            get => _meterText;

            set
            {
                _meterText = value;

                OnPropertyChanged("meterText");
            }
        }

        #endregion Properties

        #region Commands

        public ICommand ReturnCommand => new Command(async () => await ReturnAsync());

        public ICommand BackCommand => new Command(async () => await BackAsync());

        

        #endregion Commands

        #region Init

        public override async Task InitializeAsync(object navigationData)
        {
            Debug.WriteLine("Initialize TopupProccesing View");

            if (navigationData is TopupResult)
            {
                var result = (navigationData as TopupResult);
                if (result.isSuccess)
                {
                    await TopupUpdateStatus(result.TokenisedPaymentId);
                    Text = $"Your payment of £{_Amount} for {result.PaymentType} has been received";
                    meterText = "Your meter will be credited within the next hour.";
                }
                else
                {
                    IsVisible = true;
                    MsgText = "Payment Failed";
                   
                }
            }

            await base.InitializeAsync(navigationData);
        }

        #endregion Init

        #region Methods

        private async Task BackAsync()
        {
            await NavigationServiceDep.PopModalAsync();
        }

       

        private async Task ReturnAsync()
        {
            if (IsBusy)
            {
                return;
            }

            await NavigationServiceDep.RemoveBackStackAsync();

            await NavigationServiceDep.CloseAllModalsAsync();
        }

        private async Task TopupUpdateStatus(int tokenisedPaymentId)
        {
            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.CheckPaymentStatus);

                var tokenisedPaymentStatus = await PaymentServiceDev.CheckTokenisedPaymentStatusAsync(tokenisedPaymentId);

                var paymentDetails = await PaymentServiceDev.GetPaymentDetailsAsync(tokenisedPaymentId);

                _Amount = paymentDetails.Amount.ToString();

                DialogServiceDep.ShowSuccess(GlobalStrings.PaymentCompleted);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                if (exceptionMessage != GlobalStrings.SomethingWrong)
                {
                    DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                    return;
                }
                IsVisible = true;

                DialogServiceDep.HideLoading();
            }
        }

        #endregion Methods
    }
}
