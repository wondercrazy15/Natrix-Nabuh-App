using System;
using System.Diagnostics;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Models.Token;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.AccessToken
{
    public class AccessTokenService
    {
        public ISettingsDeserializer SettingsDeserializerDep = new SettingsDeserializer();
        public IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        protected async Task CheckCurrentAccessTokenAsync()
        {
            var currentToken = await GetCurrentAccessToken();

            if (IsTokenExpired(currentToken.Item2))
            {
                var credentials = await GetCredentialsAsync();

                await GetNewAccessTokenAsync(credentials);
            }
        }

        private async Task GetNewAccessTokenAsync(LoginDto loginCredentials)
        {
            Debug.WriteLine("Time to refresh ### AccessToken ###");

            var accessTokenResult = await RequestProviderDep.PostAsync<LoginDto, TokenDto>(UriHelper.BuildRequestUri(GlobalService.RequestTokenEndpoint), loginCredentials);

            if (accessTokenResult != null)
            {
                await SaveRefreshedAccessTokenAsync(accessTokenResult.Token);
            }
        }

        private async Task SaveRefreshedAccessTokenAsync(string newAccessToken)
        {
            var accessToken = new Models.Account.AccessToken
            {
                Token = newAccessToken,
                DateTimeCreated = DateTimeOffset.Now
            };

            await SettingsDeserializerDep.SaveDataAsync(accessToken, GlobalService.AccessTokenKey);
        }

        private async Task<LoginDto> GetCredentialsAsync()
        {
            var credentials = await SettingsDeserializerDep.GetDataAsync<Models.Account.Credentials>(GlobalService.CredentialsKey);

            return new LoginDto
            {
                AccountNumber = credentials.AccountNumber,
                ApiInstanceId = GlobalService.ApiInstanceId,
                Password = credentials.Password
            };
        }

        protected async Task<Tuple<string, DateTimeOffset>> GetCurrentAccessToken()
        {
            var savedAccessToken = await SettingsDeserializerDep.GetDataAsync<Models.Account.AccessToken>(GlobalService.AccessTokenKey);

            return new Tuple<string, DateTimeOffset>(savedAccessToken.Token, savedAccessToken.DateTimeCreated);
        }

        private bool IsTokenExpired(DateTimeOffset tokenDateTime) => tokenDateTime + GlobalService.ValidityTimeToken <= DateTimeOffset.Now;
    }
}
