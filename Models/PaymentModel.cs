using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class PaymentModel
    {

        public int PaymentModelId { get; set; }
        //[RegularExpression("^([0-9]?)$",ErrorMessage ="Amount Should Be Greater Than 0")]
        public double Amout { get; set; }
        //[ForeignKey("LanguageModel")]
        //public int? LanguageId { get; set; }

        [EmailAddress]
        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not Valid")]
        public string Email { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]

        public string LastName { get; set; }
        [Required, RegularExpression("^[0-9]+$", ErrorMessage = " Accept Number Only")]
        public string PhoneNumber { get; set; }
        [Required]
        public int GateWayId { get; set; }
        public string OrderNumber { get; set; }
        public string Remarks { get; set; }
        public string Attachment { get; set; }
        public DateTime TransactionDate { get; set; }
        [NotMapped]
        public string estimatedtime { get; set; }

        public string Userid { get; set; }

        public bool Active { get; set; }
        //public virtual ICollection<PaymentGetway> PaymentGetways { get; set; }

        public bool issuccess { get; set; }
        public string payment_type { get; set; }
        public string PaymentID { get; set; }
        public string Result { get; set; }
        public DateTime? PostDate { get; set; }
        public string TranID { get; set; }
        public string Ref { get; set; }
        public string TrackID { get; set; }
        public string Auth { get; set; }
        /// <summary>
        /// CBK
        /// </summary>
        public string ReceiptNo { get; set; }
        public string Message { get; set; }
        public string MerchUdf1 { get; set; }
        public string MerchUdf2 { get; set; }
        public string MerchUdf3 { get; set; }
        public string CCMessage { get; set; }
        public string MerchUdf4 { get; set; }
        public string MerchUdf5 { get; set; }
        public string ErrorCode { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int? PaymentStatusId { get; set; }

        public string resultIndicator { get; set; }
        public string successIndicator { get; set; }

        public virtual Gateway Gateway { get; set; }
        public virtual PaymentStatus PaymentStatus { get; set; }
        //public virtual LanguageModel Language { get; set; }
    }
}
