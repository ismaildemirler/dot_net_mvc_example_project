using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [EmailAddress]
        public string EMail { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public string Password { get; set; }

        [Display(Name = "Şifremi unuttum!")]
        public bool ForgotPassword { get; set; }
    }
}