using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using VPIIntercom.Models;

namespace VPIIntercom.Repo
{
    public class HttpClientBase: HttpClient
    {
        private static readonly HttpClientBase _instance = new HttpClientBase();

        CancellationTokenSource cts;
        static HttpClientBase()
        {

        }
        public HttpClientBase() : base()
        {

            // Setting the time span to get the api response
            TimeSpan time = new TimeSpan(0, 0, 60);
            Timeout = time;
            cts = new CancellationTokenSource();
            cts.CancelAfter(time);
        }

        public static HttpClientBase Instance
        {
            get
            {
                return _instance;
            }
        }

        #region Get All Extensions

        public async Task<List<GetExtensions>> GetAllExtensions(string url)
        {
            string response = string.Empty;
            // making the object of the response class which will use for deserialization of the response.
            List<GetExtensions> objData = new List<GetExtensions>();
            try
            {
                //getting the details
                var result = await GetAsync(new Uri(url));
                if (result.IsSuccessStatusCode)
                {
                    response = result.Content.ReadAsStringAsync().Result;
                    // deserialization of the response returning from the api
                    objData = JsonConvert.DeserializeObject<List<GetExtensions>>(response);
                }
            }

            catch (Exception)
            {
            }
            finally { }
            return objData;
        }

        #endregion

    }
}
