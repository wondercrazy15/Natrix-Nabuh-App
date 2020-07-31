using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
	public class PaymentDetailsDto
	{
		[JsonProperty("paymentId")]
		public int PaymentId { get; set; }
	}
}
