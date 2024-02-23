using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Memories.Data;
using Memories.Models;

namespace Memories.Pages
{
    public class failedModel : PageModel
    {
        private MemoriesContext _context;
        public PaymentModel payment { get; set; }

       
        public failedModel(MemoriesContext context)
        {
            _context = context;
        }
        public IActionResult OnGet(string payment_type, string PaymentID, string Result, int OrderID, DateTime? PostDate, string TranID,
        string Ref, string TrackID, string Auth)
        {
            try
            {
                if (OrderID == 0)
                {
                    return Redirect("NotFound");
                }
                if (OrderID != 0)
                {
                    payment = _context.Payments.FirstOrDefault(e => e.PaymentModelId == OrderID);
                    if (payment == null)
                    {
                        return Redirect("SomethingwentError");

                    }
                    payment.issuccess = false;
                    var UpdatedOrder = _context.Payments.Attach(payment);
                    payment.PostDate = DateTime.Now;
                    payment.payment_type = _context.Payments.Include(a => a.Gateway).FirstOrDefault(a => a.PaymentModelId == OrderID).Gateway.Title;
                    UpdatedOrder.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();

                }
            }
            catch (Exception)
            {
                return Redirect("SomethingwentError");
    }
            return Page();
        }
       

    }
}
