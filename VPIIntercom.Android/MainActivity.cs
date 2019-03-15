using System;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;
using Linphone;
using Android.Opengl;
using Xamarin.Forms.Platform.Android;
using System.Collections.Generic;
using Android.Util;
using Android;
using Android.Content;
using VPIIntercom.Views;
using Org.Linphone.Mediastream.Video;

namespace VPIIntercom.Droid
{
    [Activity(Label = "VPI Intercom", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        int PERMISSIONS_REQUEST = 101;

        protected override void OnCreate(Bundle bundle)
        {
            ////Java.Lang.JavaSystem.LoadLibrary("c++_shared");
            //Java.Lang.JavaSystem.LoadLibrary("bctoolbox");
            //Java.Lang.JavaSystem.LoadLibrary("ortp");
            //Java.Lang.JavaSystem.LoadLibrary("mediastreamer");
            ////Java.Lang.JavaSystem.LoadLibrary("mediastreamer_voip");
            //Java.Lang.JavaSystem.LoadLibrary("linphone");

            Java.Lang.JavaSystem.LoadLibrary("bctoolbox");
            Java.Lang.JavaSystem.LoadLibrary("ortp");
            Java.Lang.JavaSystem.LoadLibrary("mediastreamer_base");
            Java.Lang.JavaSystem.LoadLibrary("mediastreamer_voip");
            Java.Lang.JavaSystem.LoadLibrary("linphone");

            LinphoneAndroid.setAndroidContext(JNIEnv.Handle, this.Handle);

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Window.SetStatusBarColor(Android.Graphics.Color.Rgb(184, 79, 41));

            base.OnCreate(bundle);

            AssetManager assets = Assets;
            string path = FilesDir.AbsolutePath;
            string rc_path = path + "/default_rc";
            if (!File.Exists(rc_path))
            {
                using (StreamReader sr = new StreamReader(assets.Open("linphonerc_default")))
                {
                    string content = sr.ReadToEnd();
                    File.WriteAllText(rc_path, content);
                }
            }
            string factory_path = path + "/factory_rc";
            if (!File.Exists(factory_path))
            {
                using (StreamReader sr = new StreamReader(assets.Open("linphonerc_factory")))
                {
                    string content = sr.ReadToEnd();
                    File.WriteAllText(factory_path, content);
                }
            }

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);

            App.ConfigFilePath = rc_path;
            App.FactoryFilePath = factory_path;

            App application = new App(); // Do not add an arg to App constructor
            application.Manager.AndroidContext = this;

            if (App.isCall)
            {
                OnGoingCall _page = new OnGoingCall();
                application.MainPage = _page;
            }

            LoadApplication(application);

            var intent = new Intent(this, typeof(PostService));
            StartService(intent);
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (Int32.Parse(global::Android.OS.Build.VERSION.Sdk) >= 23)
            {
                List<string> Permissions = new List<string>();
                if (this.CheckSelfPermission(Manifest.Permission.Camera) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.Camera);
                }
                if (this.CheckSelfPermission(Manifest.Permission.RecordAudio) != Permission.Granted)
                {
                    Permissions.Add(Manifest.Permission.RecordAudio);
                }
                if (Permissions.Count > 0)
                {
                    this.RequestPermissions(Permissions.ToArray(), PERMISSIONS_REQUEST);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            if (requestCode == PERMISSIONS_REQUEST)
            {
                int i = 0;
                foreach (string permission in permissions)
                {
                    Log.Info("LinphoneXamarin", "Permission " + permission + " : " + grantResults[i]);
                    i += 1;
                }
            }
        }
    }
}

