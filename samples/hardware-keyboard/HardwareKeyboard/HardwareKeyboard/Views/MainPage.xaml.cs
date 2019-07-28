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

            Children.Add(new NavigationPage(new ItemsPage() { Name = "Page1" }) { Title = "1️⃣" });
            Children.Add(new NavigationPage(new ItemsPage() { Name = "Page2" }) { Title = "2️⃣" });
        }
    }
}