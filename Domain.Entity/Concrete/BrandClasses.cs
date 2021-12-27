using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("BrandClasses")]
    public class BrandClasses: IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BrandClassesId { get; set; }
        
        [Required]
        public string BrandClassesIdentification { get; set; }
        
        [Required]
        public string BrandClassesName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
