using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments.Result
{
	public class CreateTokenisedResultDto
	{
		[JsonProperty("amount")]
		public double Amount { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("paymentId")]
		public string PaymentId { get; set; }

		[JsonProperty("cardHash")]
		public string CardHash { get; set; }

		[JsonProperty("cardReference")]
		public string CardReference { get; set; }

		[JsonProperty("authCode")]
		public string AuthCode { get; set; }

		[JsonProperty("authMessage")]
		public string AuthMessage { get; set; }
	}
}
