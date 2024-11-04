using Android.App;
using Android.Content.PM;
using Android.App.Roles;
using Android.Content;

using Android.OS;
using Android.Views;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;

using MauiAppBSException.Platforms.Android;

namespace MauiAppBSException
{

    [Activity(Theme = "@style/Maui.SplashTheme", LaunchMode = LaunchMode.SingleTask, WindowSoftInputMode = SoftInput.StateAlwaysVisible, MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    [IntentFilter(new string[] { "android.intent.action.VIEW", "android.intent.action.DIAL" }, Categories = new[] { "android.intent.category.DEFAULT", "android.intent.category.BROWSABLE" }, DataScheme = "tel")]
    [IntentFilter(new string[] { "android.intent.action.DIAL" }, Categories = new[] { "android.intent.category.DEFAULT" })]


    //[Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity Instance { get; set; }
        public void StartService()
        {
            var serviceIntent = new Intent(this, typeof(MyBackgroundService));
            serviceIntent.PutExtra("inputExtra", "Background Service");
            StopService(serviceIntent);
            StartService(serviceIntent);
        }

        public void StopService()
        {
            var serviceIntent = new Intent(this, typeof(MyBackgroundService));
            StopService(serviceIntent);

        }
        public MainActivity()
        {
            //Instance = this;
            AndroidServiceManager.MainActivity = this;
        }

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Instance = this;

        }
    }
}
