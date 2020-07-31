using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Login;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.Password
{
    public class PasswordService : IPasswordService
    {
        public IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<bool> ResetPassowrdAsync(ForgottenPasswordDto forgottenPassword)
        {
            var forgottenPasswordResult = await RequestProviderDep.PostAsync<ForgottenPasswordDto>(UriHelper.BuildRequestUri(GlobalService.ReminderEndpoint), forgottenPassword);

            return forgottenPasswordResult;
        }

        public async Task<bool> UpdateNewPassowrdAsync(ResetPasswordDto resetPassword)
        {
            var resetPasswordResult = await RequestProviderDep.PostAsync<ResetPasswordDto, AccountDto>(UriHelper.BuildRequestUri(GlobalService.ResetPasswordEndpoint), resetPassword);

            return resetPassword != null;
        }
    }
}
