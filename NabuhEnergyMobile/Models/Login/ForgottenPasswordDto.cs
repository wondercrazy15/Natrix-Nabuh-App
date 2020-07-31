using NabuhEnergyMobile.Utils.Enums;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Login
{
    public class ForgottenPasswordDto
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("messageType")]
        public MessageTypeEnum MessageType { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("apiInstanceId")]
        public int ApiInstanceId { get; set; }
    }
}
