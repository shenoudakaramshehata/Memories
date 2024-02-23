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


namespace Memories.GateWays
{
    //public class CBK
    //{
    //    public string ClientId = "27387462";
    //    public string ClientSecret = "jFBm--OsYbQZTuVJgHao7QoFqllfUGSmCvl46CFedfU1";
    //    public string ENCRP_KEY = "6ePABf3mCkQeAKiTyT6GQTZtm_qdVwYxdPLBWbq_gL4ZVNRvTf1lO7iclmDAmGwNSGn5g4tUGotiWYzWzHArgoNEgRcu14Q-9iW7Lb08kpM1";

    //    public class AuthToken
    //    {
    //        public string AccessToken { get; set; }
    //        public string Status { get; set; }

    //    }

    //    public class AuthSettings
    //    {
    //        public string Url { get; set; }
    //        public string ClientId { get; set; }
    //        public string ClientSecret { get; set; }
    //        public string ENCRP_KEY { get; set; }
    //    }
    //    public void OnGet()
    //    {
    //        AuthSettings cBKAuthSettings = new AuthSettings
    //        {
    //            Url = "https://pgtest.cbk.com/ePay/api/cbk/online/pg/merchant/Authenticate",
    //            ClientId = ClientId,
    //            ClientSecret = ClientSecret,
    //            ENCRP_KEY = ENCRP_KEY
    //        };

    //        AuthToken cBKAuthToken = _download_serialized_object_data(cBKAuthSettings);
    //        Random random = new Random();

    //        if (cBKAuthToken.Status == "1")
    //        {
    //            var values = new NameValueCollection
    //            {
    //                ["tij_MerchantEncryptCode"] = cBKAuthSettings.ENCRP_KEY,
    //                ["tij_MerchantPaymentAmount"] = "1",
    //                ["tij_MerchantPaymentRef"] = "test",
    //                ["tij_MerchantPaymentLang"] = "en",
    //                ["tij_MerchantPaymentTrack"] = random.Next().ToString(),
    //                ["tij_MerchantUdf1"] = "",
    //                ["tij_MerchantUdf2"] = "",
    //                ["tij_MerchantUdf3"] = "",
    //                ["tij_MerchantUdf4"] = "",
    //                ["tij_MerchantUdf5"] = "",
    //                ["tij_MerchAuthKeyApi"] = cBKAuthToken.AccessToken,
    //                ["tij_MerchPayType"] = "",
    //                ["tij_MerchReturnUrl"] = "https://localhost:44354/Response"
    //            };

    //            var url = "https://pgtest.cbk.com/ePay/pg/epay?_v=" + cBKAuthToken.AccessToken;

    //            RedirectWithData(values, url);


    //        }
    //        else
    //        {
    //            //Response.Write("Authentication Failed");
    //            Console.WriteLine("Authentication Failed");
    //        }



    //    }

    //    public void RedirectWithData(NameValueCollection data, string url)
    //    {
    //        HttpResponse response = HttpContext.;
    //        response.Clear();

    //        StringBuilder s = new StringBuilder();
    //        s.Append("<html>");
    //        s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
    //        s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
    //        foreach (string key in data)
    //        {
    //            s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
    //        }
    //        s.Append("</form></body></html>");
    //        Console.WriteLine(s.ToString());
    //        response.WriteAsync(s.ToString());
    //        //response.End();
    //    }

    //    private AuthToken _download_serialized_object_data(AuthSettings cBKAuthSettings)
    //    {
    //        var dataString = string.Empty;
    //        var tokenResult = new AuthToken();
    //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

    //        using (var client = new WebClient())
    //        {
    //            dataString = JsonConvert.SerializeObject(cBKAuthSettings);
    //            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

    //            var ClientId = cBKAuthSettings.ClientId;
    //            var ClientSecret = cBKAuthSettings.ClientSecret;
    //            var ClientMerch = cBKAuthSettings.ENCRP_KEY;

    //            var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{ClientId}:{ClientSecret}"));
    //            client.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authHeader);
    //            try
    //            {
    //                string cBKAuthToken = client.UploadString(new Uri(cBKAuthSettings.Url), "POST", dataString);
    //                tokenResult = JsonConvert.DeserializeObject<AuthToken>(cBKAuthToken);
    //            }
    //            catch (WebException ex)
    //            {
    //                string exep = ex.Message;
    //                tokenResult.Status = ex.ToString(); ;
    //                //save exception to log
    //            }
    //            catch (Exception ex)
    //            {
    //                string exep = ex.Message;
    //                tokenResult.Status = ex.ToString(); ;
    //            }


    //        }
    //        return tokenResult;
    //    }

    //}
}
