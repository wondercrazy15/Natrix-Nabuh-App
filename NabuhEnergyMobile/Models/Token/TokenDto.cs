using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Token
{
	public class TokenDto
	{
		[JsonProperty("token")]
		public string Token { get; set; }
	}
}
