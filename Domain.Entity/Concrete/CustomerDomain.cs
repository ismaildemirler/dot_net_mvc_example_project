using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("CustomerDomain")]
    public class CustomerDomain : IEntity
    {
        [Key]
        [Required]
        public Guid CustomerDomainId { get; set; }

        [Required]
        public Guid SaleDetailId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime ExpirationDate { get; set; }

        [Required]
        public byte Period { get; set; }

        [Required]
        public string DomainName { get; set; }

        [Required]
        public int TLDTypeId { get; set; }

        [Required]
        public int DomainOperationTypeId { get; set; }

        [Required]
        public int DomainStateId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        public Guid? TransferredCustomerId { get; set; }

        public Guid DomainDetailId { get; set; }

        public string IpAddress { get; set; }

        public Guid CustomerApplicationId { get; set; }
    }
}
