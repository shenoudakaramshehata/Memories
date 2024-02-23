using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Memories.Pages
{
    public class ErrorPaymentModel : PageModel
    {
        public void OnGet(string cause, string explanation, string field, string result, string validationType)
        {
        }
    }
}
