using System;
using System.Threading.Tasks;

namespace NabuhEnergyMobile.Services.Exceptions
{
	public interface IHandleExceptionService
	{
		Task<string> HandleExceptionAsync(Exception exception, bool isRequiredToLogout = false);
	}
}
