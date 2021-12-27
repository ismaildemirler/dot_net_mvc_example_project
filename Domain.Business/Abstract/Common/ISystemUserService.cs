using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Common
{
    public interface ISystemUserService
    {
        SystemUser GetSystemUser(RequestSystemUser request);
        int CreateSystemUser(RequestSystemUser request);
        PagedList<SystemUserComplexType> GetUserPagedList(DtParameterModel dtParameterModel, List<SearchFilter> searchFilters);
        void ChangeUserState(RequestSystemUser request);
    }
}
