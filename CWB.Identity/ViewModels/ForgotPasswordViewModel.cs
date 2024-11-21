using System.ComponentModel.DataAnnotations;

namespace CWB.Identity.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        public string EmailUserName { get; set; }
    }
}
