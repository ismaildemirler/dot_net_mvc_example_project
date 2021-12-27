using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Request.Parameter
{
    public class RequestDomainPrice
    {
        public int DomainPriceId { get; set; }
        public DomainPrice DomainPrice { get; set; }
    }
}
