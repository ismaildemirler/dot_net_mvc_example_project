using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    public class Services : IEntity
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceID { get; set; }
        [Required]
        public byte[] Icon { get; set; }
        [Required]
        public byte[] ServiceImage { get; set; }
        [Required]
        [StringLength(250)]
        public string Header { get; set; }
        [Required]
        [StringLength(1000)]
        public string DescriptionText { get; set; }
        [Required]
        public string ContentText { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
