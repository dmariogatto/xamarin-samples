using System;
using System.Linq;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util.Concurrent;

namespace AccountConsumer
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private const int AccountRequestCode = 1234;
        private const string AuthTokenType = "dgatto.authtype";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            var toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            var fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;

            var authButton = FindViewById<Button>(Resource.Id.auth_button);
            authButton.Click += AuthButtonOnClick;
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == AccountRequestCode)
            {
                if (resultCode == Result.Ok)
                {
                    var am = AccountManager.Get(this);
                    var accountName = data.GetStringExtra(AccountManager.KeyAccountName);
                    var accountType = data.GetStringExtra(AccountManager.KeyAccountType);
                    var accounts = am.GetAccountsByType(accountType);
                    var acct = accounts.FirstOrDefault(a => a.Name == accountName);
                    var tok = am.PeekAuthToken(acct, AuthTokenType);
                    am.InvalidateAuthToken(accountType, tok);
                    var getNew = await am.GetAuthToken(acct, AuthTokenType, null, false, null, null).GetResultAsync(5000, TimeUnit.Milliseconds);
                }            
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                var layout = FindViewById<CoordinatorLayout>(Resource.Id.main_layout);
                Snackbar.Make(layout, "Super cool settings go here...", Snackbar.LengthLong)
                    .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            View view = (View)sender;
            Snackbar.Make(view, "Fabulicious 👋", Snackbar.LengthLong)
                .SetAction("Action", (Android.Views.View.IOnClickListener)null).Show();
        }

        private void AuthButtonOnClick(object sender, EventArgs e)
        {
            var isAuthAppInstalled = IsAccountAuthenticatorAppInstalled();

            if (isAuthAppInstalled)
            {
                var accountPickerIntent = default(Intent);
                if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
                {
                    accountPickerIntent =
                        AccountManager.NewChooseAccountIntent(null, null, new[] { "com.dgatto.accountauthenticator" }, null, null, null, null);
                }
                else
                {
                    var accounts = AccountManager.Get(this).GetAccounts();
                    accountPickerIntent =
                        AccountManager.NewChooseAccountIntent(null, accounts, null, false, null, null, null, null);
                }

                StartActivityForResult(accountPickerIntent, AccountRequestCode);
            }
            else
            {
                var adb = new Android.Support.V7.App.AlertDialog.Builder(this);
                adb.SetTitle("Attention!");
                adb.SetMessage("This app requires the Account Authenticator! Please install it before continuing...");
                adb.SetPositiveButton("OK", (s, a) => { });
                var dialog = adb.Create();
                dialog.Show();
            }
        }

        private bool IsAccountAuthenticatorAppInstalled()
        {
            var launchIntent = PackageManager.GetLaunchIntentForPackage("com.dgatto.accountauthenticator");
            return launchIntent != null;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

