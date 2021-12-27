using Domain.Entity.Enum;
using System;

namespace Domain.Entity.ComplexType
{
    public class SliderContentDetailComplexType
    {
        public Guid SliderContentDetailId { get; set; }
        public Guid SliderContentId { get; set; }
        public string ContentInfo { get; set; }
        public byte RankNumber { get; set; }
    }
}
