using System;
using System.Globalization;
using Xamarin.Forms;

namespace ControlSamples.Converters
{
    public class HtmlStringToDocumentConverter : IValueConverter
    {
        private const string HtmlDocumentFormat = "<!DOCTYPE html><html><head><meta name=\"viewport\" content=\"width=device-width, initial-scale=1\"></head><body style=\"background-color: {0};\">{1}</body></html>";
        private const string RgbaFormat = "rgba({0},{1},{2},{3})";

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var content = value?.ToString() ?? string.Empty;

            var bgColor = (Color)Application.Current.Resources["PageBackgroundColor"];
            var bgHtmlRgba = string.Format(RgbaFormat,
                System.Convert.ToInt32(bgColor.R * 255),
                System.Convert.ToInt32(bgColor.G * 255),
                System.Convert.ToInt32(bgColor.B * 255),
                System.Convert.ToInt32(bgColor.A * 255));

            return string.Format(HtmlDocumentFormat,
                    bgHtmlRgba,
                    content);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
