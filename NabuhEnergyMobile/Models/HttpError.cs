using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models
{
	public class HttpError
	{
		[JsonProperty("code")]
		public int Code { get; set; }

		[JsonProperty("message")]
		public string Message { get; set; }
	}
}
