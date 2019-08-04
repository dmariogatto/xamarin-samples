using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HardwareKeyboard.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();

            Children.Add(new NavigationPage(new ItemsPage() { Name = "Page 1" }) { Title = "Page 1" });
            Children.Add(new NavigationPage(new ItemsPage() { Name = "Page 2" }) { Title = "Page 2" });
        }
    }
}