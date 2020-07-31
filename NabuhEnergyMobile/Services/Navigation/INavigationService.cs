using System;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.OpenURL;

namespace NabuhEnergyMobile.Services.Navigation
{
    public interface INavigationService
    {
        void InitializeAsync();
        Task RemoveBackStackAsync();
        void NavigateToMain();
        Task NavigateToAsync<T>();
        Task NavigateToAsync<T>(object parameter);
        Task NavigateToLogin();
        Task ShowPopupAsync(PopupPage popupPage);
        Task PopModalAsync();
        Task CloseAllModalsAsync();
    }
}
