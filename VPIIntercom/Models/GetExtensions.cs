using System;
namespace VPIIntercom.Models
{
    public class GetExtensions
    {
        public string ramais { get; set; }
        public int id
        {
            get
            {
                return string.IsNullOrEmpty(ramais) ? -1 : System.Convert.ToInt32(ramais);
            }
        }
    }
}
