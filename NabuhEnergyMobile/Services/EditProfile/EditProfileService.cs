using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Profile;
using NabuhEnergyMobile.Services.AccessToken;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.EditProfile
{
    public class EditProfileService : AccessTokenService, IEditProfileService
    {
        public new IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();
        public new ISettingsDeserializer SettingsDeserializerDep = new SettingsDeserializer();

        public async Task<bool> UpdateUserAsync(EditProfileDto updateAccount)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var editProfileResult = await RequestProviderDep.PutAsync<EditProfileDto>(UriHelper.BuildRequestUri(GlobalService.UpdateEndpoint),
                                                                 updateAccount, validAccessToken);

            if (!String.IsNullOrEmpty(updateAccount.NewPassword) && editProfileResult)
            {
                await SaveNewPasswordAsync(updateAccount.NewPassword);
            }

            return editProfileResult;
        }

        private async Task SaveNewPasswordAsync(string newPassword)
        {
            var currentCredentials = await SettingsDeserializerDep.GetDataAsync<Credentials>(GlobalService.CredentialsKey);

            currentCredentials.Password = newPassword;

            await SettingsDeserializerDep.SaveDataAsync(currentCredentials, GlobalService.CredentialsKey);
        }
    }
}
