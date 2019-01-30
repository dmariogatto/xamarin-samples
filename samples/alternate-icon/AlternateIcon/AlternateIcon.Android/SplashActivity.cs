using System;
using Android.App;
using Android.OS;
using Android.Content;
using Android.Support.V7.App;

namespace AlternateIcon.Droid
{
    [Activity(Name = "com.dgatto.alternateicon.SplashActivity",
              Label = "Alternate Icon",
              Icon = "@drawable/icon",
              Theme = "@style/SplashTheme",
              MainLauncher = true)]
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