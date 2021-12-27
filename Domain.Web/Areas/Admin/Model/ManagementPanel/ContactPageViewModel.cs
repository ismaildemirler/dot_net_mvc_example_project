using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.Admin.Model.ManagementPanel
{
    public class ContactPageViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Adres")]
        public string FirmAddress { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Telefon")]
        public string FirmPhoneNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [EmailAddress]
        [Display(Name = "E-Posta")]
        public string FirmEMail { get; set; }
    }
}