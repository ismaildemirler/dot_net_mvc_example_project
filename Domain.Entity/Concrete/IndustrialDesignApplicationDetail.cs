using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("IndustrialDesignApplicationDetail")]
    public class IndustrialDesignApplicationDetail : IEntity
    {
        [Key]
        [Required]
        public Guid IndustrialDesignApplicationDetailId { get; set; }
       
        [Required]
        [Display(Name = "Başlık")]
        public string Title { get; set; }
        
        [Required]
        public Guid CustomerApplicationId { get; set; }

        [Display(Name = "Tasarım Başvuru Tarihi")]
        public DateTime DesignApplicationDate { get; set; }
    }
}
