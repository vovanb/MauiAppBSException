﻿using Android.App;
using Android.OS;
using Android.Runtime;

namespace MauiAppBSException
{
    [Application]
    public class MainApplication : MauiApplication
    {
        public static readonly string CHANNEL_ID = "MauiAppBS_NotificationChannel";

        public MainApplication(IntPtr handle, JniHandleOwnership ownership)
            : base(handle, ownership)
        {
        }

        protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
        public override void OnCreate()
        {
            base.OnCreate();

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
#pragma warning disable CA1416
                var serviceChannel =
                    new NotificationChannel(CHANNEL_ID,
                        "Background Service Channel",
                    NotificationImportance.High);

                if (GetSystemService(NotificationService)
                    is NotificationManager manager)
                {
                    manager.CreateNotificationChannel(serviceChannel);
                }
#pragma warning restore CA1416
            }
        }

    }
}
