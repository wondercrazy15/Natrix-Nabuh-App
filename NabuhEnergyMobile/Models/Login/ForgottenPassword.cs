using NabuhEnergyMobile.Utils.Enums;

namespace NabuhEnergyMobile.Models.Login
{
    public class ForgottenPassword
    {
        public string Email { get; set; }

        public string Mobile { get; set; }

        public MessageTypeEnum MessageType { get; set; }

        public string AccountNumber { get; set; }
    }
}
