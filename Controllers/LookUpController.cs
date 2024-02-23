using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Memories.Data;
using DevExtreme.AspNet.Mvc;
using DevExtreme.AspNet.Data;

namespace Memories.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class LookUpController : Controller
    {
        private MemoriesContext _context;
        UserManager<ApplicationUser> UserManger;


        public LookUpController(MemoriesContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            UserManger = userManager;
        }
        [HttpGet]
        public async Task<IActionResult> LanguagesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Languages
                         select new
                         {
                             Value = i.LanguageModelId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> PaymentStatusLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.PaymentStatus
                         select new
                         {
                             Value = i.PaymentStatusId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
        [HttpGet]
        public async Task<IActionResult> GateWaysLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Gateways
                         select new
                         {
                             Value = i.GateWayId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }
    }
}
