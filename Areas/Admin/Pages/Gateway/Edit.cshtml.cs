using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using Memories.Data;

namespace Memories.Areas.Admin.Pages.Gateway
{
    public class EditModel : PageModel
    {
        private readonly MemoriesContext context;
        public IToastNotification ToastNotification { get; }
        [BindProperty]
        public Models.Gateway gateway { get; set; }
        public EditModel(MemoriesContext _context, IToastNotification toastNotification)
        {
            context = _context;
            ToastNotification = toastNotification;
        }

        public void OnGet(int id)
        {
            try
            {
                gateway = context.Gateways.Find(id);
            }
            catch (Exception)
            {

                ToastNotification.AddErrorToastMessage("sothing went error...");
            }
            
        }
        public IActionResult OnPost(int id) 
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Not Valid");
                return Page();
            }
            try
            {
               var model= context.Gateways.Find(id);
                model.ApiKey = gateway.ApiKey;
                model.TestURL = gateway.TestURL;
                model.UserName = gateway.UserName;
                model.Password = gateway.Password;
                model.Title = gateway.Title;
                model.MerchantId = gateway.MerchantId;
                model.Testmode = gateway.Testmode;


                context.Attach(model).State = EntityState.Modified;
                 context.SaveChangesAsync();
                ToastNotification.AddSuccessToastMessage("Gateway edieted successfully...");
            }
            catch (Exception)
            {

                ToastNotification.AddErrorToastMessage("sothing went error...");
            }
            return RedirectToPage("list");
        }

    }
}
