using Domain.Business.Abstract.Domain;
using Domain.DataAccess.Abstract.Domain;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Basket;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Business.Concrete.Domain
{
    public class DomainService : IDomainService
    {
        private readonly IDomainDAL _domainDAL;

        public DomainService(IDomainDAL domainDAL)
        {
            _domainDAL = domainDAL;
        }

        public List<DomainPrice> GetDomainPrices()
        {
            return _domainDAL.GetRepository<DomainPrice>().Queryable().ToList();
        }

        public List<DomainApplicationComplexType> GetAllDomainApplications(
            string userId, int number = 10)
        {
            return _domainDAL.GetListSearchedDomainApplicationCT(userId).Take(number).ToList();
        }
    }
}
