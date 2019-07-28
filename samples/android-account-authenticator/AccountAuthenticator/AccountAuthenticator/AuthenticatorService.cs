using Android.App;
using Android.Content;
using Android.OS;

namespace AccountAuthenticator
{
    [Service]
    [IntentFilter(new string[] { "android.accounts.AccountAuthenticator" })]
    [MetaData("android.accounts.AccountAuthenticator", Resource = "@xml/authenticator")]
    public class AuthenticatorService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            var authenticator = new Authenticator(this);
            return authenticator.IBinder;
        }
    }
}