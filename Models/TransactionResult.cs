using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
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
}
