using System.ComponentModel.DataAnnotations;

namespace DRF.ViewModels
{
    public class LoginPageViewModel
    {
        public string ReturnUrl { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]

        public string Password { get; set; }
        [Required]
        [RegularExpression(@"\d{6}", ErrorMessage = "Invalid OTP code")]
        public string Code { get; set; }
        [Required]
        public string TwoFAMethod { get; set; }

        public string FormName { get; set; }
        public string TwoFAMethods { get; set; }
    }
}
