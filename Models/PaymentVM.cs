using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class PaymentVM
    {
        public int PaymentModelId { get; set; }
        public string estimatedtime { get; set; }
        public Gateway Gateway { get; set; }
        public int Amout { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OrderNumber { get; set; }
        public string PhoneNumber { get; set; }
        public int? PaymentStatusId { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
