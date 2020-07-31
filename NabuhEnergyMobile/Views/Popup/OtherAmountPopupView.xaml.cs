using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Utils.Enums;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels.Base;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using System.Diagnostics;
using System.Globalization;

namespace NabuhEnergyMobile.Views.Popup
{
    public partial class OtherAmountPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Fields

        private TopupPaymentEnum _currentCategory;

        private readonly IDialogService _dialogService;

        private readonly ITopupViewModel _context;

        private bool _isAmountChosen;

        #endregion

        #region Constructors

        public OtherAmountPopupView()
        {
            InitializeComponent();
        }

        public OtherAmountPopupView(
            TopupPaymentEnum currentCategory,
            IDialogService dialogService,
            ITopupViewModel context) : this()
        {
            _context = context;

            _dialogService = dialogService;

            Init(currentCategory);
        }

        #endregion

        #region Methods

        private void Init(TopupPaymentEnum currentCategory)
        {
            _currentCategory = currentCategory;

            if (currentCategory == TopupPaymentEnum.Electricity)
            {
                CategoryLabel.Text = GlobalStrings.ElectricityType;

            }
            else
            {
                CategoryLabel.Text = GlobalStrings.GasType;
            }

            MaximumAmountLabel.Text = GlobalStrings.OtherAmountMinMax;
        }

        protected override bool OnBackgroundClicked()
        {
            if (!_isAmountChosen)
            {
                _context.ResetOtherAmount();
            }

            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        async void Pressed(object sender, System.EventArgs e)
        {

            if (String.IsNullOrEmpty(AmountEntry.Text))
            {
                await _dialogService.ShowAlertAsync(GlobalStrings.IntroduceValue, GlobalStrings.MissingValue, GlobalStrings.OkButton);

                return;
            }

            bool isValidAmount = false;

            try
            {
                isValidAmount = CheckIntroducedAmount();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                await _dialogService.ShowAlertAsync(GlobalStrings.IntroduceAmount, GlobalStrings.MissingValue, GlobalStrings.OkButton);

                return;
            }

            if (isValidAmount)
            {
                double amountValue;

                var amountResult = AmountEntry.Text.Replace(",", ".");

                var convertedAmount = Double.TryParse(amountResult, NumberStyles.Any, CultureInfo.InvariantCulture, out amountValue);

                _isAmountChosen = true;

                await Navigation.PopAllPopupAsync();

                _context.OtherAmountChoosen(convertedAmount ? amountValue : 0d);
            }
            else
            {
                await _dialogService.ShowAlertAsync(GlobalStrings.IncorrectAmountTyped, GlobalStrings.IncorrectAmount, GlobalStrings.OkButton);
            }
        }

        private bool CheckIntroducedAmount()
        {
            var amountResult = AmountEntry.Text.Replace(",", ".");

            var convertedAmount = Double.Parse(amountResult, NumberStyles.Any, CultureInfo.InvariantCulture);

            return 5 <= convertedAmount && convertedAmount <= 50;
        }
        #endregion
    }
}
