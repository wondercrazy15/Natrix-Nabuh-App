using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Utils.Exceptions;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Services.Exceptions
{
    public class HandleExceptionService : IHandleExceptionService
    {
        public ILoginService LoginServiceDep => DependencyService.Get<ILoginService>() ?? new LoginService();
        public INavigationService NavigationServiceDep => DependencyService.Get<INavigationService>() ?? new NavigationService();

        public async Task<string> HandleExceptionAsync(Exception exception, bool isRequiredToLogout = false)
        {
            try
            {
                if (exception.GetType() == typeof(NotConnectedException) ||
                   exception.GetType() == typeof(ServiceIsNotAvailableException) ||
                   exception.GetType() == typeof(UnknownRequestException))
                {
                    return exception.Message;
                }

                if (exception.GetType() == typeof(RequestException) || exception.GetType() == typeof(AccountNotFoundException))
                {
                    var contentResult = await Task.Run(() =>
                    {
                        var content = exception.Message;
                        return JsonConvert.DeserializeObject<HttpError>(content);
                    });

                    if (String.IsNullOrEmpty(contentResult.Message))
                    {
                        throw new InvalidValueException();
                    }

                    if (isRequiredToLogout && exception.GetType() == typeof(AccountNotFoundException))
                    {
                        await LoginServiceDep.LogoutUserAsync();

                        await NavigationServiceDep.RemoveBackStackAsync();

                        await NavigationServiceDep.NavigateToAsync<LoginViewModel>();

                        return contentResult.Message;
                    }

                    return contentResult.Message;
                }

                return GlobalStrings.SomethingWrong;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);

                return GlobalStrings.SomethingWrong;
            }
        }
    }
}
