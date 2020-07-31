using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Register
{
	public class RegistrationDto
	{
		[JsonProperty("houseNumber")]
		public string HouseNumber { get; set; }

		[JsonProperty("postcode")]
		public string Postcode { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("mobile")]
		public string Mobile { get; set; }

		[JsonProperty("dateOfBirth")]
		public string DateOfBirth { get; set; }

		[JsonProperty("accountNumber")]
		public string AccountNumber { get; set; }

		[JsonProperty("password")]
		public string Password { get; set; }

		[JsonProperty("apiInstanceID")]
		public int ApiInstanceID { get; set; }
	}
}
