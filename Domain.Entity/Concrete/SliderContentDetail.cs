using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Concrete
{
    public class SliderContentDetail : IEntity
    {
        [Key]
        [Required]
        public Guid SliderContentDetailId { get; set; }

        public Guid SliderContentId { get; set; }

        [Required]
        [StringLength(250)]
        public string ContentInfo { get; set; }

        [Required]
        public byte RankNumber { get; set; }
    }
}
