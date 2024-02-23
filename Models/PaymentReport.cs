using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class PaymentReport
    {
        public double Amount { get; set; }
        public int? GatewayId { get; set; }
        public int? paymentstatusId { get; set; }

        public string Email { get; set; }
        public string FirstName { get; set; }
        public string StatusTitle { get; set; }

        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string OrderNumber { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }
        public string Userid { get; set; }
        public string Active { get; set; }
        public bool issuccess { get; set; }
        public string payment_type { get; set; }
        public string PaymentID { get; set; }
        public string Result { get; set; }
        public DateTime TransactionDate { get; set; }

        public string TranID { get; set; }
        public string Ref { get; set; }
        public string TrackID { get; set; }
        public string Auth { get; set; }
        public string gatewayTitle { get; set; }
    }
}
