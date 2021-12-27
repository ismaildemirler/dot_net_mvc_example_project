using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("Menu")]
    public class Menu : IEntity
    {
        [Key]
        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public string MenuTitle { get; set; }

        [Required]
        public string MenuUrl { get; set; }

        [Required]
        public string MenuIcon { get; set; }

        [Required]
        public byte UserTypeId { get; set; }

        [Required]
        public int MenuOrder { get; set; }
    }
}
