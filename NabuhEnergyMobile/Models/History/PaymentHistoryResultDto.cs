using System.Collections.Generic;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.History
{
    public class PaymentHistoryResultDto
    {
        [JsonProperty("payments")]
        public IList<Payment> Payments { get; set; }
    }
}
