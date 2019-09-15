using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Support.V4.App;
using Android.Util;
using Firebase.Messaging;

namespace AlternateIcon.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            try
            {
                if (message == null)
                    return;
                if (message.Data == null)
                    return;

                var body = message.GetNotification()?.Body;
                var title = message.GetNotification()?.Title;

                SendNotification(title, body);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void SendNotification(string messageTitle, string messageBody)
        {
            try
            {
                var intent = new Intent(this, typeof(SplashActivity));                
                intent.AddFlags(ActivityFlags.ClearTop | ActivityFlags.SingleTop);
                var pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

                var builder = new NotificationCompat.Builder(ApplicationContext, string.Empty)
                .SetContentTitle(messageTitle)
                .SetSmallIcon(Resource.Drawable.xam)
                .SetContentText(messageBody)
                .SetContentIntent(pendingIntent)
                .SetAutoCancel(true);

                var notification = builder.Build();
                var notificationManager = (NotificationManager)ApplicationContext.GetSystemService(NotificationService);
                notificationManager.Notify(0, notification);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }
    }
}