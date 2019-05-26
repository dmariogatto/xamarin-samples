using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HardwareKeyboard.Controls
{
    public class KeyboardPage : ContentPage
    {
        public string Name { get; set; }

        public virtual void OnKeyUp(string text, string description)
        {
            return;
        }
    }
}
