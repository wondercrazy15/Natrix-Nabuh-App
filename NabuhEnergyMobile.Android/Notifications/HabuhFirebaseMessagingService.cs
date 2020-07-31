using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Media;
using Android.OS;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;
using NabuhEnergyMobile.Droid;
using System;
using System.Collections.Generic;

namespace NabuhEnergy.Mobile.Droid.Notifications
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class HabuhFirebaseMessagingService : FirebaseMessagingService
    {
        private const string TAG = "MyFirebaseMsgService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Log.Debug(TAG, "From: " + message.From);

            var body = message.GetNotification().Body;
            Log.Debug(TAG, "Notification Message Body: " + body);
            SendNotification(body, message.Data, message.GetNotification().Title);
        }

        private void SendNotification(string messageBody, IDictionary<string, string> data,string title)
        {
            try
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.AddFlags(ActivityFlags.ClearTop);
                foreach (var key in data.Keys)
                {
                    intent.PutExtra(key, data[key]);
                }

                
                //var notificationBuilder = new NotificationCompat.Builder(this, MainActivity.CHANNEL_ID)
                //                          .SetSmallIcon(Resource.Drawable.nabuh_energy_icon)
                //                          .SetContentTitle("Nabuh Energy")
                //                          .SetContentText(messageBody)
                //                          .SetAutoCancel(true)
                //                          .SetContentIntent(pendingIntent);

                //var notificationManager = NotificationManagerCompat.From(this);
                //notificationManager.Notify(MainActivity.NOTIFICATION_ID, notificationBuilder.Build());

                NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(this);

               var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.UpdateCurrent | PendingIntentFlags.OneShot);

                    Random random = new Random();
                    int notifyID = random.Next(9999 - 1000) + 1000;


                    String CHANNEL_ID = "my_channel_01";

                    var notificationManager = NotificationManager.FromContext(this);


                    notificationBuilder.SetContentTitle(title)
                          .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                          .SetContentText(messageBody)
                          .SetPriority((int)NotificationImportance.High)
                          .SetStyle(new NotificationCompat.BigTextStyle().BigText(messageBody))
                          .SetAutoCancel(true)
                          .SetChannelId(CHANNEL_ID)
                          .SetBadgeIconType(NotificationCompat.BadgeIconLarge)
                          .SetContentIntent(pendingIntent);

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                    {
                        notificationBuilder.SetSmallIcon(Resource.Mipmap.ic_launcher);
                        notificationBuilder.SetColor(Resources.GetColor(Resource.Color.colorPrimaryDark));
                        NotificationChannel mChannel = new NotificationChannel(CHANNEL_ID, "nabuh", NotificationImportance.High);
                        mChannel.SetShowBadge(true);
                        notificationManager.CreateNotificationChannel(mChannel);
                    }
                    else
                    {
                        notificationBuilder.SetSmallIcon(Resource.Mipmap.ic_launcher);
                        //ME.Leolin.Shortcutbadger.ShortcutBadger.ApplyCount(this, Settings.SettingNotificationCount);
                    }
                    notificationBuilder.SetLargeIcon(BitmapFactory.DecodeResource(Resources, Resource.Mipmap.ic_launcher));
                    notificationManager.Notify(notifyID, notificationBuilder.Build());

                }
            catch (System.Exception ex)
            {

            }
           
        }
    }
}
