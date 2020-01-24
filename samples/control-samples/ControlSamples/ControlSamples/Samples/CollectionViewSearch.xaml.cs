using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace ControlSamples.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CollectionViewSearch : ContentPage
    {
        public CollectionViewSearch()
        {
            InitializeComponent();

            SearchCollectionView.ItemsSource = Enumerable.Range(1, 100).Select(i => $"Item {i}").ToList();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            // Show first in the CollectionView below the SearchBar,
            // items will scroll behind the SearchBar
            SearchCollectionHeaderView.Margin =
                new Thickness(0, SearchBarView.Height + SearchBarView.Margin.Top + 4, 0, 0);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Show SearchBar if it was previously hidden when navigating back to this page
            Task.WhenAll(
                SearchBarView.TranslateTo(0, 0, 250, Easing.CubicOut),
                SearchBarView.FadeTo(1, 200));
        }

        private void PackagesScrolled(object sender, ItemsViewScrolledEventArgs e)
        {
            var transY = Convert.ToInt32(SearchBarView.TranslationY);
            if (transY == 0 &&
                e.VerticalDelta > 15)
            {
                var trans = SearchBarView.Height + SearchBarView.Margin.Top;
                var safeInsets = On<Xamarin.Forms.PlatformConfiguration.iOS>().SafeAreaInsets();

                // Start both animations concurrently
                Task.WhenAll(
                    SearchBarView.TranslateTo(0, -(trans + safeInsets.Top), 200, Easing.CubicIn),
                    SearchBarView.FadeTo(0.25, 200));
            }
            else if (transY != 0 &&
                     e.VerticalDelta < 0 &&
                     Math.Abs(e.VerticalDelta) > 10)
            {
                Task.WhenAll(
                    SearchBarView.TranslateTo(0, 0, 200, Easing.CubicOut),
                    SearchBarView.FadeTo(1, 200));
            }
        }
    }
}