using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace VPIXamarinIntercom.CustomControls
{
    public class CustomButton : Button
    {
        public event EventHandler Pressed;
        public event EventHandler Released;

        public virtual void OnPressed()
        {
            Pressed?.Invoke(this, EventArgs.Empty);
        }

        public virtual void OnReleased()
        {
            Released?.Invoke(this, EventArgs.Empty);
        }
    }
}
