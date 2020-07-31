using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Cards;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using NabuhEnergyMobile.Views;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Services.Navigation
{
    public class NavigationService : INavigationService
    {

        public void InitializeAsync()
        {

        }

        public void NavigateToMain()
        {
            var page = new MainPage();
            Application.Current.MainPage = new NavigationPage(page);
        }

        public async Task NavigateToAsync<T>()
        {
            var pageType = typeof(T);
            Page page = Activator.CreateInstance(pageType) as Page;

            var navigationPage = Application.Current.MainPage as NavigationPage;

            var modalPage = new NavigationPage(page);

            await navigationPage?.Navigation.PushModalAsync(modalPage);
        }

        public Task RemoveBackStackAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            if (mainPage != null)
            {
                for (int i = 0; i < mainPage.Navigation.NavigationStack.Count - 1; i++)
                {
                    var page = mainPage.Navigation.NavigationStack[i];
                    mainPage.Navigation.RemovePage(page);
                }
            }

            return Task.FromResult(true);
        }

        public async Task NavigateToLogin()
        {
            var page = new LoginPage();
            Application.Current.MainPage = new NavigationPage(page);

            await (page.BindingContext as BaseViewModel).InitializeAsync();
        }

        public async Task NavigateToAsync<T>(object parameter)
        {
            var pageType = typeof(T);

            Page page = Activator.CreateInstance(pageType) as Page;

            var navigationPage = Application.Current.MainPage as NavigationPage;

            var modalPage = new NavigationPage(page);

            await navigationPage?.Navigation.PushModalAsync(modalPage);

            await (page.BindingContext as BaseViewModel).InitializeAsync(parameter);
        }

        public async Task PopModalAsync()
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            await mainPage.Navigation.PopModalAsync();
        }

        public async Task ShowPopupAsync(PopupPage popupPage)
        {
            var mainPage = Application.Current.MainPage as NavigationPage;

            await mainPage.Navigation.PushPopupAsync(popupPage);
        }

        public async Task CloseAllModalsAsync()
        {
            int numModals = Application.Current.MainPage.Navigation.ModalStack.Count;
            for (int currModal = 0; currModal < numModals; currModal++)
            {
                await Application.Current.MainPage.Navigation.PopModalAsync();
            }
        }
    }
}
