using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#if __ANDROID__
using Android.Content;

#endif

using Linphone;
using Rg.Plugins.Popup.Extensions;
using VPIIntercom.Droid;
using VPIIntercom.Models;
using VPIIntercom.Repo;
using VPIXamarinIntercom.Models;
using VPIXamarinIntercom.Repo;
using Xamarin.Forms;

namespace VPIIntercom.Views
{
    public partial class ApartmentListDark : ContentPage
    {
        HttpClientBase _base = new HttpClientBase();

        public CoreListener Listener;

        public static bool outGoingCall = false;

        List<GateListModel> _list = new List<GateListModel>();

        private Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }
        //private CoreListener Listener;

        public ApartmentListDark()
        {
            InitializeComponent();

            this.BackgroundImage = "ic_bg_dark.png";

            NavigationPage.SetHasNavigationBar(this, false);

            buildingName.Text=GetLoginResponse.building_name;

#if __IOS__
            this.Padding = new Thickness(10, 20, 10, 10);

#endif

            Listener = Factory.Instance.CreateCoreListener();
            Listener.OnCallStateChanged = OnCall;
            LinphoneCore.AddListener(Listener);

            GetData();

        }

        private async void OnCall(Core lc, Call lcall, CallState state, string message)
        {
            try
            {
                if (outGoingCall)
                {
                    if (state != CallState.IncomingReceived)
                    {
                        if (state == CallState.Connected)
                        {
                                ShowVideoPage.isCallEnded = false;
                                ShowVideoPage _videoPage = new ShowVideoPage();
                                await App.Current.MainPage.Navigation.PushAsync(_videoPage);
                                _videoPage.BindingContext = 0;
                        }
                        if (state == CallState.Error)
                        {
                            outGoingCall = false;
                            ShowVideoPage.isCallEnded = true;
                            await App.Current.MainPage.DisplayAlert("", message, "OK");
                            return;
                        }
                        
                            if (state == CallState.End)
                            {
                                outGoingCall = false;
                                OnGoingCall.isCallEnded = true;
                                ShowVideoPage.isCallEnded = true;
                            }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        async Task GetData()
        {
            try
            {
                if (!CommonLib.checkconnection())
                {
                    NoInternet.IsVisible = true;
                }
                else
                {
                    NoInternet.IsVisible = false;
                    await Navigation.PushPopupAsync(new LoaderPopup());
                    LoadData.IsVisible = true;
                    await GetAllExtensions();
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }
        }

        async Task GetAllExtensions()
        {
            try
            {
                if (CommonLib.checkconnection())
                {
                    var response = await _base.GetAllExtensions("http://" + GetLoginResponse.server_address + "/vpi" + ApiUrl.GetExtensions);
                    if (response.Count > 0)
                    {
                        var distinctList = response.Where(x => x.ramais != GetLoginResponse.user_name).OrderBy(x => x.id).Select(x => x.ramais).Distinct().ToList();

                        foreach (var item in distinctList)
                        {
                            _list.Add(new GateListModel
                            {
                                Image = "ic_call_orange.png",
                                Visible = false,
                                Heading = item,
                                BackgroundColor = Color.FromHex("#ffffff"),
                                TextColor = Color.Black,
                                VisibleRoom = true,
                            });
                        }

                        apartmentListView.FlowItemsSource = _list;
                        apartmentListView.IsVisible = true;
                    }
                    else
                    {
                        NoData.IsVisible = true;
                    }
                    LoaderPopup.CloseAllPopup();
                    LoadData.IsVisible = false;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.ToString(), "Ok");
            }
            finally
            {

            }
        }

        private void apartmentListView_FlowItemTapped(object sender, ItemTappedEventArgs e)
        {
            var selectedItem = e.Item as GateListModel;

            string phoneCall = selectedItem.Heading;

            if (LinphoneCore.CallsNb == 0)
            {
                LinphoneCore.VideoAdaptiveJittcompEnabled = true;
                var addr = LinphoneCore.InterpretUrl(phoneCall);
                CallParams CallParams = LinphoneCore.CreateCallParams(LinphoneCore.CurrentCall);
                CallParams.VideoEnabled = true;
                //CallParams.VideoDirection = MediaDirection.SendRecv;
                LinphoneCore.InviteAddressWithParams(addr, CallParams);
                outGoingCall = true;
                DialingKeyboard.outGoingCall = false;
            }
        }

        async void Try_Again_Button_Clicked(object sender, EventArgs e)
        {
            await GetData();
        }
    }
}
