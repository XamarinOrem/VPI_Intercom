using System;
using System.Collections.Generic;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace VPIIntercom.Views
{
    public partial class ShowMessage : PopupPage
    {
        public ShowMessage(string message)
        {
            InitializeComponent();

            msgTxt.Text = message;

            CloseWhenBackgroundIsClicked = false;
        }
    }
}
