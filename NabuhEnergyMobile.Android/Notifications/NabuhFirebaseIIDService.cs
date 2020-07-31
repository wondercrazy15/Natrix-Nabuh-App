using Android.App;
using Android.Util;
using Firebase.Iid;
using NabuhEnergyMobile.Utils.Helpers;

namespace NabuhEnergy.Mobile.Droid.Notifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class NabuhFirebaseIIDService : FirebaseInstanceIdService
    {
        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug("FirebaseIIDService", "Refreshed token: " + refreshedToken);
            SendRegistrationToServer(refreshedToken);
        }

        private void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
            UserSettings.AccesToken = token;
        }
   }
}
