using System;
using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Usage
{
    public class UserUsage
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("account_number")]
        public long AccountNumber { get; set; }

        [JsonProperty("fuel_type")]
        public string FuelType { get; set; }

        [JsonProperty("report_date")]
        public DateTimeOffset ReportDate { get; set; }

        [JsonProperty("gas_cumulative")]
        public double GasCumulative { get; set; }

        [JsonProperty("electric_cumulative0")]
        public double ElectricCumulative0 { get; set; }

        [JsonProperty("electric_cumulative1")]
        public long ElectricCumulative1 { get; set; }
    }

    public class DailyReportData
    {
        public string workedDate { get; set; }
        public string GasCumulative1 { get; set; }
        public double GasCumulative { get; set; }
        public double ElectricCumulative0 { get; set; }
       
    }
}
