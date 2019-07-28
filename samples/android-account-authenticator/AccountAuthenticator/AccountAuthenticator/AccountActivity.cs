using System;
using AccountAuthenticator.Services;
using Android;
using Android.Accounts;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace AccountAuthenticator
{
    [Activity]
    public class AccountActivity : AccountAuthenticatorActivity
    {
        private readonly IAuthenticationService _authService = new AuthenticationService();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.account_authenticator);

            var submit = FindViewById<Button>(Resource.Id.submit);
            submit.Click += Submit_Click;
        }

        private async void Submit_Click(object sender, EventArgs e)
        {
            var username = FindViewById<EditText>(Resource.Id.account_name).Text;
            var password = FindViewById<EditText>(Resource.Id.account_password).Text;

            var data = new Bundle();
            try
            {
                var authToken = await _authService.GetAuthToken(username, password);

                data.PutString(AccountManager.KeyAccountName, username);
                data.PutString(AccountManager.KeyAccountType, Intent.GetStringExtra(AccountManager.KeyAccountType));
                data.PutString(AccountManager.KeyAuthtoken, authToken);
                data.PutString(AccountManager.KeyPassword, password);

            }
            catch (Exception ex)
            {
                data.PutString(AccountManager.KeyErrorMessage, ex.Message);
            }

            var res = new Intent();
            res.PutExtras(data);

            FinishLogin(res);
        }

        private void FinishLogin(Intent intent)
        {
            var accountManager = AccountManager.Get(this);

            var accountName = intent.GetStringExtra(AccountManager.KeyAccountName);
            var accountPassword = intent.GetStringExtra(AccountManager.KeyPassword);
            var account = new Account(accountName, intent.GetStringExtra(AccountManager.KeyAccountType));
            
            if (Intent.GetBooleanExtra(Authenticator.KeyAddNewAccount, false))
            {
                var authToken = intent.GetStringExtra(AccountManager.KeyAuthtoken);
                // Creating the account on the device
                // Setting without the auth token will trigger AccountManager.GetAuthToken()
                accountManager.AddAccountExplicitly(account, accountPassword, null);
                accountManager.SetAuthToken(account, Authenticator.AuthTokenType, authToken);
            }
            else
            {
                accountManager.SetPassword(account, accountPassword);
            }

            SetAccountAuthenticatorResult(intent.Extras);
            SetResult(Result.Ok, intent);
            Finish();
        }
    }
}