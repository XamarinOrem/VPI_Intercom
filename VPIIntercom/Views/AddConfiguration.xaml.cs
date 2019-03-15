using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Linphone;
using Rg.Plugins.Popup.Extensions;
using VPIIntercom.Models;
using VPIXamarinIntercom.Repo;
using Xamarin.Forms;

namespace VPIIntercom.Views
{
    public partial class AddConfiguration : ContentPage
    {
        private Core LinphoneCore
        {
            get
            {
                return ((App)App.Current).LinphoneCore;
            }
        }
        private CoreListener Listener;

        public AddConfiguration()
        {
            InitializeComponent();

            if(!string.IsNullOrEmpty(GetLoginResponse.server_address))
            {
                mainLayout.IsEnabled = false;
                apartmentLayout.IsVisible = true;
                connectGrid.IsEnabled = false;
            }
            else{
                Listener = Factory.Instance.CreateCoreListener();
                Listener.OnRegistrationStateChanged = OnRegistration;

                LinphoneCore.AddListener(Listener);
            }

            this.BackgroundImage = "ic_bg_dark.png";



            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void OnRegistration(Core lc, ProxyConfig config, RegistrationState state, string message)
        {
            if (state == RegistrationState.Ok)
            {
                SaveLoginResponse _obj = new SaveLoginResponse();
                _obj.user_name = txtEntry1.Text;
                _obj.password = txtEntry2.Text;
                _obj.server_address = txtEntry3.Text;
                _obj.building_name = txtEntry.Text;
                App.Database.SaveLoggedUser(_obj);
                GetLoginResponse.user_name = txtEntry1.Text;
                GetLoginResponse.password = txtEntry2.Text;
                GetLoginResponse.server_address = txtEntry3.Text;
                GetLoginResponse.building_name = txtEntry.Text;
                LoaderPopup.CloseAllPopup();
                await Navigation.PushAsync(new ApartmentListDark());
            }
            else if (state == RegistrationState.Failed)
            {
                LoaderPopup.CloseAllPopup();
                await App.Current.MainPage.Navigation.PushPopupAsync(new ShowMessage("Credencial inválida"));
                await Task.Delay(1000);
                await Navigation.PopPopupAsync();
            }
            else
            {
                LoaderPopup.CloseAllPopup();
            }
        }

        async void Login()
        {
            try
            {
                if (!CommonLib.checkconnection())
                {
                    await Navigation.PushPopupAsync(new NoInternetPopup());
                }
                else
                {
                    try
                    {
                        await App.Current.MainPage.Navigation.PushPopupAsync(new LoaderPopup());
                        var authInfo = Factory.Instance.CreateAuthInfo(txtEntry1.Text, null, txtEntry2.Text, null, null, txtEntry3.Text);
                        LinphoneCore.AddAuthInfo(authInfo);
                        String proxyAddress = "sip:" + txtEntry1.Text + "@" + txtEntry3.Text;
                        var identity = Factory.Instance.CreateAddress(proxyAddress);
                        var proxyConfig = LinphoneCore.CreateProxyConfig();
                        identity.Username = txtEntry1.Text;
                        identity.Domain = txtEntry3.Text;
                        identity.Transport = TransportType.Udp;
                        proxyConfig.Edit();
                        proxyConfig.IdentityAddress = identity;
                        proxyConfig.ServerAddr = txtEntry3.Text + ";transport=udp";
                        proxyConfig.Route = txtEntry3.Text;
                        proxyConfig.RegisterEnabled = true;

                        proxyConfig.Done();

                        LinphoneCore.AddProxyConfig(proxyConfig);
                        LinphoneCore.DefaultProxyConfig = proxyConfig;

                        LinphoneCore.RefreshRegisters();



                    }
                    catch (Exception)
                    {

                    }
                }
            }
            catch (Exception)
            {

            }
            finally
            {

            }

        }

        private void loginBtn_Clicked(object sender, System.EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEntry.Text))
            {
                errorMsg.IsVisible = true;
                frame1.BorderColor = Color.FromHex("#ff0000");
            }
            else
            {
                errorMsg.IsVisible = false;
                frame1.BorderColor = Color.FromHex("#ff7824");
            }
            if (string.IsNullOrEmpty(txtEntry1.Text))
            {
                errorMsg1.IsVisible = true;
                frame2.BorderColor = Color.FromHex("#ff0000");
            }
            else
            {
                errorMsg1.IsVisible = false;
                frame2.BorderColor = Color.FromHex("#ff7824");
            }
            if (string.IsNullOrEmpty(txtEntry2.Text))
            {
                errorMsg2.IsVisible = true;
                frame3.BorderColor = Color.FromHex("#ff0000");
            }
            else
            {
                errorMsg2.IsVisible = false;
                frame3.BorderColor = Color.FromHex("#ff7824");
            }
            if (string.IsNullOrEmpty(txtEntry3.Text))
            {
                errorMsg3.IsVisible = true;
                frame4.BorderColor = Color.FromHex("#ff0000");
            }
            else
            {
                errorMsg3.IsVisible = false;
                frame4.BorderColor = Color.FromHex("#ff7824");
            }
            if (!string.IsNullOrEmpty(txtEntry.Text) && !string.IsNullOrEmpty(txtEntry1.Text) && !string.IsNullOrEmpty(txtEntry2.Text) && !string.IsNullOrEmpty(txtEntry3.Text))
            {
                Login();
            }
            
        }

