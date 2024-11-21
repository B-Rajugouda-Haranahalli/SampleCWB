using System.ComponentModel.DataAnnotations;

namespace CWB.Identity.ViewModels
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string NewPassword { get; set; }
        [Required]
        [Compare(nameof(NewPassword), ErrorMessage = "Passwords don't match.")]
        public string ConfirmPassword { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public string Email { get; set; }
    }
}
