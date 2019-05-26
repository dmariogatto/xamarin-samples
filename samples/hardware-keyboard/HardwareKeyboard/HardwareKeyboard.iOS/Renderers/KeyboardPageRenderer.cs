using Foundation;
using HardwareKeyboard.Controls;
using HardwareKeyboard.iOS.Renderers;
using System.Collections.Generic;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(KeyboardPage), typeof(KeyboardPageRenderer))]
namespace HardwareKeyboard.iOS.Renderers
{
    public class KeyboardPageRenderer : PageRenderer
    {
        private const string KeySelector = "KeyCommand:";

        private KeyboardPage _page => Element as KeyboardPage;
        private readonly List<UIKeyCommand> _keyCommands = new List<UIKeyCommand>();

        public override bool CanBecomeFirstResponder
        {
            get => true;
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (_keyCommands.Count == 0)
            {
                var selector = new ObjCRuntime.Selector(KeySelector);

                for (var i = 0; i < 10; i++)
                {
                    _keyCommands.Add(UIKeyCommand.Create((NSString)i.ToString(), 0, selector));
                    _keyCommands.Add(UIKeyCommand.Create((NSString)i.ToString(), UIKeyModifierFlags.NumericPad, selector));
                }

                for (var i = 0; i < 26; i++)
                {
                    var key = (char)('a' + i);
                    _keyCommands.Add(UIKeyCommand.Create((NSString)key.ToString(), 0, selector));
                }

                foreach (var kc in _keyCommands)
                {
                    AddKeyCommand(kc);
                }
            }           
        }

        [Export(KeySelector)]
        private void KeyCommand(UIKeyCommand keyCmd)
        {
            if (keyCmd == null)
                return;

            if (_keyCommands.Contains(keyCmd))
            {
                System.Diagnostics.Debug.WriteLine($"{_page?.Name ?? string.Empty}:{nameof(KeyCommand)}:{keyCmd.Input}");
                _page?.OnKeyUp(keyCmd.Input, $"{_page?.Name ?? string.Empty}:{nameof(KeyCommand)}:{keyCmd.Input}");
            }            
        }
    }
}