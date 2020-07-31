using System.Net.Http;
using System.Threading.Tasks;
using NabuhEnergyMobile.Utils.Exceptions;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;
using Plugin.Connectivity;

namespace NabuhEnergyMobile.Services.RequestProvider
{
    public abstract class BaseRequestProviderService : IBaseRequestProvider
    {
        public async Task CheckInternetConnectionAndServiceAvailabilityAsync()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                throw new NotConnectedException(GlobalStrings.NoInternetConnection);
            }

            if (!await CrossConnectivity.Current.IsRemoteReachable(GlobalService.ServiceCheckURL))
            {
                throw new ServiceIsNotAvailableException(GlobalStrings.ServiceIsDown);
            }
        }
        public async Task CheckSuccessHttpStatusAsync(HttpResponseMessage result, string currentUri = "")
        {
            string content = await result.Content.ReadAsStringAsync();

            if (!result.IsSuccessStatusCode)
            {
                switch (result.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadGateway:
                        throw new RequestException(content);
                    case System.Net.HttpStatusCode.Forbidden:
                        throw new RequestException(content);
                    case System.Net.HttpStatusCode.Unauthorized: //401
                        throw new AccountNotFoundException(content);
                    case System.Net.HttpStatusCode.NotFound: //404
                        var isAccountNotFound = (UriHelper.BuildRequestUri(GlobalService.LoginEndpoint) == currentUri) ||
                                                (UriHelper.BuildRequestUri(GlobalService.AccountLookupEndpoint) == currentUri) ||
                                                (UriHelper.BuildRequestUri(GlobalService.RequestTokenEndpoint) == currentUri);
                        if (isAccountNotFound)
                        {
                            throw new AccountNotFoundException(content);
                        }

                        throw new RequestException(content);
                    case System.Net.HttpStatusCode.Conflict: //409
                        throw new RequestException(content);
                    case System.Net.HttpStatusCode.BadRequest: //400
                        throw new RequestException(content);
                    case System.Net.HttpStatusCode.InternalServerError: //500
                        throw new RequestException(content);
                    default:
                        throw new UnknownRequestException(GlobalStrings.UnkownHttpIssue);
                }
            }
        }
    }
}
