using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using NToastNotify;
using Memories.Data;
using Memories.Models;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Memories.Areas.Admin.Pages.Payment
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private MemoriesContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public PaymentModel model { get; set; }
        public HttpClient httpClient { get; set; }
        public CreateModel(MemoriesContext context, IWebHostEnvironment hostEnvironment, IToastNotification toastNotification,UserManager<ApplicationUser> userManager )
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            _toastNotification = toastNotification;
            UserManager = userManager;
            httpClient = new HttpClient();
            model = new PaymentModel();
        }
        [BindProperty(SupportsGet = true)]
        public bool ArLang { get; set; }
        
        public UserManager<ApplicationUser> UserManager { get; }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await UserManager.GetUserAsync(User);
            try
            {
                if (file != null)
                {
                    string folder = "Attachment/";
                    model.Attachment = await UploadImage(folder, file);
                }
                var payments = _context.Payments.Include(a => a.PaymentStatus).Where(a => a.PaymentStatusId == 2).Select(a => a.OrderNumber).ToList();
                if (payments.Contains(model.OrderNumber) == true)
                {
                    _toastNotification.AddWarningToastMessage("order number exists...");
                    return Page();
                }
                model.Userid = user.Id;
                model.Active = true;
                //model.issuccess = false;
                model.PaymentStatusId = 1;
                model.TransactionDate = DateTime.Now;
                // model.estimatedtime = DateTime.Now.AddDays(1);
                _context.Payments.Add(model);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                _toastNotification.AddErrorToastMessage("Something went wrong");
                return Page();
            }
            return RedirectToPage("ConfirmDetails", new { paymentid = model.PaymentModelId });
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_hostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }

    }
}
