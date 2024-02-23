using Memories.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Memories.Models;
using NToastNotify;
using Memories.Data;

namespace Memories.Areas.Identity.Pages.Account
{
    [Authorize]
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToastNotification _toastNotification;
        [BindProperty]
        public ChangePasswordVM changePasswordVM { get; set; }
        public ChangePasswordModel(UserManager<ApplicationUser> userManager,IToastNotification toastNotification, SignInManager<ApplicationUser> signInManager )
        {
            _userManger = userManager;
            _toastNotification = toastNotification;
            _signInManager = signInManager;
        }
        public void OnGet()
        {
            
        }
        //public async Task<IActionResult> OnPost()
        //{
        //    try
        //    {
        //        if (!ModelState.IsValid)
        //            return Page();
        //        if (resetPasswordModel.ConfirmPassword != resetPasswordModel.NewPassword)
        //        {
        //            _toastNotification.AddErrorToastMessage("Password not matched");
        //        }
        //        var userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        var user = await _userManager.FindByIdAsync(userid);
        //        var Result = await _userManager.ChangePasswordAsync(user, resetPasswordModel.CurrentPassword, resetPasswordModel.NewPassword);
        //        if (!Result.Succeeded)
        //        {
        //            foreach (var error in Result.Errors)
        //            {
        //                ModelState.TryAddModelError("", error.Description);
        //            }
        //            return Page();

        //        }
        //        _toastNotification.AddSuccessToastMessage("Password Updated Successfully");


        //    }
        //    catch (Exception)
        //    {
        //        _toastNotification.AddErrorToastMessage("Something went Error");
        //    }
        //    return RedirectToPage("../Login");
        //}

        public async Task<IActionResult> OnPost()
        {
            if (!ModelState.IsValid)
                return Page();
            var user = await _userManger.GetUserAsync(User);
            var result = await _userManger.ChangePasswordAsync(user, changePasswordVM.CurrentPassword, changePasswordVM.NewPassword);
            if (!result.Succeeded)
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return Page();
            }
            await _signInManager.RefreshSignInAsync(user);
            _toastNotification.AddSuccessToastMessage("Password Updated Successfully");
             return RedirectToPage("../../Index");

        }
    }
}
