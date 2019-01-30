using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace AlternateIcon.Droid
{
    [Activity(Label = "Alternate Icon",
              Icon = "@drawable/icon",
              Theme = "@style/MainTheme", 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var altIconApp = new AlternateIcon.App();
            altIconApp.AppIconChanged += (sender, icon) =>
            {
                var splashActivity = new Android.Content.ComponentName(this, "com.dgatto.alternateicon.SplashActivity");
                var splashActivityAlias = new Android.Content.ComponentName(this, "com.dgatto.alternateicon.SplashActivityAlias");

                // ComponentEnableOption.DontKillApp
                // (i.e. don't kill app immediately, just wait a seconds)

                switch (icon)
                {
                    case AppIcon.Chicken:
                        PackageManager.SetComponentEnabledSetting(
                            splashActivity,
                            ComponentEnabledState.Enabled,
                            ComponentEnableOption.DontKillApp);
                        PackageManager.SetComponentEnabledSetting(
                            splashActivityAlias,
                            ComponentEnabledState.Disabled,
                            ComponentEnableOption.DontKillApp);
                        break;
                    case AppIcon.Cactus:
                        PackageManager.SetComponentEnabledSetting(
                            splashActivityAlias,
                            ComponentEnabledState.Enabled,
                            ComponentEnableOption.DontKillApp);
                        PackageManager.SetComponentEnabledSetting(
                            splashActivity,
                            ComponentEnabledState.Disabled,
                            ComponentEnableOption.DontKillApp);
                        break;
                    default:                        
                        break;
                }

                // Only way to stop app getting killed is to launch a new Activity
                var intent = new Android.Content.Intent(this, typeof(MainActivity));
                intent.AddFlags(Android.Content.ActivityFlags.ClearTop);
                intent.SetFlags(Android.Content.ActivityFlags.NewTask | Android.Content.ActivityFlags.ClearTask);
                Finish();
                StartActivity(intent);
            };

            LoadApplication(altIconApp);
        }
    }
}