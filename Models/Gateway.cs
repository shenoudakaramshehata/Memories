using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class Gateway
    {
        [Key]
        public int GateWayId { get; set; }
        public string Title { get; set; }
        public string TestURL { get; set; }
        public string success_url { get; set; }
        public string error_url { get; set; }

        public string ProductionURL { get; set; }
        public string MerchantUdf1 { get; set; }
        public string MerchantUdf2 { get; set; }
        public string MerchantUdf3 { get; set; }
        public string MerchantUdf4 { get; set; }
        public string MerchantUdf5 { get; set; }
        public string MerchantPaymentRef { get; set; }
        public string MerchReturnUrl { get; set; }
        public string AuthUrl { get; set; }
        public string MerchPayType { get; set; }
        public string MerchantPaymentLang { get; set; }
        public string ENCRP_KEY { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string MerchantId { get; set; }
        public string ApiKey { get; set; }
        public int? Testmode { get; set; }
        public string version { get; set; }
    }
}
