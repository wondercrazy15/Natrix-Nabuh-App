using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Login
{
    public class ResetPasswordDto
    {
        [JsonProperty("pinCode")]
        public string PinCode { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        [JsonProperty("apiInstanceId")]
        public int ApiInstanceId { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }
    }
}
