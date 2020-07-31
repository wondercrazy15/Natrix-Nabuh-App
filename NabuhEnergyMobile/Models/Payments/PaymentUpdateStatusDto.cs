using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
	public class PaymentUpdateStatusDto
	{
		[JsonProperty("paymentId")]
		public int PaymentId { get; set; }

		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("completed")]
		public bool Completed { get; set; }

		[JsonProperty("responseCode")]
		public string ResponseCode { get; set; }

		[JsonProperty("responseMessage")]
		public string ResponseMessage { get; set; }
	}
}
