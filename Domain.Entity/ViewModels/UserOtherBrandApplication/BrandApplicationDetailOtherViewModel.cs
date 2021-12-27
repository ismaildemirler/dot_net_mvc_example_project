using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandApplication
{
    public class BrandApplicationDetailOtherViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid BrandApplicationDetailId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Marka Adı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string BrandName { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Başvuru/Tescil No")]
        [StringLength(50, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string RegistryNumber { get; set; }

        [Display(Name = "Marka Kategori Açıklaması")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string BrandCategoryDescription { get; set; }

        [Required(ErrorMessage ="Lütfen Firma ve Şahıs Seçeneklerinden Birini Seçiniz")]
        [Display(Name = "Firma Başvuru Türü")]
        public byte? BrandApplicationTypeId { get; set; }

        [Display(Name = "Varsa Özel Emtia")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string SpecialClass { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Ön Yazı Gönderilsin Mi?")]
        public bool SendCoverLetter { get; set; }

        public Guid? ContactId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid CustomerApplicationId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Marka Başvuru Tarihi")]
        public DateTime? BrandApplicationDate { get; set; }
    }
}