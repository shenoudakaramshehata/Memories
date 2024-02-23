using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class PaymentGetway
    {
        public int PaymentGetwayId { get; set; }
        public int PaymentModelId { get; set; }
        public int GetWayId { get; set; }
        //public virtual Gektway Getway { get; set; }
        public virtual PaymentModel Payment { get; set; }
    }
}
