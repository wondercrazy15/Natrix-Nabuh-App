using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Register;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Services.RequestProvider;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Services.Register
{
    public class RegistrationService : IRegistrationService
    {
        public IRequestProvider RequestProviderDep => DependencyService.Get<IRequestProvider>() ?? new RequestProvider.RequestProvider();

        public async Task<bool> RegisterUserAsync(RegistrationDto registerUser)
        {
            var registrationResult = await RequestProviderDep.PostAsync<RegistrationDto, RegistrationResultDto>(UriHelper.BuildRequestUri(GlobalService.CreateAccountEndpoint), registerUser);

            return registrationResult != null;
        }
    }
}
