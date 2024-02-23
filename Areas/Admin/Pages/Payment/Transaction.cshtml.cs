using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Memories.Data;
using Memories.Models;

namespace Memories.Areas.Admin.Pages.Payment
{
    public class TransactionModel : PageModel
    {
        private readonly MemoriesContext context;
        public double totalgen { get; set; }
        public double totalRecived { get; set; }
        public double Net { get; set; }
        public List<PaymentModel> payment { get; set; }
        [BindProperty]
        public TransactionSearchVm AssetSerachVM { get; set; }
        public IToastNotification ToastNotification { get; }
        static bool isEntered = false;

        public TransactionModel(MemoriesContext _context, IToastNotification toastNotification)
        {
            context = _context;
            ToastNotification = toastNotification;
            AssetSerachVM = new TransactionSearchVm();
        }
        public IActionResult OnGetGridData(DataSourceLoadOptions loadOptions)
        {
            if (!isEntered)
            {
                payment = new List<PaymentModel>();

            }
            isEntered = false;
            return new JsonResult(DataSourceLoader.Load(payment.Distinct(), loadOptions));
        }

        public void OnGet()
        {
            totalgen = context.Payments.Where(a=>a.Active==true).Select(e => e.Amout).ToList().Sum();
            totalRecived= context.Payments.Where(a=>a.issuccess==true&&a.Active==true).Select(e => e).ToList().Sum(a=>a.Amout);
            Net= totalgen- context.Payments.Where(a => a.issuccess == false && a.Active == true).Select(e => e).ToList().Sum(a => a.Amout);
            payment = context.Payments.Where(a=>a.Active==true).ToList();
        }
        public IActionResult OnPost(int id)
        {
            var payment = context.Payments.Find(id);
            payment.Active = false;
            context.Attach(payment).State = EntityState.Modified;
            context.SaveChanges();
            ToastNotification.AddSuccessToastMessage("Payment Deleted successfully...");
            return Page();
        }

        //public async Task<IActionResult> OnPostSearch()
        //{
        //    bool CheckSearchItem = false;
        //    int SearchItem = 0;
        //    CheckSearchItem = int.TryParse(AssetSerachVM.TransactionSearchItem, out SearchItem);

        //    if (ModelState.IsValid)
        //    {
        //        payment=context.Payments.Where(x =>x.Email == AssetSerachVM.TransactionSearchItem || x.FirstName == AssetSerachVM.TransactionSearchItem || x.PhoneNumber == AssetSerachVM.TransactionSearchItem || x.Amout == SearchItem || x.OrderNumber == SearchItem || x.LastName == AssetSerachVM.TransactionSearchItem).ToList();
        //        if (payment.Count == 0)
                   
        //        {
        //            ToastNotification.AddErrorToastMessage("This Transaction Not Found");
        //            return Page();
        //        }
        //        return RedirectToPage("/Payment/Transaction", new { TransactionId = payment[0].PaymentID });
        //    }
        //    return Page();
        //}

    }
}
