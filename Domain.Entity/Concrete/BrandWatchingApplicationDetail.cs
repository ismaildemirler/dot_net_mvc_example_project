using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("BrandWatchingApplicationDetail")]
    public class BrandWatchingApplicationDetail : IEntity
    {
        [Key]
        [Required]
        public Guid BrandWatchingApplicationDetailId { get; set; }
       
        [Required]
        [Display(Name = "T.C Kimlik Numarası")]
        public string IdentityNumber { get; set; }

        [Required]
        [Display(Name = "Firma Ünvanı veya Şahıs Adı-Soyadı")]
        public string FirmName { get; set; }

        [Display(Name = "Adres")]
        public string Address { get; set; }

        [Required]
        [Display(Name = "İl")]
        public int CityId { get; set; }

        [Required]
        [Display(Name = "İlçe")]
        public int TownId { get; set; }

        [Required]
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; }

        [Display(Name = "Fax Numarası")]
        public string Fax { get; set; }

        [Required]
        public Guid CustomerApplicationId { get; set; }
    }
}
