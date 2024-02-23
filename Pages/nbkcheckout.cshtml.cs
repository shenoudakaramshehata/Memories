using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Memories.Pages
{
    public class nbkcheckoutModel : PageModel
    {
        //public string sessionId { get; set; }
        //public string sessionversion { get; set; }
        //public string successindicator { get; set; }
        //public string merchantId { get; set; }
        //public string orderId { get; set; }
        //public string orderamount { get; set; }
        //public void OnGet(string sessionId, string sessionversion, string successindicator, string merchantId, string orderId, string orderamount)
        //{
        //    this.sessionId = sessionId;
        //    this.sessionversion = sessionversion;
        //    this.successindicator = successindicator;
        //    this.merchantId = merchantId;
        //    this.orderId = orderId;
        //    this.orderamount = orderamount;
        //}
        public SessionInformation session { get; set; }
        public Session sessionInfo { get; set; }
        public HttpClient httpClient { get; set; }
        public Models.ErrorModel errorM { get; set; }
        private MemoriesContext _context;

        public nbkcheckoutModel(MemoriesContext context)
        {
            httpClient = new HttpClient();
            _context = context;
        }
        public PaymentModel transaction { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
            transaction = _context.Payments.Find(id);

            string requesturl = "https://nbkpayment.gateway.mastercard.com/api/rest/version/65/merchant/900185001/session";
            var fields = new
            {
                apiOperation = "INITIATE_CHECKOUT",
                interaction = new
                {
                    operation = "PURCHASE",
                    returnUrl = "https://memories.beintrackpay.com/nbkreturn?orderId=" + transaction.OrderNumber

                    //returnUrl = "https://localhost:44354/returnUrl?orderId=" + transaction.OrderNumber,
                },
                order = new
                {
                    amount = transaction.Amout,
                    currency = "KWD",
                    description = "Ordered goods",
                    id = transaction.OrderNumber,
                    reference = transaction.OrderNumber,
                },
                transaction = new
                {

                    reference = transaction.PaymentModelId
                }

            };

            var content = new StringContent(JsonConvert.SerializeObject(fields), Encoding.UTF8, "application/json");


            var authenticationBytes = Encoding.ASCII.GetBytes("merchant.900185001" + ":" + "937cd5c82ab0949b7afad77cd8958122");

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(authenticationBytes));
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var task = httpClient.PostAsync(requesturl, content);
            var result = await task.Result.Content.ReadAsStringAsync();

            sessionInfo = JsonConvert.DeserializeObject<Session>(result);
            if (sessionInfo.result == "SUCCESS")
            {
                JObject jObject = JObject.Parse(result);
                session = jObject["session"].ToObject<SessionInformation>();
                transaction.successIndicator = sessionInfo.successIndicator;
                var Updatedpayment = _context.Payments.Attach(transaction);
                Updatedpayment.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return Page();
            }
            else
            {
                JObject jObject = JObject.Parse(result);

                errorM = jObject["error"].ToObject<Models.ErrorModel>();
                return RedirectToPage("SomethingwentError", new { Message = errorM.explanation });
            }
        }
    }
}
