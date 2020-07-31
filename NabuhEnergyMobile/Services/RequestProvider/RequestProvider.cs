using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using NabuhEnergyMobile.Utils.Exceptions;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace NabuhEnergyMobile.Services.RequestProvider
{
    public class RequestProvider : BaseRequestProviderService, IRequestProvider
    {
        private readonly JsonSerializerSettings _serializerSettings;

        public RequestProvider()
        {
            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatString = "dd/MM/yyy",
                NullValueHandling = NullValueHandling.Ignore
            };
            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        public async Task<TResult> GetAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await CheckSuccessHttpStatusAsync(response, uri);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
              JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<bool> GetAsync(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.GetAsync(uri);

            await CheckSuccessHttpStatusAsync(response);

            return response.IsSuccessStatusCode;
        }

        public async Task<TResult> PostAsync<TInput, TResult>(string uri, TInput data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response, uri);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<bool> PostAsync<TInput>(string uri, TInput data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response);

            return response.IsSuccessStatusCode;
        }

        public async Task<TResult> PostAsync<TResult>(string uri, string data, string clientId, string clientSecret)
        {
            HttpClient httpClient = CreateHttpClient(string.Empty);

            if (!string.IsNullOrWhiteSpace(clientId) && !string.IsNullOrWhiteSpace(clientSecret))
            {
                AddBasicAuthenticationHeader(httpClient, clientId, clientSecret);
            }

            var content = new StringContent(data);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PostAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<TResult> PutAsync<TInput, TResult>(string uri, TInput data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<TResult> PutAsync<TResult>(string uri, string data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(data);

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        public async Task<bool> PutAsync<TResult>(string uri, TResult data, string token = "", string header = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data, _serializerSettings));

            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.PutAsync(uri, content);

            await CheckSuccessHttpStatusAsync(response);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.DeleteAsync(uri);

            await CheckSuccessHttpStatusAsync(response);

            return response.IsSuccessStatusCode;
        }

        public async Task<TResult> DeleteAsync<TResult>(string uri, string token = "")
        {
            HttpClient httpClient = CreateHttpClient(token);

            await CheckInternetConnectionAndServiceAvailabilityAsync();

            HttpResponseMessage response = await httpClient.DeleteAsync(uri);

            await CheckSuccessHttpStatusAsync(response);

            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized));

            return result;
        }

        private HttpClient CreateHttpClient(string token = "")
        {
            var httpClient = new HttpClient(new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip
            });

            httpClient.DefaultRequestHeaders.Accept.Clear();

            httpClient.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(token))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return httpClient;
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private void AddBasicAuthenticationHeader(HttpClient httpClient, string clientId, string clientSecret)
        {
            if (httpClient == null)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(clientId) || string.IsNullOrWhiteSpace(clientSecret))
            {
                return;
            }

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(clientId, clientSecret);
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }
    }
}
