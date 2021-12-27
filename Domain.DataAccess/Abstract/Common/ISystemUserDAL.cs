using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Common
{
    public interface ISystemUserDAL: IRepositoryFactory
    {
        SystemUser GetSystemUser(RequestSystemUser request);

        PagedList<SystemUserComplexType> GetUserPagedList(DtParameterModel dtParameterModel, List<SearchFilter> searchFilters);
    }
}
