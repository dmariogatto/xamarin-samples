using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;

namespace AlternateIcon.Droid
{
    [Activity(
#if DEBUG
        MainLauncher = true,
#endif
        Name = "com.dgatto.alternateicon.SplashActivity",
        Label = "Alternate Icon",
        Theme = "@style/SplashTheme")]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
            Finish();
        }
    }
}