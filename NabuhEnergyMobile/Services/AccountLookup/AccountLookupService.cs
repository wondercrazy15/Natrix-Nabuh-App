using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Services.AccessToken;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Models.Token;

namespace NabuhEnergyMobile.Services.AccountLookup
{
    public class AccountLookupService : AccessTokenService, IAccountLookupService
    {
        public new IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<AccountDto> GetAccountInfoAsync()
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var accountInfoResult = await RequestProviderDep.GetAsync<AccountDto>(UriHelper.BuildRequestUri(GlobalService.AccountLookupEndpoint), validAccessToken);

            return accountInfoResult;
        }

        public async Task<bool> UpdateToken(UpdateTokenDto updateToken)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var editProfileResult = await RequestProviderDep.PutAsync<UpdateTokenDto>(UriHelper.BuildRequestUri(GlobalService.UpdateToken),
                                                                 updateToken, validAccessToken);

            return editProfileResult;
        }
    }
}
