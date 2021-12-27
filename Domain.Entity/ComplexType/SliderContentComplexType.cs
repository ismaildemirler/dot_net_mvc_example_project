using Domain.Entity.Enum;
using System;

namespace Domain.Entity.ComplexType
{
    public class SliderContentComplexType
    {
        public Guid SliderContentId { get; set; }
        public byte[] SliderImage { get; set; }
        public bool ImageDeleted { get; set; }
        public string Header { get; set; }
        public string Topic { get; set; }
        public bool HasPriceSection { get; set; }
        public decimal? Price { get; set; }
        public byte? CurrencyTypeId { get; set; }
        public byte? DurationTypeId { get; set; }
        public string PriceDescription { get; set; }
        public bool HasDetailLink { get; set; }
        public string DetailUrl { get; set; }
        public byte StateId { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
