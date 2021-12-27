using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("BrandApplicationDetail")]
    public class BrandApplicationDetail : IEntity
    {
        [Key]
        [Required]
        public Guid BrandApplicationDetailId { get; set; }
       
        [Required]
        [Display(Name = "Marka Adı")]
        public string BrandName { get; set; }

        [Display(Name = "Başvuru/Tescil No")]
        public string RegistryNumber { get; set; }

        [Display(Name = "Marka Kategori Açıklaması")]
        public string BrandCategoryDescription { get; set; }
        
        [Display(Name = "Firma Başvuru Türü")]
        public byte? BrandApplicationTypeId { get; set; }
        
        [Display(Name = "Varsa Özel Emtia")]
        public string SpecialClass { get; set; }
        
        [Required]
        [Display(Name = "Ön Yazı Gönderilsin Mi?")]
        public bool SendCoverLetter { get; set; }

        public Guid? ContactId { get; set; }
        
        [Required]
        public Guid CustomerApplicationId { get; set; }

        [Display(Name = "Marka Başvuru Tarihi")]
        public DateTime? BrandApplicationDate { get; set; }
    }
}
