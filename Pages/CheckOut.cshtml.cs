using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Memories.Models;

namespace Memories.Pages
{
    public class CheckOutModel : PageModel
    {

        public string  sessionId { get; set; }
        public string sessionversion { get; set; }
        public string successindicator { get; set; }
        public string merchantId { get; set; }
        public string orderId { get; set; }
        public string orderamount { get; set; }
        public void OnGet(string sessionId,string sessionversion, string successindicator, string merchantId, string orderId, string orderamount)
        {
            this.sessionId = sessionId;
            this.sessionversion = sessionversion;
            this.successindicator = successindicator;
            this.merchantId = merchantId;
            this.orderId = orderId;
            this.orderamount = orderamount;
        }
    }
}
