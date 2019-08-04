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
        private readonly IList<UIKeyCommand> _keyCommands = new List<UIKeyCommand>();

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

                // Viewable on iPad (>= iOS 9) when holding down ⌘
                _keyCommands.Add(UIKeyCommand.Create(new NSString("x"), UIKeyModifierFlags.Command, selector, new NSString("Cut")));
                _keyCommands.Add(UIKeyCommand.Create(new NSString("c"), UIKeyModifierFlags.Command, selector, new NSString("Copy")));
                _keyCommands.Add(UIKeyCommand.Create(new NSString("v"), UIKeyModifierFlags.Command, selector, new NSString("Paste")));

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
                if (keyCmd.ModifierFlags == UIKeyModifierFlags.Command)
                {
                    switch (keyCmd.Input.ToString())
                    {
                        case "x":
                            _page?.OnKeyCommand(Controls.KeyCommand.Cut);
                            break;
                        case "c":
                            _page?.OnKeyCommand(Controls.KeyCommand.Copy);
                            break;
                        case "v":
                            _page?.OnKeyCommand(Controls.KeyCommand.Paste);
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    var desc = $"{_page?.Name ?? string.Empty} : {nameof(KeyCommand)} : {keyCmd.Input}";
                    System.Diagnostics.Debug.WriteLine(desc);
                    _page?.OnKeyUp(keyCmd.Input, desc);
                }               
            }            
        }
    }
}