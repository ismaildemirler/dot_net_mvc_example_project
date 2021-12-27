using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    public class PrmBlogCategory : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int BlogCategoryId { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        [Required]
        public byte StateId { get; set; }
    }
}
