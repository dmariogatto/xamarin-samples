using Android.Runtime;
using Android.Webkit;
using ControlSamples.Droid.Effects;
using ControlSamples.Effects;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebViewDroid = Android.Webkit.WebView;
using WebViewForms = Xamarin.Forms.WebView;

[assembly: ExportEffect(typeof(WebViewEffect_Droid), nameof(WebViewEffect))]
namespace ControlSamples.Droid.Effects
{
    [Preserve(AllMembers = true)]
    public class WebViewEffect_Droid : PlatformEffect
    {
        private WebViewForms _webViewForms;
        private WebViewDroid _webViewDroid;

        private EffectWebViewClient _effectWebViewClient;

        protected override void OnAttached()
        {
            if (Element is WebViewForms webViewForms &&
                Control is WebViewDroid webViewDroid)
            {
                _webViewForms = webViewForms;
                _webViewDroid = webViewDroid;

                _effectWebViewClient = new EffectWebViewClient(_webViewForms, _webViewDroid);
                _webViewDroid.SetWebViewClient(_effectWebViewClient);
            }
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == WebViewEffect.IsJavaScriptEnabledProperty.PropertyName)
            {
                _webViewDroid.Settings.JavaScriptEnabled = WebViewEffect.GetIsJavaScriptEnabled(_webViewForms);
            }
        }

        protected override void OnDetached()
        {
            if (_effectWebViewClient != null)
            {
                _webViewDroid.SetWebViewClient(null);
                _effectWebViewClient.Dispose();

                _webViewForms = null;
                _webViewDroid = null;
                _effectWebViewClient = null;
            }
        }

        private class EffectWebViewClient : WebViewClient
        {
            private WebViewForms _webViewForms;
            private WebViewDroid _webViewDroid;

            public EffectWebViewClient(WebViewForms webViewForms, WebViewDroid webViewDroid)
            {
                _webViewForms = webViewForms;
                _webViewDroid = webViewDroid;
            }

            public override void OnPageCommitVisible(WebViewDroid view, string url)
            {
                if (_webViewForms != null &&
                    _webViewDroid != null &&
                    WebViewEffect.GetAutoHeight(_webViewForms) == true)
                {
                    _webViewForms.HeightRequest = _webViewDroid.ContentHeight;
                }

                base.OnPageCommitVisible(view, url);
            }

            protected override void Dispose(bool disposing)
            {
                _webViewForms = null;
                _webViewDroid = null;

                base.Dispose(disposing);
            }
        }
    }
}
