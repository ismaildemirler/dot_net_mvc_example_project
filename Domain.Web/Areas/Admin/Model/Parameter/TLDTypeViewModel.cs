using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Areas.Admin.Model.Parameter
{
    public class TLDTypeViewModel
    {
        public string TLDTypeId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        [Display(Name = "Domain Uzantısı")]
        public string Description { get; set; }

        [Display(Name = "İşlemler")]
        public string Buttons { get; set; }
    }
}