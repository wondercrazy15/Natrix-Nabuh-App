using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
	public class BasketDto
	{
		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
