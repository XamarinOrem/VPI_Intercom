using System;
using VPIIntercom.Models;

namespace VPIIntercom.Repo
{
    public class ApiUrl
    {
        public static string MainUrl = "http://" + GetLoginResponse.server_address + "/vpi";

        #region Extensions

        public const string GetExtensions = "/retornaapto.php";

        #endregion
    }
}
