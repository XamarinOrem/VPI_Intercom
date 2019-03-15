using System.ComponentModel;
using UIKit;
using VPIXamarinIntercom.iOS.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer_iOS))]
namespace VPIXamarinIntercom.iOS.Custom_Renderers
{
    public class CustomEntryRenderer_iOS : EntryRenderer
    {
        public CustomEntryRenderer_iOS()
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {
                Control.Layer.BorderWidth = 2;

                //Below line is useful to give border color 
                Control.Layer.BorderColor = UIColor.FromRGB(255, 120, 36).CGColor;
                //Control.BackgroundColor=UIColor.tra
                Control.Layer.CornerRadius = 8;
            }
        }
    }
}