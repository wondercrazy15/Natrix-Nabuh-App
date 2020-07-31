using Rg.Plugins.Popup.Extensions;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms.Xaml;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.ViewModels.Base;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Views.Popup
{
    public partial class SelectCardPopupView : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Fields

        private readonly ITopupViewModel _context;

        private readonly IDialogService _dialogService;

        private bool _isSelectedCard;

        #endregion Fields

        #region Constructors

        public SelectCardPopupView()
        {
            InitializeComponent();

            CloseWhenBackgroundIsClicked = true;
        }

        public SelectCardPopupView(ObservableCollection<AttachedCards> attachedCards, IDialogService dialogService,
                                   ITopupViewModel context) : this()
        {
            _context = context;

            _dialogService = dialogService;

            if (attachedCards != null)
            {
                AttachedCards = attachedCards;
            }

            NoCardLayoutContainer.IsVisible = attachedCards == null || !attachedCards.Any();

            CardExistLayoutContainer.IsVisible = !NoCardLayoutContainer.IsVisible;

            SelectCardsListView.ItemsSource = AttachedCards;
        }

        #endregion Constructors

        #region Properties

        public ObservableCollection<AttachedCards> AttachedCards { get; set; } = new ObservableCollection<AttachedCards>();

        #endregion Properties

        #region Methods

        private async void ItemSelected(object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            var paymentCard = await _dialogService.ShowConfirmAsync($"Use card {((AttachedCards)e.SelectedItem).CardNumber} for this payment?", "Payment Card", GlobalStrings.OkButton, GlobalStrings.CancelButton);

            if (paymentCard)
            {
                _isSelectedCard = true;

                await Navigation.PopAllPopupAsync();

                _context.ApplySelectedCard((AttachedCards)e.SelectedItem);
            }
        }

        protected override bool OnBackgroundClicked()
        {
            if (!_isSelectedCard)
            {
                _context.ResetSelectedCard();
            }

            CloseAllPopup();

            return false;
        }

        private async void CloseAllPopup()
        {
            await Navigation.PopAllPopupAsync();
        }

        #endregion Methods
    }
}
