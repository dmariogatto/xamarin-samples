using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ControlSamples.Samples
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewAutoHeight : ContentPage
    {
        public WebViewAutoHeight()
        {
            InitializeComponent();

            BindingContext = this;
        }

        public string HtmlFormattedContent { get; set; } =
            "<h1>Header Level 1</h1>" +
            "<p>A super interesting paragraph. Hey, look! I'm some <b>bold</b> text.</p>" +
            "<p>Another super interesting paragraph with a CSS <span style=\"text-transform: uppercase;\">text transform</span></p>" +
            "<div style=\"margin-top: 5em; background-color: red;\">Top margin of 5em with a red background</div>" +
            "<br />" +
            "<em>Bye for now 👋</em>";
    }
}