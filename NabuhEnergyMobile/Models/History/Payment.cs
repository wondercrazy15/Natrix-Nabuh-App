using System;
using NabuhEnergyMobile.Models.Payments;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.History
{
    public class Payment
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("dateCreated")]
        public string DateCreated { get; set; }

        [JsonProperty("cardUsed")]
        public CardUsed CardUsed { get; set; }

        [JsonProperty("basketID")]
        public int BasketID { get; set; }

        [JsonProperty("orderDetails")]
        public string OrderDetails { get; set; }

        [JsonProperty("amount")]
        public double Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("energySource")]
        public string EnergySource { get; set; }

        [JsonProperty("meterNumber")]
        public string MeterNumber { get; set; }

        [JsonProperty("utrnCode")]
        public string UtrnCode { get; set; }

        //[JsonProperty("basket")]
        //public Basket Basket { get; set; }

        [JsonProperty("paymentResult")]
        public PaymentUpdateStatusDto PaymentStatus { get; set; }
    }
}
