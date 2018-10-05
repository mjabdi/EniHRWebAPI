using System;
using System.ComponentModel.DataAnnotations;

namespace EniHRWebAPI.ViewModel
{
    public class ChangePasswordViewModel
    {

        public string Username { get; set; }
        public string CurrentPassword { get; set; }

        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [StringLength(150, MinimumLength = 6)]
        [Display(Name = "Confirm New Password")]
        public string NewPasswordAgain { get; set;}
    }
}
