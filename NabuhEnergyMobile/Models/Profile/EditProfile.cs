using System;

namespace NabuhEnergyMobile.Models.Profile
{
    public class EditProfile
    {
		public string Email { get; set; }

		public string Mobile { get; set; }

		public string CurrentPassword { get; set; }

		public string NewPassword { get; set; }

        public string ConfirmNewPassword { get; set; }
    }
}