        private void Call_Keyboard_Tapped(object sender, System.EventArgs e)
        {
            var getImage = sender as Image;
            getImage.Source = "ic_radio_button_selected.png";
            Navigation.PushAsync(new DialingKeyboard());
        }

        private void Apartment_List_Tapped(object sender, System.EventArgs e)
        {
            var getImage = sender as Image;
            getImage.Source = "ic_radio_button_selected.png";
            Navigation.PushAsync(new ApartmentListDark());
        }

        private void txtEntry_TextChanged(object sender, TextChangedEventArgs e)
        {
            var getEntry = sender as Entry;
            if (!string.IsNullOrEmpty(getEntry.Text))
            {
                errorMsg.IsVisible = false;
                frame1.BorderColor = Color.FromHex("#ff7824");
            }
            else
            {
                errorMsg.IsVisible = true;
                frame1.BorderColor = Color.FromHex("#ff0000");
            }
        }

        private void txtEntry1_TextChanged(object sender, TextChangedEventArgs e)
        {
            var getEntry = sender as Entry;
            if (!string.IsNullOrEmpty(getEntry.Text))
            {
                errorMsg1.IsVisible = false;
                frame2.BorderColor = Color.FromHex("#ff7824");
            }
            else
            {
                errorMsg1.IsVisible = true;
                frame2.BorderColor = Color.FromHex("#ff0000");
            }
        }

        private void txtEntry2_TextChanged(object sender, TextChangedEventArgs e)
        {
            var getEntry = sender as Entry;
            if (!string.IsNullOrEmpty(getEntry.Text))
            {
                errorMsg2.IsVisible = false;
                frame3.BorderColor = Color.FromHex("#ff7824");
            }
            else
            {
                errorMsg2.IsVisible = true;
                frame3.BorderColor = Color.FromHex("#ff0000");
            }
        }

        private void txtEntry3_TextChanged(object sender, TextChangedEventArgs e)
        {
            var getEntry = sender as Entry;
            if (!string.IsNullOrEmpty(getEntry.Text))
            {
                errorMsg3.IsVisible = false;
                frame4.BorderColor = Color.FromHex("#ff7824");
            }
            else
            {
                errorMsg3.IsVisible = true;
                frame4.BorderColor = Color.FromHex("#ff0000");
            }
        }
    }
}
