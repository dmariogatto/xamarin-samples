using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using HardwareKeyboard.Controls;
using HardwareKeyboard.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(KeyboardPage), typeof(KeyboardPageRenderer))]
namespace HardwareKeyboard.Droid.Renderers
{
    [Preserve(AllMembers = true)]
    public class KeyboardPageRenderer : PageRenderer
    {
        private KeyboardPage _page => Element as KeyboardPage;

        public KeyboardPageRenderer(Context context) : base(context)
        {
            Focusable = true;
            FocusableInTouchMode = true;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (Visibility == ViewStates.Visible)
                RequestFocus();

            _page.Appearing += (sender, args) =>
            {
                RequestFocus();
            };
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            System.Diagnostics.Debug.WriteLine($"{_page?.Name ?? string.Empty}:{nameof(OnKeyUp)}:{keyCode}");
            _page?.OnKeyUp(keyCode.ToString(), $"{_page?.Name ?? string.Empty}:{nameof(OnKeyUp)}:{keyCode}");

            return true;
            
            return base.OnKeyUp(keyCode, e);
        }
    }
}