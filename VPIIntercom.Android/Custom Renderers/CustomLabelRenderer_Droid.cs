using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VPIXamarinIntercom.CustomControls;
using VPIXamarinIntercom.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer_Droid))]
namespace VPIXamarinIntercom.Droid.Custom_Renderers
{
    public class CustomLabelRenderer_Droid : LabelRenderer
    {
        public CustomLabelRenderer_Droid(Context context) : base(context)
        {

        }

        protected CustomLabel LineSpacingLabel { get; private set; }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.LineSpacingLabel = (CustomLabel)this.Element;
            }

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;

                var lineSpacing = this.LineSpacingLabel.LineSpacing;

                this.Control.SetLineSpacing(1f, (float)lineSpacing);

                this.UpdateLayout();
            }
        }
    }
}