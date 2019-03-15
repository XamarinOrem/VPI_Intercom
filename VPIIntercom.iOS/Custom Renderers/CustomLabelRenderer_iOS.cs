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

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer_iOS))]
namespace VPIXamarinIntercom.iOS.Custom_Renderers
{
    public class CustomLabelRenderer_iOS : LabelRenderer
    {
        public CustomLabelRenderer_iOS()
        {

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var lineSpacingLabel = (CustomLabel)this.Element;
            var paragraphStyle = new NSMutableParagraphStyle()
            {
                LineSpacing = (nfloat)lineSpacingLabel.LineSpacing
            };
            var _string = new NSMutableAttributedString(lineSpacingLabel.Text);
            var style = UIStringAttributeKey.ParagraphStyle;
            var range = new NSRange(0, _string.Length);

            _string.AddAttribute(style, paragraphStyle, range);

            this.Control.AttributedText = _string;
        }
    }
}