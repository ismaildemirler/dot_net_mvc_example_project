using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("BrandSubClasses")]
    public class BrandSubClasses : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BrandSubClassesId { get; set; }
        
        [Required]
        public string BrandSubClassesIdentification { get; set; }
        
        [Required]
        public string BrandSubClassesName { get; set; }
        
        [Required]
        public int BrandClassesId { get; set; }
    }
}
