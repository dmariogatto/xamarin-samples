using System;
using Xamarin.Forms;

namespace HardwareKeyboard.Controls
{
    public enum KeyCommand
    {
        Cut,
        Copy,
        Paste,
    }

    public class KeyboardPage : ContentPage
    {
        public string Name { get; set; }

        public virtual void OnKeyUp(string text, string description)
        {
            return;
        }

        public virtual void OnKeyCommand(KeyCommand command)
        {
            return;
        }
    }
}
