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
                var chickenActivityAlias = new Android.Content.ComponentName(this, "com.dgatto.alternateicon.SplashActivity.Chicken");
                var cactusActivityAlias = new Android.Content.ComponentName(this, "com.dgatto.alternateicon.SplashActivity.Cactus");

                // ComponentEnableOption.DontKillApp (i.e. kill app in five seconds)

                switch (icon)
                {
                    case AppIcon.Chicken:
                        // Disable the launcher activity and enable it's alias
                        PackageManager.SetComponentEnabledSetting(
                            chickenActivityAlias,
                            ComponentEnabledState.Enabled,
                            ComponentEnableOption.DontKillApp);
                        PackageManager.SetComponentEnabledSetting(
                            cactusActivityAlias,
                            ComponentEnabledState.Disabled,
                            ComponentEnableOption.DontKillApp);
                        break;
                    case AppIcon.Cactus:
                        PackageManager.SetComponentEnabledSetting(
                            cactusActivityAlias,
                            ComponentEnabledState.Enabled,
                            ComponentEnableOption.DontKillApp);
                        PackageManager.SetComponentEnabledSetting(
                            chickenActivityAlias,
                            ComponentEnabledState.Disabled,
                            ComponentEnableOption.DontKillApp);
                        break;
                    default:                        
                        break;
                }
            };

            LoadApplication(altIconApp);
        }
    }
}