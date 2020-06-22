using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlSamples.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FadingEdge : ContentPage
    {
        public FadingEdge()
        {
            InitializeComponent();

            var source = new List<string>();
            for (var i = 0; i < 100; i++)
            {
                source.Add($"Item {i + 1}");
            }
            StringCollection.ItemsSource = source;
        }
    }
}