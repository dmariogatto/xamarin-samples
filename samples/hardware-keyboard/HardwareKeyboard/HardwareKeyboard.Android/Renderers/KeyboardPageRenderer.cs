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
        private bool _disposed;
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

            if (e.OldElement != null)
            {
                e.OldElement.Appearing -= PageOnAppearing;
            } 

            if (e.NewElement != null)
            {
                e.NewElement.Appearing += PageOnAppearing;
            }
        }

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            var handled = false;

            if (e.IsCtrlPressed)
            {
                switch (keyCode)
                {
                    case Keycode.X:
                        _page?.OnKeyCommand(KeyCommand.Cut);
                        break;
                    case Keycode.C:
                        _page?.OnKeyCommand(KeyCommand.Copy);
                        handled = true;
                        break;
                    case Keycode.V:
                        _page?.OnKeyCommand(KeyCommand.Paste);
                        handled = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                if (keyCode >= Keycode.A && keyCode <= Keycode.Z)
                {
                    // Letter
                    handled = true;
                }
                else if ((keyCode >= Keycode.Num0 && keyCode <= Keycode.Num9) ||
                         (keyCode >= Keycode.Numpad0 && keyCode <= Keycode.Num9))
                {
                    // Number
                    handled = true;
                }

                var desc = $"{_page?.Name ?? string.Empty} : {nameof(OnKeyUp)} : {keyCode}";
                System.Diagnostics.Debug.WriteLine(desc);

                if (handled)
                {
                    _page?.OnKeyUp(keyCode.ToString(), desc);                    
                }
            }

            return handled || base.OnKeyUp(keyCode, e);
        }

        private void PageOnAppearing(object sender, EventArgs e)
        {
            if (_disposed)
                return;

            RequestFocus();
        }

        protected override void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (disposing)
            {
                if (Element != null)
                {
                    Element.Appearing -= PageOnAppearing;
                }                
            }

            base.Dispose(disposing);
        }
    }
}