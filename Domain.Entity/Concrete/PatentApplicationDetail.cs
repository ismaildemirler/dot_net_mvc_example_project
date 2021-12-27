using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("PatentApplicationDetail")]
    public class PatentApplicationDetail : IEntity
    {
        [Key]
        [Required]
        public Guid PatentApplicationDetailId { get; set; }
       
        [Required]
        [Display(Name = "Buluş Başlığı")]
        public string Title { get; set; }

        [Display(Name = "Buluş Özeti")]
        public string Summary { get; set; }

        public string PresentFeatures { get; set; }

        public string PlannedFeatures { get; set; }
        
        [Required]
        public Guid CustomerApplicationId { get; set; }

        [Display(Name = "Teknik çizimi mail atacağım")]
        public bool Mail { get; set; }
    }
}
