using System.ComponentModel.DataAnnotations;

namespace DRF.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]

        public string EmailAddress { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string CurrentPassword { get; set; }


        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public bool IsReset { get; set; }
    }
}
