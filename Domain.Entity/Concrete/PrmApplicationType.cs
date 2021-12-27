using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("PrmApplicationType")]
    public class PrmApplicationType : IEntity
    {
        [Key]
        [Required]
        public byte ApplicationTypeId { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}
