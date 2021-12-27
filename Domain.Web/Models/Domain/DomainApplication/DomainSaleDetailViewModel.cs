using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Web.Models.Domain.DomainApplication
{
    public class DomainSaleDetailViewModel
    {
        public byte StateId { get; set; }

        public Guid? ContactId { get; set; }

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