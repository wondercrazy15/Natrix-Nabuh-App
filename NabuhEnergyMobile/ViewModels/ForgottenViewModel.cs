using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.Password;
using NabuhEnergyMobile.Utils.Enums;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class ForgottenViewModel : BaseViewModel
    {
        #region Fields

        private ForgottenPassword _forgottenPassword;

        #endregion

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();
        public IPasswordService PasswordServiceDep => DependencyService.Get<IPasswordService>() ?? new PasswordService();

        #endregion

        #region Constructor

        public ForgottenViewModel()
        {
            ForgottenPassword = new ForgottenPassword();
        }

        #endregion

        #region Properties

        public string AccountNumber { get; set; }

        public string Mobile { get; set; }

        public string Email { get; set; }

        public ForgottenPassword ForgottenPassword
        {
            get => _forgottenPassword;
            set
            {
                _forgottenPassword = value;
                OnPropertyChanged("ForgottenPassword");
            }
        }

        #endregion

        #region Commands
        
        public ICommand ResetPasswordCommand => new Command(async () => await ResetPasswordAsync());

        #endregion

        #region Methods

        private async Task ResetPasswordAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            bool isValid = await Validate();

            if (isValid)
            {
                var forgottenPassword = new ForgottenPasswordDto
                {
                    ApiInstanceId = GlobalService.ApiInstanceId,
                    Email = ForgottenPassword.Email,
                    Mobile = ForgottenPassword.Mobile,
                    AccountNumber = ForgottenPassword.AccountNumber,
                    MessageType = GetMessageType(ForgottenPassword.Email, ForgottenPassword.Mobile)
                };

                DialogServiceDep.ShowLoading();

                try
                {
                    await PasswordServiceDep.ResetPassowrdAsync(forgottenPassword);

                    DialogServiceDep.ShowSuccess("Pin code has been sent!", Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));

                    await NavigationServiceDep.NavigateToAsync<ResetPasswordPage>(ForgottenPassword.AccountNumber);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    var exceptionMessage = await HandleExceptionAsync(ex);

                    DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));

                }
                finally
                {
                    IsBusy = false;
                }
            }

            IsBusy = false;
        }

        private async Task<bool> Validate()
        {
            if (String.IsNullOrEmpty(ForgottenPassword.AccountNumber) &&
                (String.IsNullOrEmpty(ForgottenPassword.Email) && String.IsNullOrEmpty(ForgottenPassword.Mobile)))
            {
                await DialogServiceDep.ShowAlertAsync("Please fill required fields", "Missing fields", GlobalStrings.OkButton);

                return false;
            }

            bool isValidAccountNumber = !String.IsNullOrEmpty(ForgottenPassword.AccountNumber);

            if (!isValidAccountNumber)
            {
                await DialogServiceDep.ShowAlertAsync("Account number is required", "Account number", GlobalStrings.OkButton);

                return isValidAccountNumber;
            }

            bool isValidEmail = false;

            if (!String.IsNullOrEmpty(ForgottenPassword.Email))
            {
                isValidEmail = RegExHelper.IsValidEmail(ForgottenPassword.Email);

                if (!isValidEmail)
                {
                    await DialogServiceDep.ShowAlertAsync("Email is not valid!", "Email format", GlobalStrings.OkButton);

                    return isValidEmail;
                }
            }

            bool isValidPhoneNumber = false;

            if (!String.IsNullOrEmpty(ForgottenPassword.Mobile))
            {
                isValidPhoneNumber = RegExHelper.IsValidPhoneNumber(ForgottenPassword.Mobile);

                if (!isValidPhoneNumber)
                {
                    await DialogServiceDep.ShowAlertAsync("Phone number is not valid!", "Phone number format", GlobalStrings.OkButton);

                    return isValidPhoneNumber;
                }
            }

            if (isValidAccountNumber && (!isValidPhoneNumber && !isValidEmail))
            {
                await DialogServiceDep.ShowAlertAsync("At least should be introduced email or phone number", "Missing notification fields", GlobalStrings.OkButton);

                return false;
            }

            return true;
        }

        private MessageTypeEnum GetMessageType(string email, string mobile)
        {
            bool isValidEmail = !(string.IsNullOrEmpty(email));

            bool isValidMobile = !(string.IsNullOrEmpty(mobile));

            if (isValidMobile && isValidEmail)
            {
                return MessageTypeEnum.Both;
            }

            if (isValidEmail)
            {
                return MessageTypeEnum.Email;
            }

            if (isValidMobile)
            {
                return MessageTypeEnum.Sms;
            }

            return MessageTypeEnum.Unknown;
        }

        #endregion
    }
}