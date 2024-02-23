using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Memories.Models
{
    public class ProfileVM
    {
        [Display(Name = "Email")]
        [EmailAddress]
        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not Valid")]

        public string Email { get; set; }
        [Display(Name = "New Email")]
        [EmailAddress]
        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not Valid")]

        public string NewEmail { get; set; }
        [Display(Name = "Confirm New Email")]
        [Compare("NewEmail", ErrorMessage = "The New NewEmail and Confirmation Email do not match")]
        [EmailAddress]
        [Required, RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", ErrorMessage = "Not Valid")]

        public string ConfirmNewEmail { get; set; }

        [Required]
        [Display(Name = "Current Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Should have at least one lower case , one upper case , one number,one special character and minimum length 6 characters")]

        public string CurrentPassword { get; set; }
        [Required]
        [Display(Name = "New Password")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Should have at least one lower case , one upper case , one number,one special character and minimum length 6 characters")]

        public string NewPassword { get; set; }
        [Required]
        [Display(Name = "Confirm New Password")]
        [Compare("NewPassword", ErrorMessage = "The New New Password and Confirmation Password do not match")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{6,}$", ErrorMessage = "Should have at least one lower case , one upper case , one number,one special character and minimum length 6 characters")]

        public string ConfirmNewPassword { get; set; }
        public string photo { get; set; }


    }
}
