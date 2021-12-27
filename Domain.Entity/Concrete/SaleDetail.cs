using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("SaleDetail")]
    public class SaleDetail : IEntity
    {
        [Key]
        [Required]
        public Guid SaleDetailId { get; set; }

        [Required]
        public Guid SaleId { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public byte RelatedEntityTypeId { get; set; }

        [Required]
        public Guid RelatedEntityId { get; set; }
    }
}
