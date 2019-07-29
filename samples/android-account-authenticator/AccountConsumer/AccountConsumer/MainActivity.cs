using System;
using System.Linq;
using System.Threading.Tasks;
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
        private const string AccountType = "com.dgatto.accountauthenticator";
        private const string AuthTokenType = "dgatto.authtype";

        private GridLayout _gridLayout;
        private TextView _username;
        private TextView _token;
        private Button _getNewAuthTok;

        private string _accountName;

        protected async override void OnCreate(Bundle savedInstanceState)
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

            _gridLayout = FindViewById<GridLayout>(Resource.Id.grid_layout);
            _username = FindViewById<TextView>(Resource.Id.username);
            _token = FindViewById<TextView>(Resource.Id.token);
            _getNewAuthTok = FindViewById<Button>(Resource.Id.new_auth_tok_button);
            _getNewAuthTok.Click += GetNewAuthTokOnClick;

            await RefreshAccountDetails();
        }

        private async Task RefreshAccountDetails()
        {
            var account = GetAccount();
            if (account != null)
            {
                var am = AccountManager.Get(this);
                _username.Text = account.Name;
                var result = await am.GetAuthToken(account, AuthTokenType, null, false, null, null).GetResultAsync(5000, TimeUnit.Milliseconds);
                if (result is Bundle bundle)
                {
                    _token.Text = bundle.GetString(AccountManager.KeyAuthtoken);
                }

                _gridLayout.Visibility = ViewStates.Visible;
                _getNewAuthTok.Visibility = ViewStates.Visible;
            }
        }

        private async void GetNewAuthTokOnClick(object sender, EventArgs e)
        {
            var account = GetAccount();
            if (account != null)
            {
                var am = AccountManager.Get(this);
                var tok = am.PeekAuthToken(account, AuthTokenType);
                am.InvalidateAuthToken(AccountType, tok);
                await RefreshAccountDetails();
            }
        }

        protected override async void OnActivityResult(int requestCode, Result resultCode, Android.Content.Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            if (requestCode == AccountRequestCode &&
                resultCode == Result.Ok)
            {
                _accountName = data.GetStringExtra(AccountManager.KeyAccountName);
                await RefreshAccountDetails();
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
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
                        AccountManager.NewChooseAccountIntent(null, null, new[] { AccountType }, null, null, null, null);
                }
                else
                {
                    var accounts = AccountManager.Get(this).GetAccounts();
                    accountPickerIntent =
                        AccountManager.NewChooseAccountIntent(null, accounts, new[] { AccountType }, false, null, null, null, null);
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

        private Account GetAccount()
        {
            var am = AccountManager.Get(this);
            var accounts = am.GetAccountsByType(AccountType);
            return accounts.FirstOrDefault(a => string.IsNullOrEmpty(_accountName) || a.Name == _accountName);
        }

        private bool IsAccountAuthenticatorAppInstalled()
        {
            var launchIntent = PackageManager.GetLaunchIntentForPackage("com.dgatto.accountauthenticator");
            return launchIntent != null;
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

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}

