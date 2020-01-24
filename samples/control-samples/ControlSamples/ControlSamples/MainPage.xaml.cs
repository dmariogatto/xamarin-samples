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
            BindingContext = this;

            InitializeComponent();

            Title = "Samples";
        }

        private Command _sampleClickedCommand;
        public Command SampleClickedCommand => _sampleClickedCommand ??= new Command<SamplePage>(async (page) => await SampleClicked(page));

        private async Task SampleClicked(SamplePage page)
        {
            switch (page)
            {
                case SamplePage.NoKeyboardEntry:
                    await Navigation.PushAsync(new NoKeyboardEntry());
                    break;
                case SamplePage.CollectionViewSearch:
                    await Navigation.PushAsync(new CollectionViewSearch());
                    break;
            }
        }
    }
}
