using System;
using System.Threading.Tasks;
using Android.Content;
using Linphone;
using Rg.Plugins.Popup.Extensions;
using VPIIntercom.Droid;
using VPIIntercom.Models;
using Xamarin.Forms;

namespace VPIIntercom.Views
{
    public partial class DialingKeyboard : ContentPage
    {
        public static bool outGoingCall = false;
        private Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }

        private CoreListener Listener;

        public DialingKeyboard()
        {
            InitializeComponent();

            this.BackgroundImage = "ic_bg_dark.png";

            buildingName.Text = GetLoginResponse.building_name;

            NavigationPage.SetHasNavigationBar(this, false);

            if (Device.RuntimePlatform == Device.iOS)
            {
                btn1.CornerRadius = 30;
                btn2.CornerRadius = 30;
                btn3.CornerRadius = 30;
                btn4.CornerRadius = 30;
                btn5.CornerRadius = 30;
                btn6.CornerRadius = 30;
                btn7.CornerRadius = 30;
                btn8.CornerRadius = 30;
                btn9.CornerRadius = 30;
                btn10.CornerRadius = 30;
            }

            Listener = Factory.Instance.CreateCoreListener();
            Listener.OnCallStateChanged = OnCall;
            LinphoneCore.AddListener(Listener);
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

        private void Keypad_Button_Clicked(object sender, EventArgs e)
        {
            placeholderLabel.IsVisible = false;
            dialingTextEntry.IsVisible = true;
            backspaceBtn.IsVisible = true;
            var getBtn = sender as Button;
            getBtn.BackgroundColor = Color.White;
            getBtn.TextColor = Color.Red;
            dialingTextEntry.Text = dialingTextEntry.Text + getBtn.Text;
        }

        private void Keypad_Button_Released(object sender, EventArgs e)
        {
            var getBtn = sender as Button;
            getBtn.BackgroundColor = Color.Transparent;
            getBtn.TextColor = Color.White;
        }

        private void Back_Space_Tapped(object sender, EventArgs e)
        {
            dialingTextEntry.Text = dialingTextEntry.Text.Remove(dialingTextEntry.Text.Length - 1);
        }

        private void dialingTextEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dialingTextEntry.Text.Length == 0)
            {
                dialingTextEntry.IsVisible = false;
                backspaceBtn.IsVisible = false;
                placeholderLabel.IsVisible = true;
            }
        }

        private void Cancel_Btn_Tapped(object sender, EventArgs e)
        {
            dialingTextEntry.Text = string.Empty;
            dialingTextEntry.IsVisible = false;
            backspaceBtn.IsVisible = false;
            placeholderLabel.IsVisible = true;
        }

        private async void CHAMAR_Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(dialingTextEntry.Text))
                {
                    await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage("Por favor insira o número para ligar"));
                    await Task.Delay(1000);
                    await Navigation.PopPopupAsync();
                }
                else
                {
                    if (LinphoneCore.CallsNb == 0)
                    {
                        var addr = LinphoneCore.InterpretUrl(dialingTextEntry.Text);
                        CallParams CallParams = LinphoneCore.CreateCallParams(null);
                        CallParams.VideoEnabled = true;
                        LinphoneCore.InviteAddressWithParams(addr, CallParams);
                        outGoingCall = true;
                        ApartmentListDark.outGoingCall = false;
                    }

                }
            }
            catch (Exception)
            {

            }
        }
    }
}
