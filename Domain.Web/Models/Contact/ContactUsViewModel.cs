using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Contact
{
    public class ContactUsViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string EMail { get; set; }

        [Required]
        [Display(Name = "Mesajınız")]
        [StringLength(500)]
        public string Message { get; set; }

        public string FirmAddress { get; set; }
        public string FirmPhoneNumber { get; set; }
        public string FirmEMail { get; set; }
    }
}