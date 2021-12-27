using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("PrmBrandApplicationType")]
    public class PrmBrandApplicationType : IEntity
    {
        [Key]
        [Required]
        public byte BrandApplicationTypeId { get; set; }

        [Required]
        public string Description { get; set; }

        public string HelpText { get; set; }
    }
}
