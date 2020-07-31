using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Account
{
    public class AccountDto
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("firstname")]
        public string Firstname { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("houseNumber")]
        public string HouseNumber { get; set; }

        [JsonProperty("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonProperty("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonProperty("addressLine3")]
        public string AddressLine3 { get; set; }

        [JsonProperty("town")]
        public string Town { get; set; }

        [JsonProperty("county")]
        public string County { get; set; }

        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("accountNumber")]
        public string AccountNumber { get; set; }

        //[JsonProperty("dateOfBirth")]
        //public DateTime? DateOfBirth { get; set; }

        [JsonProperty("reference2")]
        public string Reference2 { get; set; }

        [JsonProperty("reference3")]
        public string Reference3 { get; set; }

        [JsonProperty("account2")]
        public string Account2 { get; set; }

        [JsonProperty("account3")]
        public string Account3 { get; set; }

        [JsonProperty("balance")]
        public double? Balance { get; set; }

        [JsonProperty("balance2")]
        public double? Balance2 { get; set; }

        [JsonProperty("balance3")]
        public double? Balance3 { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("mobile")]
        public string Mobile { get; set; }

        [JsonProperty("allowCreditCard")]
        public bool AllowCreditCard { get; set; }

        [JsonProperty("allowDebitCard")]
        public bool AllowDebitCard { get; set; }

        [JsonProperty("meter_Balance")]
        public double? MeterBalance { get; set; }

        [JsonProperty("meter_Balance_Gas")]
        public double? MeterBalanceGas { get; set; }

        [JsonProperty("meter_balance_electricity")]
        public double? MeterBalanceElec { get; set; }

        [JsonProperty("tariff_name")]
        public string TariffName { get; set; }

        [JsonProperty("tariff_name_gas")]
        public string Tariff_Name_Gas { get; set; }

        [JsonProperty("tariff_name_energy")]
        public string Tariff_Name_Energy { get; set; }

        [JsonProperty("gas_id")]
        public double? GasId { get; set; }

        [JsonProperty("electricity_id")]
        public double? ElectricityId { get; set; }

        [JsonProperty("gas_usage_value_pounds")]
        public double? GasUsageValuePounds { get; set; }

        [JsonProperty("elec_usage_value_pounds")]
        public double? ElecUsageValuePounds { get; set; }

        [JsonProperty("debt")]
        public double? Debt { get; set; }
    }
}
