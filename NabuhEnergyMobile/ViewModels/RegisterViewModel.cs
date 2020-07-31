using System;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Register;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Services.Register;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        #region Fields

        private string _convertedDateTime;
        private string _dateOfBirth;
        private Registration _registration;

        #endregion

        #region Services

        public IOpenUrlService OpenUrlServiceDep => DependencyService.Get<IOpenUrlService>() ?? new OpenUrlService();
        public IRegistrationService RegistrationServiceDep => DependencyService.Get<IRegistrationService>() ?? new RegistrationService();
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        #endregion

        #region Constructors

        public RegisterViewModel()
        {
            Registration = new Registration();
        }

        #endregion

        #region Properties

        public Registration Registration
        {
            get => _registration;
            set
            {
                _registration = value;
                OnPropertyChanged("Registration");
            }
        }

        public string DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                _dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        #endregion

        #region Commands

        public ICommand OpenUrlCommand => new Command(() =>
        {
            OpenUrlServiceDep.OpenUrl(GlobalUrls.NabuhURL);
        });

        public ICommand RegistrationCommand => new Command(async () => await RegisterAsync());

        #endregion

        #region Methods

        private async Task RegisterAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            bool isValid = await ValidateFields();

            if (isValid)
            {
                try
                {
                    DialogServiceDep.ShowLoading();

                    var registrationUser = new RegistrationDto
                    {
                        ApiInstanceID = GlobalService.ApiInstanceId,
                        AccountNumber = Registration.AccountNumber,
                        Mobile = Registration.Mobile,
                        HouseNumber = Registration.HouseNumber,
                        Password = Registration.Password,
                        Postcode = Registration.Postcode
                    };

                    await RegistrationServiceDep.RegisterUserAsync(registrationUser);

                    await NavigationServiceDep.RemoveBackStackAsync();

                    await NavigationServiceDep.CloseAllModalsAsync();

                    DialogServiceDep.ShowSuccess(GlobalStrings.AccountRegisteredSuccessfully, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
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

        private async Task<bool> ValidateFields()
        {
            bool isValidRequiredFilds = !String.IsNullOrEmpty(Registration.AccountNumber) && !String.IsNullOrEmpty(Registration.Password) &&
                                              !String.IsNullOrEmpty(Registration.ConfirmPassword) && !String.IsNullOrEmpty(Registration.HouseNumber) &&
                                              !String.IsNullOrEmpty(Registration.Postcode);

            if (!isValidRequiredFilds)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.FillRequiredFields, GlobalStrings.RequiredFields, GlobalStrings.OkButton);

                return isValidRequiredFilds;
            }

            bool validatePassword = RegExHelper.IsValidPassword(Registration.Password);

            if (!validatePassword)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordDoesntMatch, GlobalStrings.IncorrectPassword, GlobalStrings.OkButton);

                return validatePassword;
            }

            bool isValidPasswords = !String.IsNullOrEmpty(Registration.Password) && !String.IsNullOrEmpty(Registration.ConfirmPassword) &&
                                           Registration.Password == Registration.ConfirmPassword;

            if (!isValidPasswords)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordNotEqualEmpty, GlobalStrings.PasswordDiscrepancy, GlobalStrings.OkButton);

                return isValidPasswords;
            }

            if (!String.IsNullOrEmpty(Registration.Mobile))
            {
                bool isValidPhoneNumber = RegExHelper.IsValidPhoneNumber(Registration.Mobile ?? String.Empty);

                if (!isValidPhoneNumber)
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.PhoneNumberInvalid, GlobalStrings.PhoneNumberFormat, GlobalStrings.OkButton);

                    return isValidPhoneNumber;
                }
            }

            return true;
        }

        private bool ValidateDateOfBirthFormat()
        {
            try
            {
                var _convertedDateTimeTemp = DateTime.ParseExact(DateOfBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                _convertedDateTime = _convertedDateTimeTemp.ToString("ddMMyyyy");

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return false;
            }
        }

        #endregion
    }
}
