using Domain.Infrastructure.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Concrete
{
    public class SliderContent : IEntity
    {
        [Key]
        [Required]
        public Guid SliderContentId { get; set; }

        public byte[] SliderImage { get; set; }

        [Required]
        [StringLength(100)]
        public string Header { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Topic { get; set; }

        public bool HasPriceSection { get; set; }

        public decimal? Price { get; set; }

        public byte? CurrencyTypeId { get; set; }

        public byte? DurationTypeId { get; set; }

        [StringLength(200)]
        public string PriceDescription { get; set; }
        
        public bool HasDetailLink { get; set; }

        [StringLength(500)]
        public string DetailUrl { get; set; }

        [Required]
        public byte StateId { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
    }
}
