using System;

namespace Domain.Entity.ComplexType
{
    public class DomainApplicationComplexType
    {
        public Guid CustomerDomainId { get; set; }
        public string DomainName { get; set; }
        public byte Period { get; set; }
        public DateTime ExpirationDate { get; set; }
        public DateTime StartDate { get; set; }
        public Guid CustomerApplicationId { get; set; }
    }
}
