using System.Threading.Tasks;

namespace NabuhEnergyMobile.Services.RequestProvider
{
	public interface IRequestProvider : IBaseRequestProvider
	{
		Task<TResult> GetAsync<TResult>(string uri, string token = "");

		Task<bool> GetAsync(string uri, string token = "");

		Task<TResult> PostAsync<TInput, TResult>(string uri, TInput data, string token = "", string header = "");

		Task<bool> PostAsync<TInput>(string uri, TInput data, string token = "", string header = "");

		Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret);

		Task<TResult> PutAsync<TInput, TResult>(string uri, TInput data, string token = "", string header = "");

		Task<TResult> PutAsync<TResult>(string uri, string data, string token = "", string header = "");

		Task<bool> PutAsync<TInput>(string uri, TInput data, string token = "", string header = "");

		Task<bool> DeleteAsync(string uri, string token = "");

		Task<TResult> DeleteAsync<TResult>(string uri, string token = "");
	}
}
