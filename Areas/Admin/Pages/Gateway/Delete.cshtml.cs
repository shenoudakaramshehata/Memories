using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using NToastNotify;
using Memories.Data;

namespace Memories.Areas.Admin.Pages.Gateway
{
    public class DeleteModel : PageModel
    {
        private readonly MemoriesContext context;
        public IToastNotification ToastNotification { get; }
        public Models.Gateway gateway { get; set; }
        public DeleteModel(MemoriesContext _context, IToastNotification toastNotification)
        {
            context = _context;
            ToastNotification = toastNotification;
        }
        public IActionResult OnGet(int id)
        {
            try
            {
                gateway = context.Gateways.Find(id);
                if (gateway == null)
                {
                    return Redirect("../NotFound");
                }
            }
            catch (Exception)
            {

                ToastNotification.AddErrorToastMessage("Something went wrong");
            }
            return Page();
        }
        public IActionResult OnPost(int id)
        {
            try
            {

                gateway = context.Gateways.Find(id);
                if (gateway != null)
                {
                    context.Gateways.Remove(gateway);
                    context.SaveChanges();
                    ToastNotification.AddSuccessToastMessage("gateway deleted successfully..");
                }

            }
            catch (Exception)
            {
                ToastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }
            return RedirectToPage("list");
        }
    }
}


