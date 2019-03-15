using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using VPIXamarinIntercom.CustomControls;
using VPIXamarinIntercom.iOS.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(BorderColorRenderer_iOS))]
namespace VPIXamarinIntercom.iOS.Custom_Renderers
{
    public class BorderColorRenderer_iOS : EntryRenderer
    {
        public BorderColorRenderer_iOS()
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