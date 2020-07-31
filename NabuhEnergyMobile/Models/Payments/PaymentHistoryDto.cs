using System;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
    public class PaymentHistoryDto
    {
        [JsonProperty("dateFrom")]
        public DateTime DateFrom { get; set; }

        [JsonProperty("dateTo")]
        public DateTime DateTo { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("cardUsed")]
        public CardUsedDto CardUsed { get; set; }
    }
}
