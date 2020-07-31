using System.Collections.Generic;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.History;
using NabuhEnergyMobile.Models.Payments;

namespace NabuhEnergyMobile.Services.Payments
{
	public interface IPaymentService
	{
		Task<int> CreateTokenisedPaymentAsync(CreateTokenisedDto createPayment);

		Task<bool> CheckTokenisedPaymentStatusAsync(int paymentId);

		Task<List<Payment>> GetPaymentHistoryAsync(PaymentHistoryDto paymentHistory);

		Task<PaymentRecordDto> GetPaymentDetailsAsync(int paymentId);
	}
}
