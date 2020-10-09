using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.ViewModels.Base;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Threading.Tasks;
using Xamarin.Forms.Xaml;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Views.Popup
{
    public partial class CodePopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Fields

        private readonly IDialogService _dialogService;

        private readonly ITopupViewModel _context;

        #endregion Fields

        #region Contructors

        public CodePopupView()
        {
            InitializeComponent();
        }

        public CodePopupView(IDialogService dialogService, ITopupViewModel context) : this()
        {
            _context = context;

            _dialogService = dialogService;
        }

        #endregion

        #region Methods

        private async void Pressed(object sender, System.EventArgs e)
        {
            bool isValid = await ValidateCard();

            if (isValid)
            {
                await Navigation.PopAllPopupAsync();

                await _context.MakeTopup(CV2Entry.Text);
            }
        }

        private async Task<bool> ValidateCard()
        {
            if (String.IsNullOrEmpty(CV2Entry.Text))
            {
                await _dialogService.ShowAlertAsync("Please fill CVC code!", "Missing Value", GlobalStrings.OkButton);

                return false;
            }

            bool isValidCV2Length = CV2Entry.Text.Length >= 3 && CV2Entry.Text.Length <= 4; ;

            if (!isValidCV2Length)
            {
                await _dialogService.ShowAlertAsync("CVC should contain only 3 or 4 digits!", "Incorrect CVC length", GlobalStrings.OkButton);

                return isValidCV2Length;
            }

            return true;
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
        #endregion
    }
}
