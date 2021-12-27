using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("Form")]
    public class Form : IEntity
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public int RelatedEntityId { get; set; }
    }
}
