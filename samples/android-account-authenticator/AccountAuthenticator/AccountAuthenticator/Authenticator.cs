using System;
using AccountAuthenticator.Services;
using Android.Accounts;
using Android.Content;
using Android.OS;

namespace AccountAuthenticator
{
    public class Authenticator : AbstractAccountAuthenticator
    {
        public static readonly string KeyAddNewAccount = "AddAccount";
        public static readonly string AuthTokenType = "dgatto.authtype";

        private readonly Context _context;
        private readonly IAuthenticationService _authService = new AuthenticationService();

        public Authenticator(Context ctx) : base (ctx)
        {
            _context = ctx;
        }

        public override Bundle AddAccount(AccountAuthenticatorResponse response, string accountType, string authTokenType, string[] requiredFeatures, Bundle options)
        {
            var intent = new Intent(_context, typeof(AccountActivity));
            intent.PutExtra(AccountManager.KeyAccountType, accountType);
            intent.PutExtra(nameof(AddAccount), true);
            intent.PutExtra(AccountManager.KeyAccountAuthenticatorResponse, response);

            var bundle = new Bundle();
            bundle.PutParcelable(AccountManager.KeyIntent, intent);

            return bundle;
        }

        public override Bundle ConfirmCredentials(AccountAuthenticatorResponse response, Account account, Bundle options)
        {
            // Checks credentials against an existing account
            return null;
        }

        public override Bundle EditProperties(AccountAuthenticatorResponse response, string accountType)
        {
            // Returns Bundle containing Intent for an Activity to edit properties
            return null;
        }

        public override Bundle GetAuthToken(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            var accountManager = AccountManager.Get(_context);
            var authToken = accountManager.PeekAuthToken(account, authTokenType);

            if (string.IsNullOrWhiteSpace(authToken))
            {
                var username = account.Name;
                var password = accountManager.GetPassword(account);
                if (!string.IsNullOrWhiteSpace(username) &&
                    !string.IsNullOrWhiteSpace(password))
                {
                    authToken = _authService.GetAuthToken(username, password).Result;
                }
            }

            if (!string.IsNullOrWhiteSpace(authToken))
            {
                var result = new Bundle();
                result.PutString(AccountManager.KeyAccountName, account.Name);
                result.PutString(AccountManager.KeyAccountType, account.Type);
                result.PutString(AccountManager.KeyAuthtoken, authToken);
                return result;
            }

            var intent = new Intent(_context, typeof(AccountActivity));
            intent.PutExtra(AccountManager.KeyAccountAuthenticatorResponse, response);
            intent.PutExtra(AccountManager.KeyAccountType, account.Type);
            intent.PutExtra(AccountManager.KeyAccountName, account.Name);

            var bundle = new Bundle();
            bundle.PutParcelable(AccountManager.KeyIntent, intent);
            return bundle;
        }

        public override string GetAuthTokenLabel(string authTokenType)
        {
            // Localised label for a given authTokenType
            return "full";
        }

        public override Bundle HasFeatures(AccountAuthenticatorResponse response, Account account, string[] features)
        {
            // Returns supported Authenticator features for specified Account
            var result = new Bundle();
            result.PutBoolean(AccountManager.KeyBooleanResult, false);
            return result;
        }

        public override Bundle UpdateCredentials(AccountAuthenticatorResponse response, Account account, string authTokenType, Bundle options)
        {
            // Update credentials for specified Account
            var result = new Bundle();
            result.PutBoolean(AccountManager.KeyBooleanResult, false);
            return result;
        }
    }
}