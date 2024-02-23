using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Memories.Models;

using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Text;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using NToastNotify;
using Memories.Data;
namespace Memories.Pages
{
    public class ResponseModel : PageModel
    {
        public string ClientId = "27387462";
        public string ClientSecret = "-B76ERtLowiNGtH8fYTTU8yqkeAii9O99bP8lhD6xh81";
        public string ENCRP_KEY = "FbwZvfx-xudBGOszQa-nkarUVel9jDSqm7MKZKoJ9KyybsEXb9hfiP7gaJ3--BSL78VK-k2rd6tTeISdpCnRu9gSlspqr0jU90C1h-k3yXs1";
        private MemoriesContext _context;
        public TransactionResult transaction { get; set; }
        public string Message { get; set; }
        public PaymentModel payment { get; set; }

        public ResponseModel(MemoriesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string encrp,string ErrorCode,string PayTrackId)
        {
            if (encrp != null)
            {
                AuthSettings cBKAuthSettings = new AuthSettings
                {
                    Url = "https://pg.cbk.com/ePay/api/cbk/online/pg/merchant/Authenticate",
                    ClientId = ClientId,
                    ClientSecret = ClientSecret,
                    ENCRP_KEY = ENCRP_KEY
                };

                AuthToken cBKAuthToken = _download_serialized_object_data(cBKAuthSettings);
                Random random = new Random();

                if (cBKAuthToken.Status == "1")
                {


                    var url = "https://pg.cbk.com/ePay/api/cbk/online/pg/GetTransactions/" + encrp + "/" + cBKAuthToken.AccessToken;

                    transaction = _download_serialized_json_data<TransactionResult>(url, cBKAuthSettings);
                    if (transaction == null)
                    {
                        Message = "SomeThing Went Error";
                    }
                    payment = _context.Payments.FirstOrDefault(e => e.OrderNumber == transaction.PayId);
                    if (payment == null)
                    {
                        Message = "SomeThing Went Error";
                    }
                    if (transaction.Status == "1")
                    {
                        payment.PaymentStatusId = 2;
                        payment.Status = "1";
                        payment.Description = "Payment Successfully Authorized & Captured";

                        payment.Message = transaction.Message;
                        payment.Auth = transaction.AuthCode;
                        payment.PaymentID = transaction.PaymentId;
                        payment.payment_type = transaction.PayType;
                        payment.ReceiptNo = transaction.ReceiptNo;
                        payment.Ref = transaction.ReferenceId;
                        payment.TrackID = transaction.TrackId;
                        payment.TranID = transaction.TransactionId;
                        payment.MerchUdf1 = transaction.MerchUdf1;
                        payment.MerchUdf2 = transaction.MerchUdf2;
                        payment.MerchUdf3 = transaction.MerchUdf3;
                        payment.MerchUdf4 = transaction.MerchUdf4;
                        payment.MerchUdf5 = transaction.MerchUdf5;
                        var UpdatedTransaction = _context.Payments.Attach(payment);
                        UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        Message = "Payment Successfully Authorized & Captured";
                    }
                    else if (transaction.Status == "0")
                    {
                        payment.PaymentStatusId = 3;
                        payment.Status = "0";
                        payment.Description = "Invalid Access. Re-generate new API key";
                        payment.Message = transaction.Message;
                        var UpdatedTransaction = _context.Payments.Attach(payment);
                        UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        Message = "Invalid Access. Re-generate new API key";
                    }
                    else if (transaction.Status == "-1")
                    {
                        payment.PaymentStatusId = 3;
                        payment.Status = "-1";
                        payment.Description = "Error/Invalid {encrp}/{payid}";
                        payment.Message = transaction.Message;
                        var UpdatedTransaction = _context.Payments.Attach(payment);
                        UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        Message = "Error/Invalid {encrp}/{payid}";
                    }
                    else if (transaction.Status == "2")
                    {
                        payment.PaymentStatusId = 3;
                        payment.Status = "2";
                        payment.Description = "Payment Failed";
                        payment.Message = transaction.Message;
                        var UpdatedTransaction = _context.Payments.Attach(payment);
                        UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        Message = "Payment Failed";
                    }
                    else if (transaction.Status == "3")
                    {
                        payment.PaymentStatusId = 3;
                        payment.Status = "3";
                        payment.Description = "Payment Cancelled or Expired";
                        payment.Message = transaction.Message;
                        var UpdatedTransaction = _context.Payments.Attach(payment);
                        UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        _context.SaveChanges();
                        Message = "Payment Cancelled or Expired";
                    }
                }

                else
                {
                    Message = "Authentication Failed";
                }

            }
            else
            {
                if (ErrorCode != null && PayTrackId != null)
                {
                    var payment = _context.Payments.FirstOrDefault(e => e.OrderNumber == PayTrackId);
                    if (payment == null)
                    {
                        Message = "SomeThing went Error";
                    }

                    payment.ErrorCode = ErrorCode;
                    payment.PaymentStatusId = 3;
                    var UpdatedTransaction = _context.Payments.Attach(payment);
                    UpdatedTransaction.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                    Message = "SomeThing went Error, Try Again!";

                }
                else
                {
                    Message = "SomeThing went Error";
                }
            }
            return Page();
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
        private TransactionResult _download_serialized_json_data<TransactionResult>(string url, AuthSettings cBKAuthSettings) where TransactionResult : new()
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var w = new WebClient())
            {
                var json_data = string.Empty;

                w.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                var ClientId = cBKAuthSettings.ClientId;
                var ClientSecret = cBKAuthSettings.ClientSecret;
                var ClientMerch = cBKAuthSettings.ENCRP_KEY;

                var authHeader = Convert.ToBase64String(Encoding.Default.GetBytes($"{ClientId}:{ClientSecret}"));
                w.Headers[HttpRequestHeader.Authorization] = string.Format("Basic {0}", authHeader);

                try
                {
                    json_data = w.DownloadString(url);
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
