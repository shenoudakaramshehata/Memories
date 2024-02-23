using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NToastNotify;
using Memories.Data;
using Memories.Models;
using Microsoft.EntityFrameworkCore;

namespace Memories.Pages
{
    public class PayModel : PageModel
    {
        private MemoriesContext _context;
        public HttpClient httpClient { get; set; }
        private readonly IToastNotification _toastNotification;
        public string ClientId = "27387462";
        public string ClientSecret = "-B76ERtLowiNGtH8fYTTU8yqkeAii9O99bP8lhD6xh81";
        public string ENCRP_KEY = "FbwZvfx-xudBGOszQa-nkarUVel9jDSqm7MKZKoJ9KyybsEXb9hfiP7gaJ3--BSL78VK-k2rd6tTeISdpCnRu9gSlspqr0jU90C1h-k3yXs1";
        public SessionInformation session { get; set; }
        public Session sessionInfo { get; set; }
        public Models.ErrorModel errorM { get; set; }
        public int transactionid;

        public PayModel(MemoriesContext context, IToastNotification toastNotification)
        {
            httpClient = new HttpClient();
            _context = context;
            _toastNotification = toastNotification;
        }
        public async Task<IActionResult> OnGet(int TransactionId)
        {
            var transaction = _context.Payments.FirstOrDefault(e => e.PaymentModelId == TransactionId);
            var gateway = _context.Gateways.FirstOrDefault(a => a.GateWayId == transaction.GateWayId);
            var expire = transaction.TransactionDate.AddHours(24);

            if (DateTime.Now > expire && transaction.PaymentStatusId==1)
            {
                transaction.PaymentStatusId = 4;
                transaction.Active = false;
                _context.Attach(transaction).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToPage("ExpiredLink");
            }
            //if (transaction.PaymentStatusId == 2 || transaction.PaymentStatusId == 3)
            //{
            //    return RedirectToPage("TransactionStatus", new { transactionId = transaction.PaymentModelId });
            //}
            
            if (transaction.GateWayId == 1)
            {
                //string requesturl = gateway.TestURL + "/version/" + gateway.version + "/merchant/" + gateway.MerchantId + "/session";
                //var fields = new
                //{
                //    apiOperation = "INITIATE_CHECKOUT",
                //    interaction = new
                //    {
                //        operation = "PURCHASE",
                //        returnUrl = "https://Memories.beintrackpay.com/returnUrl?orderId=" + transaction.OrderNumber
                //        //returnUrl = "https://localhost:44354/returnUrl?orderId=" + transaction.OrderNumber,
                //    },
                //    order = new
                //    {
                //        amount = transaction.Amout,
                //        currency = "KWD",
                //        description = "Ordered goods",
                //        id = transaction.OrderNumber,
                //        reference = transaction.OrderNumber,
                //    },
                //    transaction = new
                //    {

                //        reference = transaction.PaymentModelId
                //    }
                //};
                //var content = new StringContent(JsonConvert.SerializeObject(fields), Encoding.UTF8, "application/json");


                //var authenticationBytes = Encoding.ASCII.GetBytes(gateway.UserName + ":" + gateway.Password);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var task = httpClient.PostAsync(requesturl, content);
                //var result = await task.Result.Content.ReadAsStringAsync();

                //sessionInfo = JsonConvert.DeserializeObject<Session>(result);
                //if (sessionInfo.result == "SUCCESS")
                //{
                //    JObject jObject = JObject.Parse(result);
                //    session = jObject["session"].ToObject<SessionInformation>();
                //    transaction.successIndicator = sessionInfo.successIndicator;
                //    var Updatedpayment = _context.Payments.Attach(transaction);
                //    Updatedpayment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //    _context.SaveChanges();
                //    return RedirectToPage("nbkcheckout", new { sessionId = session.id, sessionversion = session.version, successindicator = sessionInfo.successIndicator, merchantId = gateway.MerchantId, orderId = transaction.OrderNumber, orderamount = transaction.Amout });
                //}
                //else
                //{
                //    JObject jObject = JObject.Parse(result);

                //    errorM = jObject["error"].ToObject<Models.ErrorModel>();
                //    return RedirectToPage("SomethingwentError", new { Message = errorM.explanation });
                //}
                return RedirectToPage("nbkcheckout", new { id = transaction.PaymentModelId });
            }
            else if (transaction.GateWayId == 2)
            {
                AuthSettings cBKAuthSettings = new AuthSettings
                {
                    Url = gateway.AuthUrl,
                    ClientId = gateway.UserName,
                    ClientSecret = gateway.Password,
                    ENCRP_KEY = gateway.ENCRP_KEY
                };

                AuthToken cBKAuthToken = _download_serialized_object_data(cBKAuthSettings);
                Random random = new Random();

                if (cBKAuthToken.Status == "1")
                {
                    var values = new NameValueCollection
                    {
                        ["tij_MerchantEncryptCode"] = cBKAuthSettings.ENCRP_KEY,
                        ["tij_MerchantPaymentAmount"] = transaction.Amout.ToString(),
                        ["tij_MerchantPaymentRef"] = gateway.MerchantPaymentRef,
                        ["tij_MerchantPaymentLang"] = gateway.MerchantPaymentLang,
                        ["tij_MerchantPaymentTrack"] = transaction.OrderNumber.ToString(),
                        ["tij_MerchantUdf1"] = gateway.MerchantUdf1,
                        ["tij_MerchantUdf2"] = gateway.MerchantUdf2,
                        ["tij_MerchantUdf3"] = gateway.MerchantUdf3,
                        ["tij_MerchantUdf4"] = gateway.MerchantUdf4,
                        ["tij_MerchantUdf5"] = gateway.MerchantUdf5,
                        ["tij_MerchAuthKeyApi"] = cBKAuthToken.AccessToken,
                        ["tij_MerchPayType"] = gateway.MerchPayType,
                        //["tij_MerchReturnUrl"] = "https://localhost:44354/Response"
                        ["tij_MerchReturnUrl"] = "https://memories.beintrackpay.com/Response"
                    };

                    var url = gateway.TestURL + cBKAuthToken.AccessToken;

                    RedirectWithData(values, url);


                }
                else
                {
                    await Response.WriteAsync("Authentication Failed");
                }

            }
            else if (transaction.GateWayId == 3)
            {
                //string requesturl = "https://ap-gateway.mastercard.com/api/rest/version/67/merchant/BEINTRACK/session";
                ////string requesturl = gateway.TestURL + "/version/" + gateway.version + "/merchant/" + gateway.MerchantId + "/session";
                //var fields = new
                //{
                //    apiOperation = "INITIATE_CHECKOUT",
                //    apiPassword= "12aa799d8ad04626fb7f739550674868",
                //    apiUsername= "merchant.BEINTRACK",
                //    interaction = new
                //    {
                //        operation = "VERIFY",
                //        returnUrl = "https://localhost:44354/returnUrl?orderId=" + transaction.OrderNumber

                //    },
                //    order = new
                //    {
                //        amount = transaction.Amout,
                //        currency = "KWD",
                //        id = transaction.OrderNumber
                //    }
                //};
                //var content = new StringContent(JsonConvert.SerializeObject(fields), Encoding.UTF8, "application/json");


                //var authenticationBytes = Encoding.ASCII.GetBytes(gateway.UserName + ":" + gateway.Password);

                //httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
                //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //var task = httpClient.PostAsync(requesturl, content);
                //var result = await task.Result.Content.ReadAsStringAsync();

                //sessionInfo = JsonConvert.DeserializeObject<Session>(result);
                //if (sessionInfo.result == "SUCCESS")
                //{
                //    JObject jObject = JObject.Parse(result);
                //    session = jObject["session"].ToObject<SessionInformation>();
                //    transaction.successIndicator = sessionInfo.successIndicator;
                //    var Updatedpayment = _context.Payments.Attach(transaction);
                //    Updatedpayment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                //    _context.SaveChanges();
                //    return RedirectToPage("CheckOut", new { sessionId = session.id, sessionversion = session.version, successindicator = sessionInfo.successIndicator, merchantId = gateway.MerchantId, orderId = transaction.OrderNumber, orderamount = transaction.Amout });
                //}
                //else
                //{
                //    JObject jObject = JObject.Parse(result);

                //    errorM = jObject["error"].ToObject<Models.ErrorModel>();
                //    return RedirectToPage("SomethingwentError", new { Message = errorM.explanation });
                //}
                return RedirectToPage("MasterCardCheckout", new { id = transaction.PaymentModelId });
            }
            return Page();
        }
        public void RedirectWithData(NameValueCollection data, string url)
        {
            HttpResponse response = HttpContext.Response;
            response.Clear();

            StringBuilder s = new StringBuilder();
            s.Append("<html>");
            s.AppendFormat("<body onload='document.forms[\"form\"].submit()'>");
            s.AppendFormat("<form name='form' action='{0}' method='post'>", url);
            foreach (string key in data)
            {
                s.AppendFormat("<input type='hidden' name='{0}' value='{1}' />", key, data[key]);
            }
            s.Append("</form></body></html>");
            Console.WriteLine(s.ToString());
            response.WriteAsync(s.ToString());
            //response.End();
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
