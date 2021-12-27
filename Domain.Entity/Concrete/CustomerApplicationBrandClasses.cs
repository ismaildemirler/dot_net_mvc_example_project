using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("CustomerApplicationBrandClasses")]
    public class CustomerApplicationBrandClasses : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CustomerApplicationBrandClassesId { get; set; }
        
        [Required]
        public Guid BrandApplicationDetailId { get; set; }
        
        [Required]
        public int BrandSubClassesId { get; set; }
    }
}
