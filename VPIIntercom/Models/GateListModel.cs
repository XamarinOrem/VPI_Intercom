using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace VPIXamarinIntercom.Models
{
    public class GateListModel : INotifyPropertyChanged
    {
        private string _img;

        public string Image
        {
            get { return _img; }
            set
            {
                _img = value;

                OnPropertyChanged(); // Notify, that Image has been changed
            }
        }

        public Color BackgroundColor { get; set; }

        public bool Visible { get; set; }

        public bool VisibleRoom { get; set; }

        public Color TextColor { get; set; }

        public string Heading { get; set; }

        public string Description { get; set; }

        public bool Enable { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
