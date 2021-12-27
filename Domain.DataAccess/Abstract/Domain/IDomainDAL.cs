using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Domain
{
    public interface IDomainDAL : IRepositoryFactory
    {
        List<DomainApplicationComplexType> GetListSearchedDomainApplicationCT(string userId);
    }
}
