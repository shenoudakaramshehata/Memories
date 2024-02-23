using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class TransactionsModel : PageModel
    {
        private readonly MemoriesContext context;
        private readonly UserManager<ApplicationUser> userManager;
        public List<PaymentModel> Transactions = new List<PaymentModel>();
        public static List<PaymentModel> Listings2 = new List<PaymentModel>();
        public static List<PaymentModel> Listings3 = new List<PaymentModel>();
        public List<int> Pagenumbers = new List<int>();
        public int pages = 10;
        public static bool ajax = true;
        public static bool first = true;
        public FilterModel FilterModel { get; set; }
        public TransactionsModel(MemoriesContext context, UserManager<ApplicationUser> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            FilterModel = new FilterModel();
        }
        public async Task<IActionResult> OnPostPagesList([FromBody] int num)
        {

            var alllist = Listings3;
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
            Transactions = Listings3.Skip(start).Take(pages).ToList();
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
            if (first)
            {

                var allTransactions = context.Payments.ToList();
                getpagescount(allTransactions);
                Transactions = context.Payments.Include(a => a.Gateway).OrderByDescending(a => a.TransactionDate)
                                      .Take(pages).ToList();
                Listings3 = allTransactions;
                first = false;
                foreach (var item in Transactions.Where(e => e.PaymentStatusId == 1))
                {
                    if (item.TransactionDate.AddHours(24) <= DateTime.Now)
                    {

                        item.PaymentStatusId = 4;
                        item.Active = false;

                        context.Attach(item).State = EntityState.Modified;

                    }
                    else
                    {
                        item.estimatedtime = item.TransactionDate.AddDays(1).Subtract(DateTime.Now).ToString(@"hh\:mm");
                    }
                }

                context.SaveChanges();
            }
            else
            {
                Transactions = Listings2;
                getpagescount(Listings3);
            }

            return Page();
        }
        public IActionResult OnPost(FilterModel filterModel)
        {

            if (ajax == false)
            {
                Transactions = Listings2;
                Pagenumbers = getpagescount(Listings3);
                ajax = true;
                return Page();
            }
            var alllist = context.Payments.Include(a => a.Gateway).Include(a => a.PaymentStatus).OrderByDescending(a => a.TransactionDate).ToList();


            if (filterModel.PaymentSatusId != null)
            {
                if (filterModel.PaymentSatusId == 0)
                {
                    Listings3 = alllist.OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.PaymentSatusId == 1)
                {
                    Listings3 = alllist.Where(a => a.PaymentStatusId == 1).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.PaymentSatusId == 2)
                {
                    Listings3 = alllist.Where(a => a.PaymentStatusId == 2).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.PaymentSatusId == 3)
                {
                    Listings3 = alllist.Where(a => a.PaymentStatusId == 3).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.PaymentSatusId == 4)
                {
                    Listings3 = alllist.Where(a => a.PaymentStatusId == 4).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
            }
            if (filterModel.GateID != 0)
            {
                if (filterModel.GateID == 1)
                {
                    Listings3 = Listings3.Where(a => a.GateWayId == 1).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.GateID == 2)
                {
                    Listings3 = Listings3.Where(a => a.GateWayId == 2).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
                if (filterModel.GateID == 3)
                {
                    Listings3 = Listings3.Where(a => a.GateWayId == 3).OrderByDescending(a => a.TransactionDate).ToList();
                    Transactions = Listings2 = Listings3.Take(pages).ToList();
                }
            }
            var date = filterModel.Date.Split('-');
            var start = date[0];
            var End = date[1];
            if (date != null)
            {
                Listings3 = Listings3.Where(a => a.TransactionDate.Date >= DateTime.Parse(start, CultureInfo.InvariantCulture).Date && a.TransactionDate.Date <= DateTime.Parse(End, CultureInfo.InvariantCulture).Date).OrderByDescending(a => a.TransactionDate).ToList();
                Transactions = Listings2 = Listings3.Take(pages).ToList();

            }
            foreach (var item in Listings3.Where(a => a.PaymentStatusId == 1))
            {

                item.estimatedtime = item.TransactionDate.AddDays(1).Subtract(DateTime.Now).ToString(@"hh\:mm");

            }
            Pagenumbers = getpagescount(Listings3);

            return Page();
        }
    }
}
