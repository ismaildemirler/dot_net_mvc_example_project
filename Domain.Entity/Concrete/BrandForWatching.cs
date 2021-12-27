using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("BrandForWatching")]
    public class BrandForWatching : IEntity
    {
        [Key]
        [Required]
        public Guid BrandForWatchingId { get; set; }
       
        [Required]
        [Display(Name = "Marka Adı")]
        public string BrandName { get; set; }

        [Required]
        [Display(Name = "Firma Adı")]
        public string FirmName { get; set; }

        [Required]
        [Display(Name = "İzlenecek Sınıflar")]
        public string ClassesToWatch { get; set; }

        [Required]
        [Display(Name = "Başvuru/Tescil No")]
        public string RegistryNumber { get; set; }

        [Required]
        public Guid BrandWatchingApplicationDetailId { get; set; }
    }
}
