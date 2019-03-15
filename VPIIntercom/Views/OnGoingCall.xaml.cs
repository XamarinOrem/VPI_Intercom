using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Linphone;
using VPIIntercom.Droid;
using Xamarin.Forms;
using System.Linq;
using Android.App;

namespace VPIIntercom.Views
{
    public partial class OnGoingCall : ContentPage
    {
        public static bool isCallEnded = false;

        public OnGoingCall()
        {
            try
            {
                InitializeComponent();

                this.BackgroundImage = "ic_bg_dark.png";

                string data = PostService._call.RemoteAddressAsString;

                var result = from Match match in Regex.Matches(data, "\"([^\"]*)\"")
                             select match.ToString();

                foreach (var item in result)
                {
                    receiveCallingNumber.Text = item;
                }
                NavigationPage.SetHasNavigationBar(this, false);
            }
            catch(Exception)
            {
                
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
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
                            Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new ApartmentListDark());
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

        void Phone_Call_Receive(object sender,TappedEventArgs e)
        {
            if (PostService._call != null)
            {
                PostService.LinphoneCore.AcceptCall(PostService._call);
                this.BindingContext = "0";
                endCallLayout.IsVisible = true;
                callingGrid.IsVisible = false;
            }
        }

        void Phone_Call_End_Post(object sender, TappedEventArgs e)
        {
            if (PostService._call != null)
            {
                PostService.LinphoneCore.TerminateCall(PostService._call);
                App.isCall = false;
                Xamarin.Forms.Application.Current.MainPage = new NavigationPage(new ApartmentListDark());
            }
        }


        void Phone_Call_End(object sender, TappedEventArgs e)
        {
            if (PostService._call != null)
            {
                PostService.LinphoneCore.TerminateCall(PostService._call);
                isCallEnded = true;
            }
        }
    }
}
