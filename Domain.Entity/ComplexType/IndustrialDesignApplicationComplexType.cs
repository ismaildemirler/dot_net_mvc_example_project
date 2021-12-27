using System;

namespace Domain.Entity.ComplexType
{
    public class IndustrialDesignApplicationComplexType
    {
        public Guid IndustrialDesignApplicationDetailId { get; set; }
        public string Title { get; set; }
        public Guid CustomerApplicationId { get; set; }
        public DateTime DesignApplicationDate { get; set; }
    }
}
