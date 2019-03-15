using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VPIIntercom.Droid.Resources
{
    [Activity(Label = "VPI Intercom", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        private static int _SPLASH_TIME = 1 * 1000;// 1 seconds
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestWindowFeature(WindowFeatures.NoTitle);
            SetContentView(Resource.Layout.Splash);


            try
            {
                new Handler().PostDelayed(() =>
                {
                    var intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                    Finish();

                }, _SPLASH_TIME);

            }
            catch (Exception) { }

        }
    }
}