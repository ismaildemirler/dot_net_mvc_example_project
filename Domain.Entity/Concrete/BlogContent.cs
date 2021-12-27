using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Concrete
{
    public class BlogContent : IEntity
    {
        [Key]
        [Required]
        public Guid BlogContentId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Contents { get; set; }

        [Required]
        [StringLength(500)]
        public string ShortContent { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public byte[] PostImage { get; set; }

        [Required]
        public DateTime PostDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public byte StateId { get; set; }
    }
}
