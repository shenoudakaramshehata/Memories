using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Memories.Areas.Admin.Pages.Payment
{
    public class TransactionListModel : PageModel
    {
        private readonly MemoriesContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public List<PaymentModel> Transactions = new List<PaymentModel>();
        public static List<PaymentModel> Listings2 = new List<PaymentModel>();
        public List<int> Pagenumbers = new List<int>();
        public int pages = 10;
        public static bool ajax = true;
        public static bool first = true;

        public TransactionListModel(MemoriesContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public async Task<IActionResult> OnPostPagesList([FromBody] int num)
        {
            var alllist = context.Payments.Include(a => a.Gateway).Include(a => a.PaymentStatus).OrderByDescending(a => a.TransactionDate).ToList();
            foreach (var item in alllist.Where(e => e.PaymentStatusId == 1))
            {
                //if (item.TransactionDate.AddHours(24) <= DateTime.Now)
                //{
                //    item.PaymentStatusId = 4;
                //    item.Active = false;
                //    context.Attach(item).State = EntityState.Modified;

                //}
                //else
                    item.estimatedtime = item.TransactionDate.AddDays(1).Subtract(DateTime.Now).ToString(@"hh\:mm");
            }
            ajax = false;
            var start = (num - 1) * pages;
            Transactions = alllist.Skip(start).Take(pages).ToList();
            Listings2 = Transactions;
            return new JsonResult(num);
        }
        public List<int> getpagescount(List<PaymentModel> payments)
        {

            float number = (float)payments.Count() / pages;
            var pagenumber = Math.Ceiling(number);
            for (int i = 1; i <= pagenumber; i++)
            {
                Pagenumbers.Add(i);
            }
            return Pagenumbers;
        }
        public async Task<IActionResult> OnGet()
        {
            var alllist = context.Payments.Include(a => a.Gateway).Include(a => a.PaymentStatus).ToList();
            getpagescount(alllist);
            if (first)
            {
                var user = await userManager.GetUserAsync(User);
                Transactions = context.Payments.Include(a => a.Gateway).Where(e => e.Userid == user.Id).OrderByDescending(a => a.TransactionDate)
                                      .Take(pages).ToList();
                foreach (var item in Transactions.Where(e => e.PaymentStatusId == 1))
                {
                    if (item.TransactionDate.AddHours(24) <= DateTime.Now)
                    {

                        item.PaymentStatusId = 4;
                        item.Active = false;

                        context.Attach(item).State = EntityState.Modified;

                    }
                    else
                        item.estimatedtime = item.TransactionDate.AddDays(1).Subtract(DateTime.Now).ToString(@"hh\:mm");
                }

                context.SaveChanges();

                first = false;
            } else
                Transactions= Listings2;

            return Page();
        }
    }
}
