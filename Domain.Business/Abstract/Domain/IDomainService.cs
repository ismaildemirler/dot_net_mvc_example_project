using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Domain
{
    public interface IDomainService
    {
        List<DomainPrice> GetDomainPrices();

        List<DomainApplicationComplexType> GetAllDomainApplications(
            string userId, int number = 10);
    }
}
