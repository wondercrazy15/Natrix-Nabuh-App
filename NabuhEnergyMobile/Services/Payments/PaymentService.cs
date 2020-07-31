using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NabuhEnergyMobile.Models.History;
using NabuhEnergyMobile.Models.Payments;
using NabuhEnergyMobile.Models.Payments.Result;
using NabuhEnergyMobile.Services.AccessToken;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Utils.Helpers;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Services.Payments
{
    public class PaymentService : AccessTokenService, IPaymentService
    {
        public new IRequestProvider RequestProviderDep = new RequestProvider.RequestProvider();

        public async Task<bool> CheckTokenisedPaymentStatusAsync(int paymentId)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var checkPaymentUpdateStatusResult = await RequestProviderDep.GetAsync(
                UriHelper.BuildRequestUri(GlobalService.PaymentUpdateStatusEndpoint, $"PaymentId={paymentId}"),
                validAccessToken);
            return checkPaymentUpdateStatusResult;
        }

        public async Task<int> CreateTokenisedPaymentAsync(CreateTokenisedDto createPayment)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var tokenisedPaymentResult = await RequestProviderDep.PostAsync<CreateTokenisedDto, CreateTokenisedResultDto>(
                UriHelper.BuildRequestUri(GlobalService.CreatePaymentEndpoint),
                createPayment,
                validAccessToken);
            return Int32.Parse(tokenisedPaymentResult.PaymentId);
        }

        public async Task<List<Payment>> GetPaymentHistoryAsync(PaymentHistoryDto paymentHistory)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var paymentHistoryResult = await RequestProviderDep.GetAsync<PaymentHistoryResultDto>(
                UriHelper.BuildRequestUri(GlobalService.ListOfPaymentsEndpoint),
                validAccessToken);

            return paymentHistoryResult.Payments.ToList();
        }

        public async Task<PaymentRecordDto> GetPaymentDetailsAsync(int paymentId)
        {
            await CheckCurrentAccessTokenAsync();

            string validAccessToken = (await GetCurrentAccessToken()).Item1;

            var paymentDetails = await RequestProviderDep.PostAsync<PaymentDetailsDto, PaymentRecordDto>(
                UriHelper.BuildRequestUri(GlobalService.PaymentDetailsEndpoint),
                new PaymentDetailsDto() { PaymentId = paymentId },
                validAccessToken);

            return paymentDetails;
        }
    }
}
