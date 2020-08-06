using System;

namespace NabuhEnergyMobile.Values
{
    public static class GlobalService
    {
        #region BD Keys

        public const string AccessTokenKey = nameof(AccessTokenKey);

        public const string FirebaseTokenKey = nameof(FirebaseTokenKey);

        public const string CredentialsKey = nameof(CredentialsKey);

        #endregion BD Keys

        #region Service Endpoints

        public const string SchemeEndpoint = "http";

        #region Testing Endpoints
       // public const string BaseServiceEndpoint = "keyivr-api-int.keyivr.co.uk/v1.1";

       // public const string ServiceCheckURL = "http://keyivr-api-int.keyivr.co.uk";

       // public const int ApiInstanceId = 1;

        #endregion

        #region Live Endpoints
          public const string BaseServiceEndpoint = "nabuh-api.keyivr.com/v1.1";

          public const string ServiceCheckURL = "http://nabuh-api.keyivr.com";

          public const int ApiInstanceId = 2;
        #endregion
        public static TimeSpan ValidityTimeToken { get; } = TimeSpan.FromMinutes(15);

        //public const string SchemeEndpoint = "https";

        //public const string BaseServiceEndpoint = "api.keyivr.com/v1.1";

        //public const string ServiceCheckURL = "https://api.keyivr.com";

        #region Status

        //This operation sends a heartbeat to the server
        public const string PingEndoint = "/ping";

        //Displays the API version information
        public const string VersionEndpoint = "/version";

        //Displays the current status of the API server
        public const string StatusEndpoint = "/status";

        #endregion Status

        #region Accounts

        public const string LoginEndpoint = "/account/tokenaccount";

        public const string ReminderEndpoint = "/account/reminder";

        public const string CreateAccountEndpoint = "/account/create";

        public const string UpdateEndpoint = "/account/update";

        public const string UpdateToken = "/account/updatefirebasetoken";

        public const string RequestTokenEndpoint = "/account/token";

        public const string ResetPasswordEndpoint = "/account/passwordReset";

        public const string AccountLookupEndpoint = "/account/lookup";

        public const string AccountUsageEndpoint = "/account/usage";


         
        #endregion Accounts

        #region Cards

        public const string CreateCardEndpoint = "/card/create";

        public const string DeleteCardEndpoint = "/card/delete";

        public const string ListOfCardsEndpoint = "/card/list";

        public const string SetPreferredCardEndpoint = "/card/setPreferred";

        #endregion Cards

        #region Payments

        public const string CreatePaymentEndpoint = "/payment/createtokenised";

        public const string ListOfPaymentsEndpoint = "/payment/history";

        public const string PaymentDetailsEndpoint = "/payment/history/details";

        public const string PaymentUpdateStatusEndpoint = "/payment/updatestatus";

        #endregion Payments

        #endregion Service Endpoints
    }
}