using NabuhEnergyMobile.Models.Account;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Login
{
    public class LoginResultDto
    {
        [JsonProperty("token")]
        public string Token { get; set; }

        [JsonProperty("account")]
        public AccountDto Account { get; set; }
    }
}
