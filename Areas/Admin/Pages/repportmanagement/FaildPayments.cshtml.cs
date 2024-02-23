using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Memories.Data;
using Memories.Models;
using Memories.Reports;

namespace Memories.Areas.Admin.Pages.repportmanagement
{
    public class FaildPaymentsModel : PageModel
    {
        private readonly MemoriesContext context;

        [BindProperty]
        public rptsuccpayment Report { get; set; }
        public FaildPaymentsModel(MemoriesContext context)
        {
            this.context = context;
        }
        public IActionResult OnGet()
        {
            List<PaymentModel> ds = context.Payments.Include(c=>c.Gateway).Where(a => a.issuccess == false).ToList();

            Report = new rptsuccpayment();
            Report.DataSource = ds;
            //Report.Parameters[0].Value = AssetId;
            //Report.RequestParameters = false;
            return Page();
        }
    }
}
