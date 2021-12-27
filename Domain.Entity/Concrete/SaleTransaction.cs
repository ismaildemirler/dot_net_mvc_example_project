using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("SaleTransaction")]
    public class SaleTransaction : IEntity
    {
        [Key]
        [Required]
        public Guid SaleTransactionId { get; set; }
        public Guid SaleId { get; set; }
        public string Code { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Processor { get; set; }
        public string ProcessorTransactionId { get; set; }
        public decimal Amount { get; set; }
        public string CCNum { get; set; }
        public string CCType { get; set; }
        public string Response { get; set; }
    }
}
