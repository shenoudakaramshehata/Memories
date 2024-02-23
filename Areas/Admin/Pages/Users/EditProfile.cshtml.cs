using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Memories.Data;
using Memories.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using NToastNotify;

namespace Memories.Areas.Admin.Pages.Users
{
    public class EditProfileModel : PageModel
    {
        public UserManager<ApplicationUser> usermanager;
        private readonly IToastNotification toastNotification;
        private readonly IWebHostEnvironment hostEnvironment;

        public SignInManager<ApplicationUser> SignInManager { get; }
        public ApplicationDbContext ApplicationDb { get; }

        public EditProfileModel(UserManager<ApplicationUser> usermanager, SignInManager<ApplicationUser> signInManager,IToastNotification _toastNotification,ApplicationDbContext applicationDb, IWebHostEnvironment _hostEnvironment)
        {
            this.usermanager = usermanager;
            SignInManager = signInManager;
            toastNotification = _toastNotification;
            ApplicationDb = applicationDb;
            hostEnvironment = _hostEnvironment;
        }
        [BindProperty]
        public ProfileVM ProfileVM { get; set; }


        public async Task<IActionResult> OnPostChangepassword()
        {
            var user = await usermanager.GetUserAsync(User);
            var result = await usermanager.ChangePasswordAsync(user, ProfileVM.CurrentPassword, ProfileVM.NewPassword);
            if (!result.Succeeded)
            {
                toastNotification.AddErrorToastMessage("Something Went Error..");
                return Page();
            }
            user.LastChangePassword = DateTime.Now.Date;
            ApplicationDb.Attach(user).State = EntityState.Modified;
            ApplicationDb.SaveChanges();
            await SignInManager.RefreshSignInAsync(user);
            toastNotification.AddSuccessToastMessage("Password Updated Successfully");
            return Page();

        }
        public async Task<IActionResult> OnPostChangeEmail()
        {
            var user = await usermanager.GetUserAsync(User);
            user.UserName = ProfileVM.NewEmail;
            user.Email = ProfileVM.NewEmail;
            await usermanager.UpdateAsync(user);
            ApplicationDb.Attach(user).State = EntityState.Modified;
            ApplicationDb.SaveChanges();
            await SignInManager.RefreshSignInAsync(user);
            toastNotification.AddSuccessToastMessage("Email Updated Successfully");
            return Page();
        }
        public async Task<IActionResult> OnPost(IFormFile file)
        {
            var user = await usermanager.GetUserAsync(User);
            if (file != null)
            {
                if (ProfileVM.photo != null)
                {
                    var ImagePath = Path.Combine(hostEnvironment.WebRootPath, user.Photo);
                    if (System.IO.File.Exists(ImagePath))
                    {
                        System.IO.File.Delete(ImagePath);
                    }
                }
                string folder = "Images/Profile/";
                user.Photo = await UploadImage(folder, file);
            }
            await usermanager.UpdateAsync(user);
            ApplicationDb.Attach(user).State = EntityState.Modified;
            ApplicationDb.SaveChanges();
            return Page();
        }
        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(hostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

            return folderPath;
        }
    }
}
