using System;
using NabuhEnergyMobile.Models.Account;
using NabuhEnergyMobile.Models.Payments;

namespace NabuhEnergyMobile.Models.History
{
    public class ReceiptData
    {
        public PaymentRecordDto Payment { get; set; }

        public AccountDto Account { get; set; }
    }
}
