using System.Net.Http;
using System.Threading.Tasks;

namespace NabuhEnergyMobile.Services.RequestProvider
{
	public interface IBaseRequestProvider
	{
		Task CheckInternetConnectionAndServiceAvailabilityAsync();

		Task CheckSuccessHttpStatusAsync(HttpResponseMessage result, string currentUri = "");
	}
}
