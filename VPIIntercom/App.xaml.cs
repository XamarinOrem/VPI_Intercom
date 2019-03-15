using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Linphone;
using VPIIntercom.Models;
using VPIIntercom.Views;
using VPIXamarinIntercom.Data;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace VPIIntercom
{
    public partial class App : Application
    {

        SaveLoginResponse _getLoginDetails = App.Database.GetLoginUser();

        public static bool isCall = false;

        public LinphoneManager Manager { get; set; }

        public static string ConfigFilePath { get; set; }
        public static string FactoryFilePath { get; set; }

        public Core LinphoneCore
        {
            get
            {
                return Manager.Core;
            }
        }

        public App()
        {
            InitializeComponent();

            Manager = new LinphoneManager();
            Manager.Init(ConfigFilePath, FactoryFilePath);

            if (_getLoginDetails != null)
            {
                if (!string.IsNullOrEmpty(_getLoginDetails.server_address))
                {
                    GetLoginResponse.user_name = _getLoginDetails.user_name;
                    GetLoginResponse.server_address = _getLoginDetails.server_address;
                    GetLoginResponse.password = _getLoginDetails.password;
                    GetLoginResponse.building_name = _getLoginDetails.building_name;
                }
            }
            GetMainPage();
        }

        public StackLayout getLayoutView()
        {
            return MainPage.FindByName<StackLayout>("stack_layout");
        }

        public void Register()
        {
            var authInfo = Factory.Instance.CreateAuthInfo(GetLoginResponse.user_name, null, GetLoginResponse.password, null, null, GetLoginResponse.server_address);
            LinphoneCore.AddAuthInfo(authInfo);
            String proxyAddress = "sip:" + GetLoginResponse.user_name + "@" + GetLoginResponse.server_address;
            var identity = Factory.Instance.CreateAddress(proxyAddress);
            var proxyConfig = LinphoneCore.CreateProxyConfig();
            identity.Username = GetLoginResponse.user_name;
            identity.Domain = GetLoginResponse.server_address;
            identity.Transport = TransportType.Udp;
            proxyConfig.Edit();
            proxyConfig.IdentityAddress = identity;
            proxyConfig.ServerAddr = GetLoginResponse.server_address + ";transport=udp";
            proxyConfig.Route = GetLoginResponse.server_address;
            proxyConfig.RegisterEnabled = true;

            proxyConfig.Done();

            LinphoneCore.AddProxyConfig(proxyConfig);
            LinphoneCore.DefaultProxyConfig = proxyConfig;

            LinphoneCore.RefreshRegisters();
        }

        public void GetMainPage()
        {
            if (!string.IsNullOrEmpty(GetLoginResponse.server_address))
            {
                Register();
            }
            MainPage = new NavigationPage(new AddConfiguration());
        } 

        private static Database _database;

        /// <summary>
        /// Getting the database path to be used statically in th entire application.
        /// </summary>
        public static Database Database
        {
            get
            {
                if (_database == null)
                {
                    try
                    {
#if __IOS__
                        string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

                        if (!Directory.Exists(libFolder))
                        {
                            Directory.CreateDirectory(libFolder);
                        }

                        var path = Path.Combine(libFolder, "DBVPI.db3");

                        _database = new Database(path);

#else

                        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                        string _path = Path.Combine(path, "DBVPI.db3");
                        _database = new Database(_path);
#endif
                    }
                    catch (Exception)
                    {
                    }
                    finally { }
                }
                return _database;
            }
        }



        protected override void OnStart()
        {
            // Handle when your app starts
            Manager.Start();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes

        }
    }
}
