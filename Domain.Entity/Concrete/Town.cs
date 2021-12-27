using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("Town")]
    public class Town: IEntity
    {
        [Key]
        [Required]
        public int TownId { get; set; }
        
        [Required]
        public string TownName { get; set; }
        
        [Required]
        public int CityId { get; set; }
    }
}
