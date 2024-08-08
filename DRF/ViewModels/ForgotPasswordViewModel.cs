using System.ComponentModel.DataAnnotations;

namespace DRF.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        public bool IsSent { get; set; }
    }
}
