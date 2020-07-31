using System;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Login;

namespace NabuhEnergyMobile.Services.Login
{
    public interface ILoginService
    {
        Task<LoginResultDto> LoginUserAsync(LoginDto credentials);

        Task LogoutUserAsync();
    }
}
