using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Register;

namespace NabuhEnergyMobile.Services.Register
{
    public interface IRegistrationService
    {
        Task<bool> RegisterUserAsync(RegistrationDto registerUser);
    }
}
