using System;

namespace NabuhEnergyMobile.Models.Register
{
    public class Registration
    {
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public string HouseNumber { get; set; }

        public string Postcode { get; set; }

        public string Mobile { get; set; }

        public string AccountNumber { get; set; }

        public DateTime? DateOfBirth { get; set; }
    }
}
