using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("RelatedEntity")]
    public class RelatedEntity : IEntity
    {
        public int RelatedEntityId { get; set; }
        public string TableName { get; set; }
    }
}
