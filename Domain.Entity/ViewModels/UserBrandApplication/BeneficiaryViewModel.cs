using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.ViewModels.UserBrandApplication
{
    public class BeneficiaryViewModel
    {
        [Required(ErrorMessage = "{0} zorunlu alandır")]
        public Guid BeneficiaryId { get; set; }

        public Guid? BrandApplicationDetailId { get; set; }

        public Guid? PatentApplicationDetailId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Şirket Unvanı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string FirmName { get; set; }

        [Display(Name = "Eski Firma Unvanı")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PreviousFirmName { get; set; }

        [Display(Name = "Şahıs Adı")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PersonName { get; set; }

        [Display(Name = "T.C. Kimlik Numarası")]
        [StringLength(11, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string TCNumber { get; set; }

        [Display(Name = "Doğum Yeri")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string BirthPlace { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Vergi Dairesi")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string TaxOffice { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Vergi Numarası")]
        [StringLength(20, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string TaxNumber { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Adres")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Address { get; set; }

        [Display(Name = "Eski Resmi Adres")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PreviousOfficialAddress { get; set; }

        [Display(Name = "Email Adresi")]
        [StringLength(150, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İl")]
        public int? CityId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "İlçe")]
        public int? TownId { get; set; }

        [Required(ErrorMessage = "{0} zorunlu alandır")]
        [Display(Name = "Telefon Numarası")]
        [StringLength(20, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax")]
        [StringLength(20, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Fax { get; set; }

        [Display(Name = "Açıklama")]
        [StringLength(500, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string Description { get; set; }

        [Display(Name = "Şirket Unvanı")]
        public byte? FirmStatuTypeId { get; set; }

        [Display(Name = "Markanızın Adı")]
        [StringLength(250, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string ExtraBrandName { get; set; }

        [Display(Name = "Markanızın Başvuru/Tescil Numarası")]
        [StringLength(20, ErrorMessage = "{0} alanına maksimum {1} karakter girilebilir")]
        public string ExtraBrandRegistryNumber { get; set; }
    }
}