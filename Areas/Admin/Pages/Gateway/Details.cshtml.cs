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
    public class DetailsModel : PageModel
    {
        private readonly MemoriesContext context;
        public IToastNotification ToastNotification { get; }
        public Models.Gateway gateway { get; set; }
        public DetailsModel(MemoriesContext _context, IToastNotification toastNotification)
        {
            context = _context;
            ToastNotification = toastNotification;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                gateway=context.Gateways.Find(id);
            }
            catch (Exception)
            {
                ToastNotification.AddErrorToastMessage("Something Went Error..");
                return RedirectToPage("list");
            }
            return Page();
        }
    }
}
