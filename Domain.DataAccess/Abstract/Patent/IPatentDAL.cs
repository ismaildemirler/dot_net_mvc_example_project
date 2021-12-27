using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Patent
{
    public interface IPatentDAL: IRepositoryFactory
    {
        PagedList<PatentApplicationComplexType> GetPagedListSearchedPatentApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters);

        List<PatentApplicationComplexType> GetListSearchedPatentApplicationCT(string userId);
    }
}
