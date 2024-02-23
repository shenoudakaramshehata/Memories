using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Memories.Data;
using Memories.Models;

namespace Memories.Pages
{
    public class successModel : PageModel
    {
        private MemoriesContext _context;
        public PaymentModel payment { get; set; }


        public successModel(MemoriesContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> OnGetAsync(string payment_type,string PaymentID,string Result,int OrderID, DateTime? PostDate,string TranID,
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
                    if (payment==null)
                    {
                        return Redirect("SomethingwentError");

                    }
                    payment.issuccess = true;
                    
                    payment.payment_type = payment_type;
                    payment.PaymentID = PaymentID;
                    payment.Result = Result;
                    payment.PaymentModelId = OrderID;
                    payment.PostDate = PostDate;
                    payment.TranID = TranID;
                    payment.Ref = Ref;
                    payment.TrackID = TrackID;
                    payment.Auth = Auth;
                    payment.payment_type = _context.Payments.Include(a => a.Gateway).FirstOrDefault(a => a.PaymentModelId == OrderID).Gateway.Title;
                    var UpdatedOrder = _context.Payments.Attach(payment);
                    UpdatedOrder.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.SaveChanges();
                   
                }
            }
            catch (Exception e)
            {
                return Redirect("SomethingwentError");
            }
            return Page();
        }
            

        }
    }

