using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;

using Plugin.FirebasePushNotification;
using ObjCRuntime;
using UserNotifications;
using Firebase.CloudMessaging;
using Xamarin.Forms;

namespace NabuhEnergyMobile.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            try
            {
                Rg.Plugins.Popup.Popup.Init();
                Forms.SetFlags("SwipeView_Experimental");
                global::Xamarin.Forms.Forms.Init();
                XF.Material.iOS.Material.Init();
                ZXing.Net.Mobile.Forms.iOS.Platform.Init();

                LoadApplication(new App());

                FirebasePushNotificationManager.Initialize(options, true);

                FirebasePushNotificationManager.CurrentNotificationPresentationOption = UNNotificationPresentationOptions.Alert | UNNotificationPresentationOptions.Badge;

                CrossFirebasePushNotification.Current.RegisterForPushNotifications();

                UNUserNotificationCenter.Current.Delegate = this;

                
            }
            catch (Exception)
            {

            }
            return base.FinishedLaunching(app, options);

        }

        //[Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        //public void  RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        //}
        [Export("application:didRegisterForRemoteNotificationsWithDeviceToken:")]
        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {
            //base.RegisteredForRemoteNotifications(application, deviceToken);
            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }

        [Export("application:didFailToRegisterForRemoteNotificationsWithError:")]
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            //base.FailedToRegisterForRemoteNotifications(application, error);
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        //public void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        //}

        [Export("application:didReceiveRemoteNotification:fetchCompletionHandler:")]
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            //FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            // Do your magic to handle the notification data
            System.Console.WriteLine(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);
        }

        [Export("userNotificationCenter:willPresentNotification:withCompletionHandler:")]
        public void WillPresentNotification(UNUserNotificationCenter center, UNNotification notification, Action<UNNotificationPresentationOptions> completionHandler)
        {
            completionHandler(UNNotificationPresentationOptions.Alert);
        }

        //public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        //{
        //    FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        //    //FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        //}

        //public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        //{
        //    FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        //}

        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{
        //    FirebasePushNotificationManager.DidReceiveMessage(userInfo);
        //    System.Console.WriteLine(userInfo);
        //    completionHandler(UIBackgroundFetchResult.NewData);
        //}
    }
}
