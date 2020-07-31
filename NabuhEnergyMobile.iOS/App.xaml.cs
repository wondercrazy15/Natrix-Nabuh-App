using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NabuhEnergyMobile.Services;
using NabuhEnergyMobile.Views;
using NabuhEnergyMobile.Services.Navigation;
using NabuhEnergyMobile.Services.OpenURL;
using NabuhEnergyMobile.Services.Login;
using NabuhEnergyMobile.Services.Dialog;
using NabuhEnergyMobile.Services.DataStore;
using NabuhEnergyMobile.Services.RequestProvider;
using NabuhEnergyMobile.Values;
using NabuhEnergyMobile.Services.Exceptions;
using NabuhEnergyMobile.Services.AccountLookup;
using NabuhEnergyMobile.Services.EditProfile;
using NabuhEnergyMobile.Services.Cards;
using Plugin.FirebasePushNotification;
using NabuhEnergyMobile.Utils.Helpers;

namespace NabuhEnergyMobile
{
    public partial class App : Application
    {

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjgzMjYxQDMxMzgyZTMyMmUzMGowOXBLR3VvSWRpU3JTaUl5dFdPeEY0aGxtSkR6NTNJWVYySTg1UVVIOEU9");
            Syncfusion.SfChart.XForms.iOS.Renderers.SfChartRenderer.Init();
            XF.Material.Forms.Material.Init(this);
            InitializeComponent();

            InitNavigation();

        }

        private static void InitNavigation()
        {
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
                SendTokentoServer(p.Token);
            };

            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {

                System.Diagnostics.Debug.WriteLine("Received");

            };

            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }


            };

            if (Current.Properties.ContainsKey(GlobalService.AccessTokenKey))
            {
                Current.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                Current.MainPage = new NavigationPage(new LoginPage());
            }


        }

        private static void SendTokentoServer(string token)
        {
            UserSettings.AccesToken = token;
        }

        protected override void OnStart()
        {
            //// Handle when your app starts
            //CrossFirebasePushNotification.Current.Subscribe("general");
            //CrossFirebasePushNotification.Current.OnTokenRefresh += async (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine($"TOKEN REC: {p.Token}");

            //    Application.Current.Properties[GlobalService.FirebaseTokenKey] = p.Token;

            //    await Application.Current.SavePropertiesAsync();
            //};
            //System.Diagnostics.Debug.WriteLine($"TOKEN: {CrossFirebasePushNotification.Current.Token}");

            //CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Received");
            //};

            //CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            //{
            //    //System.Diagnostics.Debug.WriteLine(p.Identifier);

            //    System.Diagnostics.Debug.WriteLine("Opened");
            //    foreach (var data in p.Data)
            //    {
            //        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //    }
            //};

            //CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Action");

            //    if (!string.IsNullOrEmpty(p.Identifier))
            //    {
            //        System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
            //        foreach (var data in p.Data)
            //        {
            //            System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
            //        }

            //    }

            //};

            //CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) =>
            //{
            //    System.Diagnostics.Debug.WriteLine("Dismissed");
            //};
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
