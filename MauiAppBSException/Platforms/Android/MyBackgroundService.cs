using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Provider;
using AndroidX.Core.App;
//using Microsoft.AspNetCore.SignalR.Client;
using Android.Util;
using Java.Lang;
using Android.Content.PM;
using Android.Widget;
using Android.App.Admin;

//using Java.Util;


namespace MauiAppBSException.Platforms.Android;

[Service(Exported = true, ForegroundServiceType = global::Android.Content.PM.ForegroundService.TypeSpecialUse)]
internal class MyBackgroundService : Service
{
    bool _batteryAlarmSent = false;

    Timer timer = null;
    int myId = new object().GetHashCode();
    int BadgeNumber = 0;
    private readonly IBinder binder = new LocalBinder();
    NotificationCompat.Builder notification;
    //HubConnection hubConnection;

    public class LocalBinder : Binder
    {
        public MyBackgroundService GetService()
        {
            return this.GetService();
        }
    }

    public override IBinder OnBind(Intent intent)
    {
        return binder;
    }

    public override StartCommandResult OnStartCommand(Intent intent,
        StartCommandFlags flags, int startId)
    {
        CheckAndOpenUsageAccessSettings();

        var input = intent?.GetStringExtra("inputExtra");
        sendNotification(input);


        // timer to ensure hub connection
        timer = new Timer(Timer_Elapsed, notification, 0, 3000);

        // You can stop the service from inside the service by calling StopSelf();

        return StartCommandResult.Sticky;
    }

    void sendNotification(string currentApp)
    {

        Log.Info("MauiAppBS bs", $"time {DateTime.Now.ToString()} sendNotification, restarting {currentApp}");

        var notificationIntent = new Intent(this, typeof(MainActivity));
        notificationIntent.SetAction("USER_TAPPED_NOTIFIACTION");

        var pendingIntent = PendingIntent.GetActivity(this, 0, notificationIntent,
            PendingIntentFlags.Immutable);

        // Increment the BadgeNumber
        BadgeNumber++;

        notification = new NotificationCompat.Builder(this,
                MainApplication.CHANNEL_ID)
        .SetContentText(currentApp)
            .SetSmallIcon(Microsoft.Maui.Resource.Drawable.alarmalert)
            .SetAutoCancel(false)
            .SetContentTitle("MauiAppBS Backgroud Service started")
            .SetPriority(NotificationCompat.PriorityDefault)
            .SetContentIntent(pendingIntent);

        notification.SetNumber(BadgeNumber);
        int sdkVersion = (int)Build.VERSION.SdkInt;
        if (Build.VERSION.SdkInt >= BuildVersionCodes.UpsideDownCake)
        // build and notify
            StartForeground(myId, notification.Build(),ForegroundService.TypeSpecialUse);
        else
        {
            StartForeground(myId, notification.Build());
        }



       




}
    

    /// <summary>
    /// 
    /// </summary>
    /// <param name="state"></param>
    async void Timer_Elapsed(object state)
    {
        try
        {
            AndroidServiceManager.IsRunning = true;


            // Microsoft.Maui.ApplicationModel.Platform.CurrentActivity.StartActivity(new Android.Content.Intent(Android.Provider.Settings.ActionUsageAccessSettings));

            string package = AppInfo.Current.PackageName;

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;

            if (activity == null || activity.IsDestroyed || activity.IsFinishing)
            {
                Log.Error("MauiAppBS bs restarting ", $"time {DateTime.Now.ToString()}");

                OpenPackage(package);
            }

           

            Log.Info("MauiAppBS bs", $"time {DateTime.Now.ToString()} Timer_Elapsed");

         

           
        }
        catch (System.Exception ex)
        {
            //await DisplayAlert("Error", ex.Message, "OK");
            Log.Error("MauiAppBS", $"time {DateTime.Now.ToString()} Timer_Elapsed Error ={ex.Message}");
          

        }
    }

    private void CheckAndOpenUsageAccessSettings()
    {
        var currentActivity = Platform.CurrentActivity;

        if (!Settings.CanDrawOverlays(this))
        {
            
            //TODO ask for permission Overlay
            AlertDialog.Builder builderOverlay = new AlertDialog.Builder(currentActivity);
            builderOverlay.SetTitle("Grant Overlay permissions");
            builderOverlay.SetMessage("Access for Overlay are currently disabled for this app. Would you like to enable them?");
            builderOverlay.SetPositiveButton("Yes", (sender, args) =>
            {
                Intent intent = new Intent();
                intent.SetAction(global::Android.Provider.Settings.ActionManageOverlayPermission);

                // For Android 5-7
                intent.PutExtra(global::Android.Provider.Settings.ExtraAppPackage, PackageName);

                // For Android 8 and above
                intent.PutExtra(global::Android.Provider.Settings.ExtraChannelId, ApplicationInfo.Uid);

                currentActivity.StartActivity(intent);
            });
            builderOverlay.SetNegativeButton("No", (sender, args) =>
            {
            });

            AlertDialog overlayDialog = builderOverlay.Create();
            overlayDialog.Show();
        }
        else
        {

        }
    }

    void OpenPackage(string packageName)
    {
        var appScreen = new Intent(this, typeof(MainActivity));
        appScreen.SetFlags(ActivityFlags.NewTask | ActivityFlags.BroughtToFront    );
        //appScreen.SetFlags(ActivityFlags.SingleTop | ActivityFlags.BroughtToFront);
        appScreen.SetAction("START_ACTION");

        //lockScreen.PutExtra("callUserObject", JsonConvert.SerializeObject(CallUserObject));
        appScreen.PutExtra("packageName", packageName);

        global::Android.App.Application.Context.StartActivity(appScreen);
        //this.StartActivity(appScreen);
    }



    



   

}