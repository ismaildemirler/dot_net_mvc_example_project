using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("PrmFirmStatusType")]
    public class PrmFirmStatusType : IEntity
    {
        [Key]
        [Required]
        public byte FirmStatusTypeId { get; set; }
        
        [Required]
        public string Description { get; set; }
    }
}
