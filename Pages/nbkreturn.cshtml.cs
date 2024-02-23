using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memories.Pages
{
    public class nbkreturnModel : PageModel
    {
        private MemoriesContext _context;
        public PaymentModel payment { get; set; }
        public bool issuccess { get; set; }
        public nbkreturnModel(MemoriesContext context)
        {
            _context = context;

        }
        public IActionResult OnGet(string orderId, string resultIndicator)
        {
            payment = _context.Payments.FirstOrDefault(e => e.OrderNumber == orderId);

            if (payment.successIndicator == resultIndicator)
            {
                payment.resultIndicator = resultIndicator;
                payment.PaymentStatusId = 2;
                issuccess = true;
                var UpdatedOrder = _context.Payments.Attach(payment);
                UpdatedOrder.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();

            }
            else
            {
                payment.resultIndicator = resultIndicator;
                payment.successIndicator = payment.successIndicator;
                payment.PaymentStatusId = 3;
                var UpdatedOrder = _context.Payments.Attach(payment);
                UpdatedOrder.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
            }

            return Page();
        }

    }
}
