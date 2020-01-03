using ControlSamples.Effects;
using ControlSamples.iOS.Effects;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("ControlSamples.Effects")]
[assembly: ExportEffect(typeof(NoKeyboardEffect_iOS), nameof(NoKeyboardEffect))]
namespace ControlSamples.iOS.Effects
{
    public class NoKeyboardEffect_iOS : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                if (Control is UITextField textField)
                {
                    // dummy view to override the soft keyboard
                    textField.InputView = new UIView();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"{nameof(NoKeyboardEffect)} failed to attached: {ex.Message}");
            }
        }

        protected override void OnDetached()
        {            
        }
    }
}