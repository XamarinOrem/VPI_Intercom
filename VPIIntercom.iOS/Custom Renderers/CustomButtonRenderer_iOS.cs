using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;
using VPIXamarinIntercom.CustomControls;
using VPIXamarinIntercom.iOS.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer_iOS))]
namespace VPIXamarinIntercom.iOS.Custom_Renderers
{
    public class CustomButtonRenderer_iOS : ButtonRenderer
    {
        public CustomButtonRenderer_iOS()
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Button> e)
        {
            base.OnElementChanged(e);

            var customButton = e.NewElement as CustomButton;

            var thisButton = Control as UIButton;
            thisButton.TouchDown += delegate
            {
                customButton.OnPressed();
            };
            thisButton.TouchUpInside += delegate
            {
                customButton.OnReleased();
            };
        }
    }
}