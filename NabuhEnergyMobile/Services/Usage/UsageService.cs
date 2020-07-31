using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Usage;
using NabuhEnergyMobile.Services.AccessToken;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.Usage
{
    public class UsageService : AccessTokenService, IUsageService
    {
        public new IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<List<UserUsage>> GetUsageHistoryAsync()
        {
            try
            {
                await CheckCurrentAccessTokenAsync();

                string validAccessToken = (await GetCurrentAccessToken()).Item1;

                var Result = await RequestProviderDep.GetAsync<UsageResultDto>(
                    UriHelper.BuildRequestUri(GlobalService.AccountUsageEndpoint),
                    validAccessToken);
                if (Result != null)
                {
                    return Result.UsageList.ToList();
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
           
        }

        
    }
}
