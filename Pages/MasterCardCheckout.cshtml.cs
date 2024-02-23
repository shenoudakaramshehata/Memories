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
    public class MasterCardCheckoutModel : PageModel
    {
        public SessionInformation session { get; set; }
        public Session sessionInfo { get; set; }
        public HttpClient httpClient { get; set; }
        public Models.ErrorModel errorM { get; set; }
        private MemoriesContext _context;

        public MasterCardCheckoutModel(MemoriesContext context)
        {
            httpClient = new HttpClient();
            _context = context;
        }
        public PaymentModel transaction { get; set; }
        public async Task<IActionResult> OnGet(int id)
        {
             transaction = _context.Payments.Find(id);
            string requesturl = "https://ap-gateway.mastercard.com/api/rest/version/65/merchant/BEINTRACK/session";
            var fields = new
            {
                apiOperation = "INITIATE_CHECKOUT",
                interaction = new
                {
                    operation = "PURCHASE",
                    returnUrl = "https://memories.beintrackpay.com/returnUrl?orderId=" + transaction.OrderNumber
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


            var authenticationBytes = Encoding.ASCII.GetBytes("merchant.BEINTRACK" + ":" + "12aa799d8ad04626fb7f739550674868");

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
