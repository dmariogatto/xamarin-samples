using System;
using Xamarin.Forms;

namespace ControlSamples.Effects
{
	public class WebViewEffect : RoutingEffect
    {
        public WebViewEffect() : base($"ControlSamples.Effects.{nameof(WebViewEffect)}")
        {
        }

        public static readonly BindableProperty AutoHeightProperty =
            BindableProperty.CreateAttached(
                propertyName: "AutoHeight",
                returnType: typeof(bool),
                declaringType: typeof(WebViewEffect),
                defaultValue: false,
                propertyChanged: null);

        public static readonly BindableProperty IsJavaScriptEnabledProperty =
            BindableProperty.CreateAttached(
                propertyName: "IsJavaScriptEnabled",
                returnType: typeof(bool),
                declaringType: typeof(WebViewEffect),
                defaultValue: true,
                propertyChanged: null);

        public static bool GetAutoHeight(BindableObject view)
        {
            return (bool)view.GetValue(AutoHeightProperty);
        }

        public static void SetAutoHeight(BindableObject view, bool value)
        {
            view.SetValue(AutoHeightProperty, value);
        }

        public static bool GetIsJavaScriptEnabled(BindableObject view)
        {
            return (bool)view.GetValue(IsJavaScriptEnabledProperty);
        }

        public static void SetIsJavaScriptEnabled(BindableObject view, bool value)
        {
            view.SetValue(IsJavaScriptEnabledProperty, value);
        }
    }
}
