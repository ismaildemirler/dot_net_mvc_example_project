using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Account
{
    public class RegisterViewModel
    {
        [MaxLength(250)]
        [EmailAddress]
        [Required]
        [Display(Name = "E-Posta")]
        public string Email { get; set; }
        
        [MaxLength(100)]
        [Required]
        [Display(Name = "Adınız")]
        public string FirstName { get; set; }
        
        [MaxLength(100)]
        [Required]
        [Display(Name = "Soyadınız")]
        public string LastName { get; set; }
        
        [MaxLength(50)]
        [Required]
        [Display(Name = "Şifre")]
        public string Password { get; set; }

        [MaxLength(50)]

        [Compare("Password", ErrorMessage = "Şifreyi kontrol ediniz.")]
        [Required]
        [Display(Name = "Şifre (Tekrar)")]
        public string PasswordConfirm { get; set; }

        [MaxLength(50)]
        [Required]
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
    }
}