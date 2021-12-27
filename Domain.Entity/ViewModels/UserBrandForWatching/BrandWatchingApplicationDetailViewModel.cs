using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandForWatching
{
    public class BrandWatchingApplicationDetailViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid BrandWatchingApplicationDetailId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "T.C Kimlik Numarası")]
        [StringLength(11, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Firma Ünvanı veya Şahıs Adı-Soyadı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string FirmName { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Adres")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Address { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İl")]
        public int CityId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İlçe")]
        public int TownId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Telefon Numarası")]
        [StringLength(15, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Phone { get; set; }

        [Display(Name = "Fax Numarası")]
        [StringLength(15, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid CustomerApplicationId { get; set; }
    }
}