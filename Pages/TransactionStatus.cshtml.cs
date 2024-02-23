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
    public class TransactionStatusModel : PageModel
    {
        private MemoriesContext _context;
        public PaymentModel payment { get; set; }
        public bool issuccess { get; set; }
        public TransactionStatusModel(MemoriesContext context)
        {
            _context = context;

        }
        public IActionResult OnGet(int transactionId)
        {
            try
            {
                payment = _context.Payments.FirstOrDefault(e => e.PaymentModelId == transactionId);
                return Page();
            }
            catch(Exception e)
            {
                return RedirectToPage("SomethingwentError");
            }



        }
    }
}
