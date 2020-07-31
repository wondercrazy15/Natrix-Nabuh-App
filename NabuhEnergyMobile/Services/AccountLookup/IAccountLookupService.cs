using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Token;

namespace NabuhEnergyMobile.Services.AccountLookup
{
	public interface IAccountLookupService
	{
		Task<AccountDto> GetAccountInfoAsync();
        Task<bool> UpdateToken(UpdateTokenDto updateToken);
    }
}
