using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.UserAccount
{
    public class ChangePasswordViewModel
    {
        [Required]
        [Display(Name = "Şifreniz")]
        public string OldPassword { get; set; }

        [Required]
        [Display(Name = "Yeni Şifreniz")]
        public string NewPassword { get; set; }

        [Required]
        [Display(Name = "Yeni Şifreniz (Tekrar)")]
        [Compare("NewPassword", ErrorMessage = "Şifreyi kontrol ediniz.")]
        public string NewPasswordConfirm { get; set; }
    }
}