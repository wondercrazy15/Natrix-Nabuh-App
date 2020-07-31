namespace NabuhEnergyMobile.Models.History
{
    public class PaymentHistory
    {
        public int Id { get; set; }

        public string DateCreated { get; set; }

        public string UtrnCode { get; set; }

        public string EnergySource { get; set; }

        public string PaymentStatus { get; set; }
    }
}
