using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Views.Popup;
using Xamarin.Forms;

namespace NabuhEnergyMobile.ViewModels
{
    public class AddCardViewModel : BaseViewModel
    {
        #region Services

        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public ICardService CardServiceDep => DependencyService.Get<ICardService>() ?? new CardService();
        public IAccountLookupService AccountLookupServiceDep => DependencyService.Get<IAccountLookupService>() ?? new AccountLookupService();
        public IOpenUrlService OpenUrlServiceDep => DependencyService.Get<IOpenUrlService>() ?? new OpenUrlService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        #endregion

        #region Fields

        private ObservableCollection<AttachedCards> _attachedCards;
        private int _attachedCardsCount;

        #endregion

        #region Init

        public override async Task InitializeAsync(object navigationData)
        {
            try
            {
                Debug.WriteLine("Initialize AddCard View");

                if (navigationData != null && navigationData is ObservableCollection<AttachedCards>)
                {
                    _attachedCards = (navigationData as ObservableCollection<AttachedCards>);
                    AttachedCards = _attachedCards;
                    AttachedCardsCount = _attachedCards.Count;
                }

                await base.InitializeAsync(navigationData);
            }
            catch (Exception ex)
            {

            }
            
        }

        #endregion

        #region Properties

        public ObservableCollection<AttachedCards> AttachedCards
        {
            get => _attachedCards;

            set
            {
                _attachedCards = value;
                OnPropertyChanged("AttachedCards");
            }
        }

        public int AttachedCardsCount
        {
            get => _attachedCardsCount;

            set
            {
                _attachedCardsCount = value;
                OnPropertyChanged("AttachedCardsCount");
            }
        }

        #endregion

        #region Methods

        public async Task AddNewCardAsync(CreateCardDto newCard)
        {
           
            try
             {
                 Device.BeginInvokeOnMainThread(() => DialogServiceDep.ShowLoading());

                 var newAddedCard = await CardServiceDep.CreateCardAsync(newCard);

                 AttachedCards.Clear();

                 newAddedCard.ForEach(x =>
                 {
                     AttachedCards.Add(new AttachedCards
                     {
                         Id = x.Id,
                         CardNumber = x.Last4Digits,
                         IsPreferred = x.Preferred
                     });
                 });
                 AttachedCardsCount = AttachedCards.Count;
                 Device.BeginInvokeOnMainThread(() => DialogServiceDep.ShowSuccess(GlobalStrings.CardAdded));
             }
             catch (Exception ex)
             {
                 Debug.WriteLine(ex.Message);

                 var exceptionMessage = await HandleExceptionAsync(ex, true);

                 Device.BeginInvokeOnMainThread(() => DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium)));
             }
        }

        private async Task AddCardPopupAsync()
        {
            if (IsBusy)
            {
                return;
            }

            await NavigationServiceDep.ShowPopupAsync(new AddCardPopupView(DialogServiceDep, this));
        }

        private async Task PopModalAsync()
        {
            if (IsBusy)
            {
                return;
            }

            await NavigationServiceDep.PopModalAsync();
        }

        private async Task DeleteCardAsync(AttachedCards item)
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                DialogServiceDep.ShowLoading(GlobalStrings.DeletingCard);

                var updatedCards = await CardServiceDep.DeleteCardAsync(item.Id);

                AttachedCards.Clear();

                updatedCards.ForEach(x =>
                {
                    AttachedCards.Add(new AttachedCards
                    {
                        Id = x.Id,
                        CardNumber = x.Last4Digits,
                        IsPreferred = x.Preferred
                    });
                });
                AttachedCardsCount = AttachedCards.Count;
                DialogServiceDep.ShowSuccess($"Card {item.CardNumber} successfully deleted!");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                var exceptionMessage = await HandleExceptionAsync(ex, true);

                DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
            }
        }

        private async Task SetDefaultCardAsync(AttachedCards item)
        {
            if (IsBusy)
            {
                return;
            }

            if (item.IsPreferred)
            {
                return;
            }

            var newDefaultCard = await DialogServiceDep.ShowConfirmAsync($"Make card ending in {item.CardNumber} the default payment card?", "Default Card", GlobalStrings.OkButton, GlobalStrings.CancelButton);

            if (newDefaultCard)
            {
                try
                {
                    DialogServiceDep.ShowLoading();

                    var newRefreshedAttachedCards = await CardServiceDep.SetPreferredCardAsync(item.Id);

                    AttachedCards.Clear();
                    IsBusy = true;
                    newRefreshedAttachedCards.ForEach(x =>
                    {
                        AttachedCards.Add(new AttachedCards
                        {
                            Id = x.Id,
                            CardNumber = x.Last4Digits,
                            IsPreferred = x.Preferred
                        });
                    });
                    IsBusy = false;
                    DialogServiceDep.ShowSuccess(GlobalStrings.DefaultCardUpdated);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);

                    var exceptionMessage = await HandleExceptionAsync(ex, true);

                    DialogServiceDep.ShowError(exceptionMessage, Utils.Helpers.Utils.GetTimeDuration(Utils.Helpers.Utils.TimeDuration.Medium));
                }
            }
         }

        #endregion

        #region Commands

        public ICommand OpenUrlCommand => new Command(() =>
        {
            OpenUrlServiceDep.OpenUrl(GlobalStrings.SelectCard);
        });

        public ICommand DeleteCardCommand => new Command<AttachedCards>(async (itemToDelete) => await DeleteCardAsync(itemToDelete));

        public ICommand MakeDefaultCardCommand => new Command<AttachedCards>(async (item) => await SetDefaultCardAsync(item));

        public ICommand NewCardCommand => new Command(async () => await AddCardPopupAsync());

        public ICommand DoneCommand => new Command(async () => await PopModalAsync());

        #endregion
    }
}

