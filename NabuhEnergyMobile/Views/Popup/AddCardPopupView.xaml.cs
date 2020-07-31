using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Views.Popup
{
    public partial class AddCardPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Fields

        private readonly IDialogService _dialogService;

        private readonly AddCardViewModel _context;

        #endregion

        #region Constructors

        public AddCardPopupView()
        {
            InitializeComponent();
        }

        public AddCardPopupView(IDialogService dialogService, AddCardViewModel context) : this()
        {
            _context = context;

            _dialogService = dialogService;
        }
        #endregion

        #region Methods
        async void Pressed(object sender, System.EventArgs e)
        {
            bool isValid = await ValidateCard();

            if (isValid)
            {
                var newCard = new CreateCardDto
                {
                    CardNumber = CardNumberEntry.Text,
                    Cvv = CardCvvEntry.Text,
                    DateToMM = ExpirationEntry.Text.Substring(0, 2),
                    DateToYY = ExpirationEntry.Text.Substring(3, 2),
                };

                await Navigation.PopAllPopupAsync();

                await _context.AddNewCardAsync(newCard);
            }
        }

        protected override bool OnBackgroundClicked()
        {
            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        private async Task<bool> ValidateCard()
        {
            if (String.IsNullOrEmpty(CardNumberEntry.Text) || String.IsNullOrEmpty(ExpirationEntry.Text)
             || String.IsNullOrEmpty(CardCvvEntry.Text))
            {
                await _dialogService.ShowAlertAsync("Please fill all fields!", "Missing fields", GlobalStrings.OkButton);

                return false;
            }

            bool isValidCardNumber = CardNumberEntry.Text.Length == 16;

            if (!isValidCardNumber)
            {
                await _dialogService.ShowAlertAsync("Card number does not match our format. !", "Incorrect card number", GlobalStrings.OkButton);

                return isValidCardNumber;
            }

            bool isValidExpireDate = ValidateExpireDateFormat();

            if (!isValidExpireDate)
            {
                await _dialogService.ShowAlertAsync("Invalid format of expire date", "Expire Date format", GlobalStrings.OkButton);

                return isValidExpireDate;
            }

            bool isValidCvv = CardCvvEntry.Text.Length == 3;

            if (!isValidCvv)
            {
                await _dialogService.ShowAlertAsync("Cvv doesnt  match!", "Incorrect cvv format", GlobalStrings.OkButton);

                return isValidCvv;
            }

            return true;
        }

        private bool ValidateExpireDateFormat()
        {
            try
            {
                var _convertedDateTimeTemp = DateTime.ParseExact(ExpirationEntry.Text, "MM/yy", CultureInfo.InvariantCulture);

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
