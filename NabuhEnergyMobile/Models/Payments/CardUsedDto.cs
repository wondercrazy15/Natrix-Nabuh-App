using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Payments
{
	public class CardUsedDto
	{
		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
