using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Login
{
    public class LoginDto
    {
        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("apiInstanceId")]
        public int ApiInstanceId { get; set; }
    }
}
