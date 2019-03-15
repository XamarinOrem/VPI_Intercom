using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VPIXamarinIntercom.Droid.Custom_Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer_Droid))]
namespace VPIXamarinIntercom.Droid.Custom_Renderers
{
    public class CustomEntryRenderer_Droid : EntryRenderer
    {
        public CustomEntryRenderer_Droid(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);


            if (!string.IsNullOrEmpty(e.NewElement?.StyleId))
            {
                var font = Typeface.CreateFromAsset(Android.App.Application.Context.ApplicationContext.Assets, e.NewElement.StyleId + ".ttf");

                Control.Typeface = font;
            }

            Control.SetPadding(20, 0, 0, 0);
            GradientDrawable customBG = new GradientDrawable();
            customBG.SetCornerRadius(8);
            int borderWidth = 3;
            customBG.SetStroke(borderWidth, Android.Graphics.Color.Rgb(255, 120, 36));
            Control.SetBackground(customBG);
            Control.Gravity = GravityFlags.CenterVertical;
        }
    }
}