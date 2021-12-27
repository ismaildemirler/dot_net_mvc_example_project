using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandForWatching
{
    public class BrandForWatchingViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid BrandForWatchingId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Marka Adı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Firma Adı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string FirmName { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İzlenecek Sınıflar")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string ClassesToWatch { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Başvuru/Tescil No")]
        [StringLength(100, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string RegistryNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid BrandWatchingApplicationDetailId { get; set; }
    }
}