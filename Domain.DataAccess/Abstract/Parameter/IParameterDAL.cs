using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Parameter
{
    public interface IParameterDAL : IRepositoryFactory
    {
        List<DomainPriceComplexType> GetDomainPriceList();
    }
}
