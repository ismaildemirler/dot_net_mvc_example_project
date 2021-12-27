using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.UserAccount
{
    public class UserAccountInfoViewModel
    {
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
        [Display(Name = "Telefon")]
        public string PhoneNumber { get; set; }
    }
}