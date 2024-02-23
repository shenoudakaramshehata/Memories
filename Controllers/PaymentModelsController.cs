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
    public class PaymentModelsController : Controller
    {
        private MemoriesContext _context;

        public PaymentModelsController(MemoriesContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var payments = _context.Payments.Where(a=>a.Active==true).Select(i => new {
                i.PaymentModelId,
                i.Amout,
                //i.LanguageId,
                i.Email,
                i.FirstName,
                i.LastName,
                i.PhoneNumber,
                i.OrderNumber,
                i.Remarks,
                i.Attachment,
                i.Auth,
                i.payment_type,
                i.PostDate,
                i.Ref,
                i.Userid,
                i.GateWayId,
                i.Gateway,
                i.TransactionDate,
                i.issuccess
            }).OrderBy(a=>a.TransactionDate);

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "PaymentModelId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(payments, loadOptions));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetDeleted(DataSourceLoadOptions loadOptions) {
            var payments = _context.Payments.Where(a=>a.Active==false).Select(i => new {
                i.PaymentModelId,
                i.Amout,
                //i.LanguageId,
                i.Email,
                i.FirstName,
                i.LastName,
                i.PhoneNumber,
                i.OrderNumber,
                i.Remarks,
                i.Attachment,
                i.Auth,
                i.payment_type,
                i.PostDate,
                i.Ref,
                i.Userid,
                i.GateWayId
            });

            // If underlying data is a large SQL table, specify PrimaryKey and PaginateViaPrimaryKey.
            // This can make SQL execution plans more efficient.
            // For more detailed information, please refer to this discussion: https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "PaymentModelId" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(payments, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new PaymentModel();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Payments.Add(model);
            await _context.SaveChangesAsync();

            return Json(new { result.Entity.PaymentModelId });
        }

        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Payments.FirstOrDefaultAsync(item => item.PaymentModelId == key);
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
            var model = await _context.Payments.FirstOrDefaultAsync(item => item.PaymentModelId == key);

            _context.Payments.Remove(model);
            await _context.SaveChangesAsync();
        }


        [HttpGet]
        public async Task<IActionResult> LanguagesLookup(DataSourceLoadOptions loadOptions)
        {
            var lookup = from i in _context.Languages
                         orderby i.Title
                         select new
                         {
                             Value = i.LanguageModelId,
                             Text = i.Title
                         };
            return Json(await DataSourceLoader.LoadAsync(lookup, loadOptions));
        }

        private void PopulateModel(PaymentModel model, IDictionary values) {
            string PAYMENT_MODEL_ID = nameof(PaymentModel.PaymentModelId);
            string AMOUT = nameof(PaymentModel.Amout);
            //string LANGUAGE_ID = nameof(PaymentModel.LanguageId);
            string EMAIL = nameof(PaymentModel.Email);
            string FIRST_NAME = nameof(PaymentModel.FirstName);
            string LAST_NAME = nameof(PaymentModel.LastName);
            string PHONE_NUMBER = nameof(PaymentModel.PhoneNumber);
            string ORDER_NUMBER = nameof(PaymentModel.OrderNumber);
            string REMARKS = nameof(PaymentModel.Remarks);
            string ATTACHMENT = nameof(PaymentModel.Attachment);

            if(values.Contains(PAYMENT_MODEL_ID)) {
                model.PaymentModelId = Convert.ToInt32(values[PAYMENT_MODEL_ID]);
            }

            if(values.Contains(AMOUT)) {
                model.Amout = Convert.ToInt32(values[AMOUT]);
            }

            //if (values.Contains(LANGUAGE_ID))
            //{
            //    model.LanguageId = Convert.ToInt32(values[LANGUAGE_ID]);
            //}

            if (values.Contains(EMAIL)) {
                model.Email = Convert.ToString(values[EMAIL]);
            }

            if(values.Contains(FIRST_NAME)) {
                model.FirstName = Convert.ToString(values[FIRST_NAME]);
            }

            if(values.Contains(LAST_NAME)) {
                model.LastName = Convert.ToString(values[LAST_NAME]);
            }

            if(values.Contains(PHONE_NUMBER)) {
                model.PhoneNumber = Convert.ToString(values[PHONE_NUMBER]);
            }

            if(values.Contains(ORDER_NUMBER)) {
                model.OrderNumber = Convert.ToString(values[ORDER_NUMBER]);
            }

            if(values.Contains(REMARKS)) {
                model.Remarks = Convert.ToString(values[REMARKS]);
            }

            if(values.Contains(ATTACHMENT)) {
                model.Attachment = Convert.ToString(values[ATTACHMENT]);
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