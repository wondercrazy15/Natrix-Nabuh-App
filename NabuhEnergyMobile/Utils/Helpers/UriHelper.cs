using System;
using NabuhEnergyMobile.Values;

namespace NabuhEnergyMobile.Utils.Helpers
{
    public static class UriHelper
    {
        public static string BuildRequestUri(string path, string requiredParameter = "")
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = GlobalService.SchemeEndpoint,
                Host = GlobalService.BaseServiceEndpoint,
                Path = path
            };

            if (!String.IsNullOrEmpty(requiredParameter))
            {
                uriBuilder.Query += "?" + requiredParameter;
            }

            return uriBuilder.ToString();
        }
    }
}
