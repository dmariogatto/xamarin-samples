using ControlSamples.Effects;
using Xamarin.Forms;

namespace ControlSamples.Samples
{
    public class NoKeyboardSample : ContentPage
    {
        public NoKeyboardSample()
        {
            Title = nameof(NoKeyboardSample);

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

            Content = layout;
        }
    }
}
