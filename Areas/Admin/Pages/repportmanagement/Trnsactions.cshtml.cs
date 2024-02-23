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
    public class Trnsactions : PageModel
    {
        private readonly MemoriesContext context;

        [BindProperty]
        public rptTransaction Report { get; set; }
        [BindProperty]

        public FilterModel filtermodel { get; set; }
        public Trnsactions(MemoriesContext context)
        {
            this.context = context;
        }
        public IActionResult OnGet()
        {
            List<PaymentModel> ds = context.Payments.Where(e=>e.PaymentStatusId==1).ToList();
            foreach (var item in ds)
            {
                if (item.TransactionDate.AddHours(24) < DateTime.Now)
                {

                    item.PaymentStatusId = 4;
                    item.Active = false;

                    context.Attach(item).State = EntityState.Modified;

                }
                context.SaveChanges();
            }
            //List<PaymentModel> ds = context.Payments.Include(a=>a.Gateway).Where(a => a.issuccess == true).ToList();

            //Report = new rptsuccpayment();
            //Report.DataSource = ds;
            ////Report.Parameters[0].Value = AssetId;
            ////Report.RequestParameters = false;
            return Page();
        }
        public IActionResult OnPost()
        {
            //List<PaymentModel> ds = context.Payments.Include(a=>a.Gateway).Where(a => a.issuccess == true).ToList();
            List<PaymentReport> ds = context.Payments.Include(a => a.Gateway).Include(a => a.PaymentStatus).Select(i => new PaymentReport
            {
                FirstName = i.FirstName,
                LastName = i.LastName,
                Amount = i.Amout,
                Email = i.Email,
                Attachment = i.Attachment,
                OrderNumber = i.OrderNumber,
                payment_type = i.payment_type,
                PhoneNumber = i.PhoneNumber,
                Remarks = i.Remarks,
                TransactionDate = i.TransactionDate,
                issuccess = i.issuccess,
                gatewayTitle = i.Gateway.Title
                ,
                StatusTitle=i.PaymentStatus.Title,
                paymentstatusId = i.PaymentStatusId,

                GatewayId = i.GateWayId,


            }).ToList();
            if (filtermodel.all)
            {
                ds = ds.ToList();
            }
            if (filtermodel.GateID != null)
            {
                ds = ds.Where(i => i.GatewayId == filtermodel.GateID).ToList();
            }
            if (filtermodel.PaymentSatusId != null)
            {
                ds = ds.Where(i => i.paymentstatusId == filtermodel.PaymentSatusId).ToList();
            }
            
                   
                    if (filtermodel.FromDate != null && filtermodel.ToDate != null)
                    {
                        ds = ds.Where(i => i.TransactionDate.Date >= filtermodel.FromDate.Value.Date && i.TransactionDate.Date <= filtermodel.ToDate.Value.Date).ToList();
                    }

   
            if (filtermodel.FromDate == null && filtermodel.ToDate == null  && filtermodel.GateID == null && filtermodel.PaymentSatusId == null && filtermodel.all == false)
            {
                ds = null;
            }

            Report = new rptTransaction();
            Report.DataSource = ds;
            //Report.Parameters[0].Value = AssetId;
            //Report.RequestParameters = false;
            return Page();
        }

    }
}
