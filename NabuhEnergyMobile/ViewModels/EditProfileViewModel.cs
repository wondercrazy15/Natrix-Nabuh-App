using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Models.Profile;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.EditProfile;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class EditProfileViewModel : BaseViewModel
    {
        public EditProfileViewModel()
        {
            Title = "Edit Profile";
        }

        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();
        public IEditProfileService EditProfileServiceDep => DependencyService.Get<IEditProfileService>() ?? new EditProfileService();
        public ICardService CardServiceDep => DependencyService.Get<ICardService>() ?? new CardService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        #endregion

        #region Fields

        private EditProfile _editProfile;
        private string CurrentEmail;
        private string CurrentMobile;

        #endregion

        #region Properties

        public EditProfile EditProfile
        {
            get => _editProfile;
            set
            {
                _editProfile = value;
                OnPropertyChanged("EditProfile");
            }
        }

        private bool _IsUpdate;
        public bool IsUpdate
        {
            get => _IsUpdate;
            set
            {
                _IsUpdate = value;
                OnPropertyChanged("IsUpdate");
            }
        }

        #endregion Properties

        #region Methods

        public async Task SaveChangesAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            bool isValid = await ValidatePWD();

            if (isValid)
            {
                try
                {
                    DialogServiceDep.ShowLoading(GlobalStrings.Updating);

                    var updateAccount = new EditProfileDto
                    {
                        Email = !String.IsNullOrEmpty(EditProfile.Email) ? EditProfile.Email : null,
                        Mobile = !String.IsNullOrEmpty(EditProfile.Mobile) ? EditProfile.Mobile : null,
                        CurrentPassword = !String.IsNullOrEmpty(EditProfile.CurrentPassword) ? EditProfile.CurrentPassword : null,
                        NewPassword = !String.IsNullOrEmpty(EditProfile.NewPassword) ? EditProfile.NewPassword : null
                    };

                    await EditProfileServiceDep.UpdateUserAsync(updateAccount);

                    DialogServiceDep.ShowSuccess(GlobalStrings.UpdateSuccess);
                    EditProfile.CurrentPassword = string.Empty;
                    EditProfile.NewPassword = string.Empty;
                    EditProfile.ConfirmNewPassword = string.Empty;
                    IsUpdate = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    var exceptionMessage = await HandleExceptionAsync(ex, true);

                    DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                }
            }

            IsBusy = false;
        }

        public async Task AddCardAsync()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.GettingCards);

                List<Card> attachedCards = await CardServiceDep.GetListOfCardsAsync();

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

                await NavigationServiceDep.RemoveBackStackAsync();

                await NavigationServiceDep.NavigateToAsync<AddCardPage>(attachedCardsList);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }

            IsBusy = false;
        }

        private async Task<bool> Validate()
        {
            if (String.IsNullOrEmpty(EditProfile.CurrentPassword) && String.IsNullOrEmpty(EditProfile.Email) &&
               String.IsNullOrEmpty(EditProfile.Mobile) && String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.FillFieldsToUpdate, GlobalStrings.MissingFields, GlobalStrings.OkButton);

                return false;
            }

            if (!String.IsNullOrEmpty(EditProfile.Email))
            {
                bool isValidEmail = RegExHelper.IsValidEmail(EditProfile.Email);

                if (!isValidEmail)
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.EmailNotValid, GlobalStrings.EmailFormat, GlobalStrings.OkButton);

                    return isValidEmail;
                }
            }

            if (!String.IsNullOrEmpty(EditProfile.Mobile))
            {
                bool isValidMobile = RegExHelper.IsValidPhoneNumber(EditProfile.Mobile);

                if (!isValidMobile)
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.PhoneNumberInvalid, GlobalStrings.PhoneNumberFormat, GlobalStrings.OkButton);

                    return isValidMobile;
                }
            }

            if (!String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                bool validateNewPassword = RegExHelper.IsValidPassword(EditProfile.NewPassword);

                if (!validateNewPassword)
                {
                    await DialogServiceDep.ShowAlertAsync($"New {GlobalStrings.PasswordDoesntMatch}", GlobalStrings.IncorrectPassword, GlobalStrings.OkButton);

                    return validateNewPassword;
                }

                if (String.IsNullOrEmpty(EditProfile.CurrentPassword))
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.CurrentPassword, GlobalStrings.ChangingPassword, GlobalStrings.OkButton);

                    return false;
                }
            }

            bool isValidOldPassword = !String.IsNullOrEmpty(EditProfile.CurrentPassword);

            if (isValidOldPassword && String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.NewPassword, GlobalStrings.ChangingPassword, GlobalStrings.OkButton);

                return false;
            }

            bool isValidPasswords = !String.IsNullOrEmpty(EditProfile.NewPassword) && !String.IsNullOrEmpty(EditProfile.ConfirmNewPassword) &&
                                           EditProfile.NewPassword == EditProfile.ConfirmNewPassword;

            if (!isValidPasswords)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordNotEqualEmpty, GlobalStrings.PasswordDiscrepancy, GlobalStrings.OkButton);

                return isValidPasswords;
            }

            return true;
        }

        private async Task<bool> ValidatePWD()
        {
            if (String.IsNullOrEmpty(EditProfile.CurrentPassword) && String.IsNullOrEmpty(EditProfile.Email) &&
               String.IsNullOrEmpty(EditProfile.Mobile) && String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.FillFieldsToUpdate, GlobalStrings.MissingFields, GlobalStrings.OkButton);

                return false;
            }

            if (!String.IsNullOrEmpty(EditProfile.Email))
            {
                bool isValidEmail = RegExHelper.IsValidEmail(EditProfile.Email);

                if (!isValidEmail)
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.EmailNotValid, GlobalStrings.EmailFormat, GlobalStrings.OkButton);

                    return isValidEmail;
                }
            }

          

            if (!String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                bool validateNewPassword = RegExHelper.IsValidPassword(EditProfile.NewPassword);

                if (!validateNewPassword)
                {
                    await DialogServiceDep.ShowAlertAsync($"New {GlobalStrings.PasswordDoesntMatch}", GlobalStrings.IncorrectPassword, GlobalStrings.OkButton);

                    return validateNewPassword;
                }

                if (String.IsNullOrEmpty(EditProfile.CurrentPassword))
                {
                    await DialogServiceDep.ShowAlertAsync(GlobalStrings.CurrentPassword, GlobalStrings.ChangingPassword, GlobalStrings.OkButton);

                    return false;
                }
            }

            bool isValidOldPassword = !String.IsNullOrEmpty(EditProfile.CurrentPassword);

            if (isValidOldPassword && String.IsNullOrEmpty(EditProfile.NewPassword))
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.NewPassword, GlobalStrings.ChangingPassword, GlobalStrings.OkButton);

                return false;
            }

            bool isValidPasswords = !String.IsNullOrEmpty(EditProfile.NewPassword) && !String.IsNullOrEmpty(EditProfile.ConfirmNewPassword) &&
                                           EditProfile.NewPassword == EditProfile.ConfirmNewPassword;

            if (!isValidPasswords)
            {
                await DialogServiceDep.ShowAlertAsync(GlobalStrings.PasswordNotEqualEmpty, GlobalStrings.PasswordDiscrepancy, GlobalStrings.OkButton);

                return isValidPasswords;
            }

            return true;
        }

        private async Task EditProfileAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                DialogServiceDep.ShowLoading();

                var accountInfo = await AccountLookupServiceDep.GetAccountInfoAsync();

                EditProfile = new EditProfile();

                CurrentEmail = accountInfo.Email;
                CurrentMobile = accountInfo.Mobile;
                EditProfile.Email = accountInfo.Email;
                EditProfile.Mobile = accountInfo.Mobile;

                DialogServiceDep.HideLoading();
            }
            catch (Exception ex)
            {
               // Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }

        #endregion

        #region Init

        public override async Task InitializeAsync()
        {
            //Debug.WriteLine("Initialize Edit Profile View");

            await EditProfileAsync();

            await base.InitializeAsync();
        }

        #endregion Init
    }
}
