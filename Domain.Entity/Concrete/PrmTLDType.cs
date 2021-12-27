using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("PrmTLDType")]
    public class PrmTLDType : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int TLDTypeId { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public byte StateId { get; set; }
    }


}
