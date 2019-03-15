using System.ComponentModel;
using VPIXamarinIntercom.CustomControls;
using VPIXamarinIntercom.iOS.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry1), typeof(BorderColorRenderer1_iOS))]
namespace VPIXamarinIntercom.iOS.Custom_Renderers
{
    public class BorderColorRenderer1_iOS : EntryRenderer
    {
        public BorderColorRenderer1_iOS()
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (Control != null)
            {

            }
        }
    }
}