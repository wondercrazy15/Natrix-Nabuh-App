using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Acr.UserDialogs;
using Plugin.FirebasePushNotification;
using Android.Content;
using XF.Material.Forms;
using Xamarin.Forms;

namespace NabuhEnergyMobile.Droid
{
    [Activity(Label = "Nabuh Energy",
        Icon = "@drawable/nabuh_energy_icon",
        Theme = "@style/MainTheme",
        LaunchMode = LaunchMode.SingleTop,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        internal static readonly int NOTIFICATION_ID = 100;
        internal static readonly string CHANNEL_ID = "my_notification_channel";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            UserDialogs.Init(this);
            Forms.SetFlags("SwipeView_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FirebasePushNotificationManager.Initialize(this, true);
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            //Material.dr.Material.Init(this, savedInstanceState);
            XF.Material.Droid.Material.Init(this, savedInstanceState);
           
            LoadApplication(new App());
    
           // FirebasePushNotificationManager.ProcessIntent(this, Intent);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }

    }
}