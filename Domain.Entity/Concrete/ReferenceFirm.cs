using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("ReferenceFirm")]
    public class ReferenceFirm : IEntity
    {
        [Key]
        [Required]
        public Guid ReferenceFirmId { get; set; }

        [Required]
        public byte[] LogoImage { get; set; }

        [Required]
        [StringLength(350)]
        public string Name { get; set; }
        
        [StringLength(350)]
        public string WorkName { get; set; }

        public string Detail { get; set; }

        public byte State { get; set; }

        public DateTime InsertDate { get; set; }
    }
}
