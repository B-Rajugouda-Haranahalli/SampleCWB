using System.ComponentModel.DataAnnotations;

namespace CWB.Identity.ViewModels
{
    public class ForgotUsernameViewModel
    {
        [Required]
        [Display(Name = "Phone or Email")]
        public string PhoneEmail { get; set; }
    }
}
