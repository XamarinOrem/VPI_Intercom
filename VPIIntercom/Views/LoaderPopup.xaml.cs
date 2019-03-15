using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace VPIIntercom.Views
{
    public partial class LoaderPopup : PopupPage
    {
        public LoaderPopup()
        {
            InitializeComponent();
        }

        public static async void CloseAllPopup()
        {
            await App.Current.MainPage.Navigation.PopAllPopupAsync(false);
        }
    }
}
