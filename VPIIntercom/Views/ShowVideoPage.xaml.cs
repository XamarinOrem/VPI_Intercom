using System;
using Android.Views;
using Android.Widget;
using Linphone;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using VPIIntercom.Droid;
using Android.App;
using Org.Linphone.Mediastream.Video;

namespace VPIIntercom.Views
{
    public partial class ShowVideoPage : ContentPage
    {
        public static bool isCallEnded = false;

        public Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }
        Org.Linphone.Mediastream.Video.Display.GL2JNIView displayCamera;
        SurfaceView captureCamera;

        public ShowVideoPage()
        {
            try
            {
                InitializeComponent();

                NavigationPage.SetHasNavigationBar(this, false);
#if __ANDROID__

                Android.Content.Context context = Android.App.Application.Context;
                LinearLayout fl = new LinearLayout(context);

                ViewGroup.LayoutParams lparams = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.WrapContent);
                fl.LayoutParameters = lparams;

                displayCamera = new Org.Linphone.Mediastream.Video.Display.GL2JNIView(context);
                ViewGroup.LayoutParams dparams = new ViewGroup.LayoutParams(640, 480);
                displayCamera.LayoutParameters = dparams;
                displayCamera.Holder.SetFixedSize(640, 480);

                captureCamera = new SurfaceView(context);
                ViewGroup.LayoutParams cparams = new ViewGroup.LayoutParams(320, 240);
                captureCamera.LayoutParameters = cparams;
                captureCamera.Holder.SetFixedSize(240, 320);

                fl.AddView(displayCamera);
                fl.AddView(captureCamera);

                AndroidVideoWindowImpl androidView = new AndroidVideoWindowImpl(displayCamera, captureCamera, null);
                //this.LinphoneCore.NativeVideoWindowId = androidView.Handle;
                this.LinphoneCore.NativePreviewWindowId = captureCamera.Handle;

                this.getLayoutView().Children.Add(fl);

                //this.LinphoneCore.VideoDisplayEnabled = true;
                this.LinphoneCore.VideoCaptureEnabled = true;
            }
            catch(Exception)
            {
                
            }
#endif
        }


        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            try
            {
                if (BindingContext != null)
                {
                    if (BindingContext.ToString() == "0")
                    {
                        min = 0;
                        sec = 0;
                        hr = 0;
                        Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                        {
                            durationLbl.Text = timerTime();
                            if (isCallEnded)
                            {
                                App.isCall = false;
                                if (!ApartmentListDark.outGoingCall)
                                {
                                    Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new ApartmentListDark());
                                }
                                else if (!DialingKeyboard.outGoingCall)
                                {
                                    Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new DialingKeyboard());
                                }
                                return false;
                            }
                            else
                            {
                                return true;
                            }
                        });
                    }
                }
            }
            catch(Exception)
            {
                
            }
           
        }

        int min = 0;
        int sec = 0;
        int hr = 0;
        public string hrTime = "00";
        public string minTime = "00";
        public string secTime = "00";
        public double _min = 0;
        public string timerTime()
        {
            string time = "00:00:00";
            try
            {
                if (min == 59 && sec == 59)
                {
                    sec = 0;
                    min = 0;
                    hr = hr + 1;

                    hrTime = (hr).ToString();
                    minTime = (min).ToString();
                    _min++;

                }
                else if (sec == 59)
                {
                    sec = 0;
                    min = min + 1;

                    minTime = (min).ToString();
                    _min++;
                }
                else
                {
                    sec = sec + 1;
                    secTime = (sec).ToString();

                }

                time = hrTime + " : " + minTime + " : " + secTime;
            }
            catch (Exception)
            {
            }
            return time;
        }

        void Phone_Call_End(object sender, TappedEventArgs e)
        {
            var _call = LinphoneCore.CurrentCall;
            if (_call != null)
            {
                PostService.LinphoneCore.TerminateCall(_call);
                isCallEnded = true;
                if (!ApartmentListDark.outGoingCall)
                {
                    Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new ApartmentListDark());
                }
                else if(!DialingKeyboard.outGoingCall)
                {
                    Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new DialingKeyboard());
                }
                                   
            }
        }

        public Xamarin.Forms.StackLayout getLayoutView()
        {
            var getLayout = this.FindByName<Xamarin.Forms.StackLayout>("stack_layout");
            return getLayout;
        }

    }
}
