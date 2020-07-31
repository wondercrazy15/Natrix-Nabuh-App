using Newtonsoft.Json;

namespace NabuhEnergyMobile.Models.Profile
{
	public class EditProfileDto
	{
		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("mobile")]
		public string Mobile { get; set; }

		[JsonProperty("newpassword")]
		public string NewPassword { get; set; }

		[JsonProperty("currentPassword")]
		public string CurrentPassword { get; set; }
	}
}
