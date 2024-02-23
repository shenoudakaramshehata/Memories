using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Models;

namespace Memories.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GatewaysController : Controller
    {
        private MemoriesContext _context;

        public GatewaysController(MemoriesContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var gateways = _context.Gateways.Select(i => new {
                i.GateWayId,
                i.Title,
                i.TestURL,
                i.UserName,
                i.Password,
                i.MerchantId,
                i.ApiKey,
                i.Testmode
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "GateWayId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(gateways, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Gateway();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Gateways.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.GateWayId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Gateways.FirstOrDefaultAsync(item => item.GateWayId == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Gateways.FirstOrDefaultAsync(item => item.GateWayId == key);

            _context.Gateways.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Gateway model, IDictionary values) {
            string GATE_WAY_ID = nameof(Gateway.GateWayId);
            string TITLE = nameof(Gateway.Title);
            string API_URL = nameof(Gateway.TestURL);
            string USER_NAME = nameof(Gateway.UserName);
            string PASSWORD = nameof(Gateway.Password);
            string MERCHANT_ID = nameof(Gateway.MerchantId);
            string API_KEY = nameof(Gateway.ApiKey);
            string TESTMODE = nameof(Gateway.Testmode);

            if(values.Contains(GATE_WAY_ID)) {
                model.GateWayId = Convert.ToInt32(values[GATE_WAY_ID]);
            }

            if(values.Contains(TITLE)) {
                model.Title = Convert.ToString(values[TITLE]);
            }

            if(values.Contains(API_URL)) {
                model.TestURL = Convert.ToString(values[API_URL]);
            }

            if(values.Contains(USER_NAME)) {
                model.UserName = Convert.ToString(values[USER_NAME]);
            }

            if(values.Contains(PASSWORD)) {
                model.Password = Convert.ToString(values[PASSWORD]);
            }

            if(values.Contains(MERCHANT_ID)) {
                model.MerchantId = Convert.ToString(values[MERCHANT_ID]);
            }

            if(values.Contains(API_KEY)) {
                model.ApiKey = Convert.ToString(values[API_KEY]);
            }

            if(values.Contains(TESTMODE)) {
                model.Testmode = values[TESTMODE] != null ? Convert.ToInt32(values[TESTMODE]) : (int?)null;
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}