using Plugin.Connectivity;
using System;

namespace VPIXamarinIntercom.Repo
{
    public class CommonLib
    {
        /// <summary>
        /// This is the method that will check the connectivity of the internet and returns the response true if internet is
        /// connected or false if the internet is not connected.
        /// </summary>
        /// <returns></returns>
        public static bool checkconnection()
        {
            var con = CrossConnectivity.Current.IsConnected;
            return con == true ? true : false; // returning the bool value either true or false based on connectivity.
        }
    }
}
