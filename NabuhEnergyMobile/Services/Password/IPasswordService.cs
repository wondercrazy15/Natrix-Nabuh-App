using System.Threading.Tasks;
using NabuhEnergyMobile.Models.Login;

namespace NabuhEnergyMobile.Services.Password
{
	public interface IPasswordService
	{
		Task<bool> ResetPassowrdAsync(ForgottenPasswordDto forgottenPassword);

		Task<bool> UpdateNewPassowrdAsync(ResetPasswordDto resetPassword);
	}
}
