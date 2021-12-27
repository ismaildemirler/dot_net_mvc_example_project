using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("CustomerApplication")]
    public class CustomerApplication : IEntity
    {
        [Key]
        [Required]
        public Guid CustomerApplicationId { get; set; }
        
        [Required]
        [Display(Name = "Başvuru Türünüz")]
        public byte ApplicationTypeId { get; set; }
        
        [Required]
        public DateTime ApplyDate { get; set; }
        
        [Required]
        public byte StateId { get; set; }
        
        public int? CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public String AnonymousApplicationCode { get; set; }
    }
}
