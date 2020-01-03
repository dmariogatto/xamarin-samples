using ControlSamples.Samples;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ControlSamples
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Title = "Samples";
        }

        private void SampleButtonClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new NoKeyboardSample());
        }
    }
}
