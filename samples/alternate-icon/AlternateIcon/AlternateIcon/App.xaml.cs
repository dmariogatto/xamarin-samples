using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AlternateIcon
{
    public partial class App : Application
    {
        public EventHandler<AppIcon> AppIconChanged;

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        public void ChangeIcon(AppIcon icon)
        {
            AppIconChanged?.Invoke(this, icon);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
