using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using VPIXamarinIntercom.CustomControls;
using VPIXamarinIntercom.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(BorderColorRenderer_Droid))]
namespace VPIXamarinIntercom.Droid.Custom_Renderers
{
    public class BorderColorRenderer_Droid : EntryRenderer
    {
        public BorderColorRenderer_Droid(Context context) : base(context)
        {

        }

        protected CustomEntry BorderColorEntry { get; private set; }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                this.BorderColorEntry = (CustomEntry)this.Element;
            }

            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;

                Control.SetPadding(20, 0, 0, 0);
                GradientDrawable customBG = new GradientDrawable();
                customBG.SetCornerRadius(8);
                int borderWidth = 3;
                customBG.SetStroke(borderWidth, Android.Graphics.Color.Transparent);
                Control.SetBackground(customBG);
                Control.Gravity = GravityFlags.CenterVertical;

                this.UpdateLayout();
            }


        }
    }
}