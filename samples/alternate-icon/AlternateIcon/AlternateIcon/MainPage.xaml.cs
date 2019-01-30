using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AlternateIcon
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void IconButton_Clicked(object sender, EventArgs e)
        {
            var app = (App)Application.Current;

            if (sender == ChickenButton)
            {
                app.ChangeIcon(AppIcon.Chicken);
            }
            else if (sender == CactusButton)
            {
                app.ChangeIcon(AppIcon.Cactus);
            }
        }
    }
}
