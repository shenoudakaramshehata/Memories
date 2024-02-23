using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Memories.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StatisticsController : Controller
    {
        private readonly MemoriesContext context;

        public StatisticsController(MemoriesContext context)
        {
            this.context = context;
        }
        [HttpGet]
        public object GetWeeklyTransactionCount(DataSourceLoadOptions loadOptions)
        {
            var WeeklyAppointmentCount = context.Payments
                .Where(o =>o.TransactionDate.Date <= DateTime.Now.Date && o.TransactionDate.Date >= DateTime.Now.Date.AddDays(-6))
                .GroupBy(c => c.TransactionDate.Date).

            Select(g => new
            {

                OnDay = g.Key,

                Count = g.Count()

            }).OrderByDescending(r => r.Count);

            return WeeklyAppointmentCount;
        }
        [HttpGet]
        public object GetSuccessTransactionCount(DataSourceLoadOptions loadOptions)
        {
            var WeeklyAppointmentCount = context.Payments
                .Where(o =>o.PaymentStatusId==2&& o.TransactionDate.Date <= DateTime.Now.Date && o.TransactionDate.Date >= DateTime.Now.Date.AddDays(-6))
                .GroupBy(c => c.TransactionDate.Date).

            Select(g => new
            {

                OnDay = g.Key,

                Count = g.Count()

            }).OrderByDescending(r => r.Count);

            return WeeklyAppointmentCount;
        }
        [HttpGet]
        public object GetFailedTransactionCount(DataSourceLoadOptions loadOptions)
        {
            var WeeklyAppointmentCount = context.Payments
                .Where(o =>o.PaymentStatusId==3&& o.TransactionDate.Date <= DateTime.Now.Date && o.TransactionDate.Date >= DateTime.Now.Date.AddDays(-6))
                .GroupBy(c => c.TransactionDate.Date).

            Select(g => new
            {

                OnDay = g.Key,

                Count = g.Count()

            }).OrderByDescending(r => r.Count);

            return WeeklyAppointmentCount;
        }
        [HttpGet]
        public object GetMonthlyTransactionCount(DataSourceLoadOptions loadOptions)
        {
            var abs = System.Math.Abs(DateTime.Now.Date.AddMonths(-10).Month);
            var abs2 = System.Math.Abs(DateTime.Now.Date.AddMonths(-10).Year);

            var WeeklyAppointmentCount = context.Payments
                .Where(o => o.TransactionDate.Date<= DateTime.Now.Date && o.TransactionDate.Date >= DateTime.Now.Date.AddMonths(-10))
                .GroupBy(c => c.TransactionDate.Date.Month).

            Select(g => new
            {

                OnMonth = g.Key,

                Count = g.Count()

            }).OrderByDescending(r => r.Count);

            return WeeklyAppointmentCount;
        }
        [HttpGet]
        public object GetGatewayssuccesspayment(DataSourceLoadOptions loadOptions)
        {
            var list = context.Payments.Where(e=>e.PaymentStatusId==2).GroupBy(e => e.GateWayId).Select(e => new
            {
                name=context.Gateways.FirstOrDefault(a=>a.GateWayId==e.Key).Title,
                count=/*context.Payments.Where(i=>i.issuccess&&i.GateWayId==e.Key)*/e.Count()
                
            }).OrderByDescending(r => r.count);
            return list;
            
        }

        [HttpGet]
        public object GetGatewaysfailedpayment(DataSourceLoadOptions loadOptions)
        {
            var list = context.Payments.Where(e => e.PaymentStatusId==4).GroupBy(e => e.GateWayId).Select(e => new
            {
                name = context.Gateways.FirstOrDefault(a => a.GateWayId == e.Key).Title,
                count = e.Count()

            }).OrderByDescending(r => r.count);
            return list;

        }


    }
}
