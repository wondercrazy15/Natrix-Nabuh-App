using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Services.Login
{
    public class LoginService : ILoginService
    {
        public ISettingsDeserializer SettingsDeserializerDep = new SettingsDeserializer();
        public IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<LoginResultDto> LoginUserAsync(LoginDto credentials)
        {
            var loginResult = await RequestProviderDep.PostAsync<LoginDto, LoginResultDto>(UriHelper.BuildRequestUri(GlobalService.LoginEndpoint), credentials);

            if (loginResult != null)
            {
                await SettingsDeserializerDep.SaveDataAsync(new Models.Account.AccessToken { Token = loginResult.Token, DateTimeCreated = DateTimeOffset.Now }, GlobalService.AccessTokenKey);

                await SettingsDeserializerDep.SaveDataAsync(new Models.Account.Credentials { AccountNumber = credentials.AccountNumber, Password = credentials.Password }, GlobalService.CredentialsKey);

                Application.Current.Properties[GlobalService.AccessTokenKey] = true;

                await Application.Current.SavePropertiesAsync();

                return loginResult;
            }

            return null;
        }

        public async Task LogoutUserAsync()
        {
            await SettingsDeserializerDep.EraseDataAsync(GlobalService.AccessTokenKey, GlobalService.CredentialsKey);

            Application.Current.Properties.Remove(GlobalService.AccessTokenKey);

            await Application.Current.SavePropertiesAsync();
        }
    }
}
