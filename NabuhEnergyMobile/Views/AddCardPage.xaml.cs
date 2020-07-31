using System;
using System.Collections.Generic;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;

using Xamarin.Forms;

namespace NabuhEnergyMobile.Views
{
    public partial class AddCardPage : ContentPage
    {
        private readonly AddCardViewModel addCardViewModel;
        public IDialogService DialogServiceDep => DependencyService.Get<IDialogService>() ?? new DialogService();
        public AddCardPage()
        {
            try
            {
                InitializeComponent();

                base.BindingContext = addCardViewModel = new AddCardViewModel();
            }
            catch (Exception ex)
            {

            }
           
        }

        void OnDelete(object sender, System.EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            DeleteCard(menuItem);
         
        }

      

        private async void DeleteCard(MenuItem menuItem)
        {
            var DeleteCard = await DialogServiceDep.ShowConfirmAsync("Are you sure you want delete the card?", "Delete Card", GlobalStrings.OkButton, GlobalStrings.CancelButton);
            if(DeleteCard)
            {
                addCardViewModel.DeleteCardCommand.Execute((AttachedCards)menuItem.CommandParameter);
            }
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        async void Cancel_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
