using Foundation;
using ControlSamples.Effects;
using ControlSamples.iOS.Effects;
using System.ComponentModel;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using System.Threading.Tasks;

[assembly: ExportEffect(typeof(WebViewEffect_iOS), nameof(WebViewEffect))]
namespace ControlSamples.iOS.Effects
{
    [Preserve(AllMembers = true)]
    public class WebViewEffect_iOS : PlatformEffect
    {
        private WebView _webViewForms;
        private WKWebView _webView;

        private EffectWKNavigationDelegate _wkNavigationDelegate;

        protected override void OnAttached()
        {
            if (Element is WebView webViewForms &&
                Control is WKWebView webView)
            {
                _webViewForms = webViewForms;
                _webView = webView;

                _wkNavigationDelegate = new EffectWKNavigationDelegate(_webViewForms, _webView);
                _webView.NavigationDelegate = _wkNavigationDelegate;
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == WebViewEffect.IsJavaScriptEnabledProperty.PropertyName)
            {
                _webView.Configuration.Preferences.JavaScriptEnabled = WebViewEffect.GetIsJavaScriptEnabled(_webViewForms);
            }
        }

        protected override void OnDetached()
        {
            if (_wkNavigationDelegate != null)
            {
                _webView.NavigationDelegate = null;
                _wkNavigationDelegate.Dispose();

                _webViewForms = null;
                _webView = null;
                _wkNavigationDelegate = null;
            }
        }

        private class EffectWKNavigationDelegate : WKNavigationDelegate
        {
            private WebView _webViewForms;
            private WKWebView _webView;

            public EffectWKNavigationDelegate(WebView webViewForms, WKWebView webView)
            {
                _webViewForms = webViewForms;
                _webView = webView;
            }

            public override void DidFinishNavigation(WKWebView webView, WKNavigation navigation)
            {
                if (_webViewForms != null &&
                    _webView != null &&
                    WebViewEffect.GetAutoHeight(_webViewForms) == true)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        // Force async execution
                        // Generally gives enough time for render to complete{
                        await Task.Yield();
                        _webViewForms.HeightRequest = _webView.ScrollView.ContentSize.Height;
                    });
                }
            }

            protected override void Dispose(bool disposing)
            {
                _webViewForms = null;
                _webView = null;

                base.Dispose(disposing);
            }
        }
    }
}
