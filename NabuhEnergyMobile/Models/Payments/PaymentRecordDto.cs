using System;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
    public class PaymentRecordDto
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("dateCreated")]
        public String DateCreated { get; set; }

        [JsonProperty("cardUsed")]
        public CardUsedDto CardUsed { get; set; }

        [JsonProperty("card_number")]
        public string CardNumber { get; set; }

        [JsonProperty("basketID")]
        public int? BasketId { get; set; }

        [JsonProperty("amount")]
        public double? Amount { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("status")] // Should be an enum
        public string Status { get; set; }

        [JsonProperty("energySource")]
        public string EnergySource { get; set; }

        [JsonProperty("meterNumber")]
        public string MeterNumber { get; set; }

        [JsonProperty("meterId")]
        public string MeterId { get; set; }

        [JsonProperty("tarifName")]
        public string TarifName { get; set; }

        [JsonProperty("utrnCode")]
        public string UtrnCode { get; set; }

        [JsonProperty("basket")]
        public BasketDto Basket { get; set; }

        [JsonProperty("utrn_number")]
        public string UtrnNumber { get; set; }

        [JsonProperty("paymentResult")]
        public PaymentUpdateStatusDto PaymentStatus { get; set; }
    }
}
