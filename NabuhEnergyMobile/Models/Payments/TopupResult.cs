namespace NabuhEnergyMobile.Models.Payments
{
    public class TopupResult
    {
        public int TokenisedPaymentId { get; set; }
        public string PaymentType { get; set; }
        public double Amount { get; set; }
        public bool isSuccess { get; set; }
    }
}
