using ControlSamples.Effects;
using Xamarin.Forms;

namespace ControlSamples.Samples
{
    public class NoKeyboardEntry : ContentPage
    {
        public NoKeyboardEntry()
        {
            Title = nameof(NoKeyboardEntry);

            var layout = new StackLayout();
            layout.Spacing = 8;
            layout.Margin = new Thickness(6, 20, 6, 6);

            layout.Children.Add(new Label()
            {
                Text = "An effect applied to an Entry control. Disables the soft keyboard from displaying when focused."
            });

            var entry = new Entry();
            entry.Effects.Add(new NoKeyboardEffect());
            layout.Children.Add(entry);

            layout.Children.Add(new Label()
            {
                Text = "On Android you may need to disable ‘Show virtual keyboard’ in the Settings, which by default shows the soft keyboard when a hardware keyboard is detected."
            });

            var btn = new Button() { Text = "Focus Entry" };
            btn.Clicked += (sender, args) => entry.Focus();
            layout.Children.Add(btn);

            Content = layout;
        }
    }
}
