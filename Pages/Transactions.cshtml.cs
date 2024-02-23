using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Memories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Memories.Pages
{
    public class TransactionsModel : PageModel
    {
        public string ClientId = "27387462";
        public string ClientSecret = "jFBm--OsYbQZTuVJgHao7QoFqllfUGSmCvl46CFedfU1";
        public string ENCRP_KEY = "6ePABf3mCkQeAKiTyT6GQTZtm_qdVwYxdPLBWbq_gL4ZVNRvTf1lO7iclmDAmGwNSGn5g4tUGotiWYzWzHArgoNEgRcu14Q-9iW7Lb08kpM1";
        private MemoriesContext _context;
        public TransactionsModel(MemoriesContext context)
        {
            _context = context;
        }
        public class AuthToken
        {
            public string AccessToken { get; set; }
            public string Status { get; set; }

        }

        public class TransactionDataConfirm
        {
            public string Payid { get; set; }
            public string Encrypmerch { get; set; }
            public string Authkey { get; set; }
        }


        public class AuthSettings
        {
            public string Url { get; set; }
            public string ClientId { get; set; }
            public string ClientSecret { get; set; }
            public string ENCRP_KEY { get; set; }
        }

        public class TransactionResult
        {
            public string Status { get; set; }
            public string Amount { get; set; }
            public string TrackId { get; set; }
            public string PayType { get; set; }
            public string PaymentId { get; set; }
            public string ReceiptNo { get; set; }
            public string AuthCode { get; set; }
            public string PostDate { get; set; }
            public string ReferenceId { get; set; }
            public string TransactionId { get; set; }
            public string Message { get; set; }
            public string PayId { get; set; }
            public string MerchUdf1 { get; set; }
            public string MerchUdf2 { get; set; }
            public string MerchUdf3 { get; set; }
            public string CCMessage { get; set; }
            public string MerchUdf4 { get; set; }
            public string MerchUdf5 { get; set; }
        }

        public void OnGet(int TransactionId)
        {
            var transaction = _context.Payments.FirstOrDefault(e => e.PaymentModelId == TransactionId);
            var gateway = _context.Gateways.FirstOrDefault(a => a.GateWayId == transaction.GateWayId);
            if (transaction.GateWayId == 2)
            {
                AuthSettings cBKAuthSettings = new AuthSettings
                {
                    Url = gateway.AuthUrl,
                    ClientId = gateway.UserName,
                    ClientSecret = gateway.Password,
                    ENCRP_KEY = gateway.ENCRP_KEY
                };

                AuthToken cBKAuthToken = _download_serialized_object_data(cBKAuthSettings);

                TransactionDataConfirm transactionData = new TransactionDataConfirm
                {
                    Authkey = cBKAuthToken.AccessToken,
                    Encrypmerch = cBKAuthSettings.ENCRP_KEY,
                    Payid = transaction.OrderNumber.ToString()
                };

                var url = "https://pgtest.cbk.com/ePay/api/cbk/online/pg/Verify/";

                var transactionResult = _download_serialized_json_data1<TransactionResult>(url, transactionData, cBKAuthSettings);

                Response.Redirect(JsonConvert.SerializeObject(transactionResult));
            }
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
                }
                catch (Exception ex)
                {
                    string exep = ex.Message;
                }


            }
            return tokenResult;
        }

        private TransactionResult _download_serialized_json_data1<TransactionResult>(string url, TransactionDataConfirm transactionDataConfirm, AuthSettings cBKAuthSettings) where TransactionResult : new()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            var dataString = string.Empty;

            using (var w = new WebClient())
            {
                var json_data = string.Empty;
                dataString = JsonConvert.SerializeObject(transactionDataConfirm);
                w.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                var ClientId = cBKAuthSettings.ClientId;
                var ClientSecret = cBKAuthSettings.ClientSecret;
                var ClientMerch = cBKAuthSettings.ENCRP_KEY;

                var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{ClientId}:{ClientSecret}"));
                w.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authHeader);

                try
                {
                    json_data = w.UploadString(new Uri(url), "POST", dataString);

                }
                catch (WebException ex)
                {
                    string exep = ex.Message;
                }
                catch (Exception ex)
                {
                    string exep = ex.Message;
                }
                return !string.IsNullOrEmpty(json_data) ? JsonConvert.DeserializeObject<TransactionResult>(json_data) : new TransactionResult();
            }
        }
    }
}
