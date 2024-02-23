using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Memories.Models;

namespace Memories.GateWays
{
    public class CBKGateWayModel : PageModel
    {
        public string ClientId = "27387462";
        public string ClientSecret = "jFBm--OsYbQZTuVJgHao7QoFqllfUGSmCvl46CFedfU1";
        public string ENCRP_KEY = "6ePABf3mCkQeAKiTyT6GQTZtm_qdVwYxdPLBWbq_gL4ZVNRvTf1lO7iclmDAmGwNSGn5g4tUGotiWYzWzHArgoNEgRcu14Q-9iW7Lb08kpM1";
        
        
        public class AuthToken
        {
            public string AccessToken { get; set; }
            public string Status { get; set; }

        }

        public class AuthSettings
        {
            public string Url { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string ENCRP_KEY { get; set; }
        }
        public AuthToken OnGet()
        {
            AuthSettings cBKAuthSettings = new AuthSettings
            {
                Url = "https://pgtest.cbk.com/ePay/api/cbk/online/pg/merchant/Authenticate",
                ClientId = ClientId,
                ClientSecret = ClientSecret,
                ENCRP_KEY = ENCRP_KEY
            };

            AuthToken cBKAuthToken = _download_serialized_object_data(cBKAuthSettings);
            return cBKAuthToken;
        }


        private AuthToken _download_serialized_object_data(AuthSettings cBKAuthSettings)
        {
            var dataString = string.Empty;
            var tokenResult = new AuthToken();
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var client = new WebClient())
            {
                dataString = JsonConvert.SerializeObject(cBKAuthSettings);
                client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                var ClientId = cBKAuthSettings.ClientId;
                var ClientSecret = cBKAuthSettings.ClientSecret;
                var ClientMerch = cBKAuthSettings.ENCRP_KEY;

                var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{ClientId}:{ClientSecret}"));
                client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authHeader);
                try
                {
                    string cBKAuthToken = client.UploadString(new Uri(cBKAuthSettings.Url), "POST", dataString);
                    tokenResult = JsonConvert.DeserializeObject<AuthToken>(cBKAuthToken);
                }
                catch (WebException ex)
                {
                    string exep = ex.Message;
                    tokenResult.Status = ex.ToString(); ;
                    //save exception to log
                }
                catch (Exception ex)
                {
                    string exep = ex.Message;
                    tokenResult.Status = ex.ToString(); ;
                }


            }
            return tokenResult;
        }

    }
}
