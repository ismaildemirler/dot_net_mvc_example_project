using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("Beneficiary")]
    public class Beneficiary : IEntity
    {
        [Key]
        [Required]
        public Guid BeneficiaryId { get; set; }

        public Guid? BrandApplicationDetailId { get; set; }

        public Guid? PatentApplicationDetailId { get; set; }

        public string FirmName { get; set; }

        [Display(Name = "Eski Firma Unvanı")]
        public string PreviousFirmName { get; set; }

        [Display(Name = "Şahıs Adı")]
        public string PersonName { get; set; }

        [Display(Name = "T.C. Kimlik Numarası")]
        public string TCNumber { get; set; }

        [Display(Name = "Doğum Yeri")]
        public string BirthPlace { get; set; }

        [Display(Name = "Doğum Tarihi")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Vergi Dairesi")]
        public string TaxOffice { get; set; }

        [Display(Name = "Vergi Numarası")]
        public string TaxNumber { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Display(Name = "Eski Resmi Adres")]
        public string PreviousOfficialAddress { get; set; }

        [Display(Name = "Email Adresi")]
        public string Email { get; set; }

        [Display(Name = "İl")]
        public int? CityId { get; set; }

        [Display(Name = "İlçe")]
        public int? TownId { get; set; }

        [Display(Name = "Telefon Numarası")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Açıklama")]
        public string Description { get; set; }

        [Display(Name = "Şirket Unvanı")]
        public byte? FirmStatuTypeId { get; set; }

        [Display(Name = "Markanızın Adı")]
        public string ExtraBrandName { get; set; }

        [Display(Name = "Markanızın Başvuru/Tescil Numarası")]
        public string ExtraBrandRegistryNumber { get; set; }
    }
}
