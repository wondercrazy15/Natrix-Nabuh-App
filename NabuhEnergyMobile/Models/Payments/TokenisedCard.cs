using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
    public class TokenisedCard
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("cvv")]
        public string CVC { get; set; }
    }
}
