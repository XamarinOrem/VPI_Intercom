using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Widget;
using Linphone;
using VPIIntercom.Views;

namespace VPIIntercom.Droid
{
    [Service]
    public class PostService:Service
    {
        public CoreListener Listener;

        public static Call _call;

        public static Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Listener = Factory.Instance.CreateCoreListener();
            Listener.OnCallStateChanged = OnCall;
            LinphoneCore.AddListener(Listener);
        }


        private async void OnCall(Core lc, Call lcall, CallState state, string message)
        {
            try
            {
                if (state == CallState.IncomingReceived)
                {
                    App.isCall = true;
                    _call = lcall;
                    StartActivity(typeof(MainActivity));
                }
                //if (state == CallState.Connected)
                //{
                //    ShowVideoPage.isCallEnded = false;
                //    if (ApartmentListDark.outGoingCall||DialingKeyboard.outGoingCall)
                //    {
                //        ShowVideoPage _videoPage = new ShowVideoPage(); 
                //        await App.Current.MainPage.Navigation.PushAsync(_videoPage);
                //        _videoPage.BindingContext = 0;
                //    }
                //}
                if (!ApartmentListDark.outGoingCall||!DialingKeyboard.outGoingCall)
                {
                    if (state == CallState.Error)
                    {
                        OnGoingCall.isCallEnded = true;
                        await App.Current.MainPage.DisplayAlert("", message, "OK");
                        return;
                    }
                    if (state == CallState.End)
                    {
                        OnGoingCall.isCallEnded = true;
                        ShowVideoPage.isCallEnded = true;
                    }
                }
            }
            catch(Exception ex)
            {
                
            }
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.NotSticky;
        }


        public override IBinder OnBind(Intent intent)
        {
            // This is a started service, not a bound service, so we just return null.
            return null;
        }


        public override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}

