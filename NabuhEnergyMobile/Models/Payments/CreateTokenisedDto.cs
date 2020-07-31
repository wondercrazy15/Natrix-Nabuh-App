using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
	public class CreateTokenisedDto
	{
		[JsonProperty("reference")]
		public string Reference { get; set; }

		[JsonProperty("amount")]
		public double Amount { get; set; }

		[JsonProperty("tokenisedCard")]
		public TokenisedCard TokenisedCard { get; set; }

		[JsonProperty("emailReceipt")]
		public bool EmailReceipt { get; set; }

		[JsonProperty("smsReceipt")]
		public bool SmsReceipt { get; set; }
	}
}
