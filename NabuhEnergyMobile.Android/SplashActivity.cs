using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Content.PM;
using Android.Util;
using System.Threading.Tasks;

namespace NabuhEnergyMobile.Droid
{
    [Activity(Label = "Nabuh Energy",
        Theme = "@style/Theme.Splash",
        NoHistory = true,
        MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashActivity : Activity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task task = new Task(() => StartMainTask());
            task.Start();
        }

        private async void StartMainTask()
        {
            Log.Debug(TAG, "Performing start job");
            await Task.Delay(3000);
            Log.Debug(TAG, "Start job finished");
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
