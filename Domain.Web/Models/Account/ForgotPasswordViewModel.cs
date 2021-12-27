using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string EMail { get; set; }
    }
}