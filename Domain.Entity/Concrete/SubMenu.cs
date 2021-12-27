using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Concrete
{
    [Table("SubMenu")]
    public class SubMenu : IEntity
    {
        [Key]
        [Required]
        public Guid SubMenuId { get; set; }

        [Required]
        public string SubMenuTitle { get; set; }

        [Required]
        public string SubMenuUrl { get; set; }

        [Required]
        public string SubMenuIcon { get; set; }

        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public int SubMenuOrder { get; set; }
    }
}
