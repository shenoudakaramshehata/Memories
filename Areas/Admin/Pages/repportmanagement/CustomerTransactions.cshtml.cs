using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Memories.Data;
using Memories.Models;

namespace Memories.Areas.Admin.Pages.repportmanagement
{
    public class CustomerTransactionsModel : PageModel
    {
        private readonly MemoriesContext context;
        public UserManager<ApplicationUser> UserManager { get; }

        public List<PaymentModel> Transactions { get; set; }
        public CustomerTransactionsModel(MemoriesContext context, UserManager<ApplicationUser> userManager)
        {
            UserManager = userManager;

            this.context = context;
        }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await UserManager.GetUserAsync(User);
            Transactions = context.Payments.Where(e=>e.Userid== user.Id)
                                  .ToList();
            return Page();
        }
    }
}
