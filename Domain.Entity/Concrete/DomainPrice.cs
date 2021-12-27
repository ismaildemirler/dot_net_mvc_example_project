using Domain.Infrastructure.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("DomainPrice")]
    public class DomainPrice : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int DomainPriceId { get; set; }

        [Required]
        public int TLDTypeId { get; set; }

        [Required]
        public decimal RegisterPrice { get; set; }
        public decimal? RegisterPriceWithDiscount { get; set; }

        [Required]
        public decimal TransferPrice { get; set; }
        public decimal? TransferPriceWithDiscount { get; set; }

        [Required]
        public decimal RefreshPrice { get; set; }
        public decimal? RefreshPriceWithDiscount { get; set; }

        [Required]
        public byte StateId { get; set; }

        public string Currency { get; set; }
    }
}
