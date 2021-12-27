using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("City")]
    public class City : IEntity
    {
        [Key]
        [Required]
        public int CityId { get; set; }
       
        [Required]
        public string CityName { get; set; }
    }
}
