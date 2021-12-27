using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.IndustrialDesign
{
    public interface IIndustrialDesignDAL : IRepositoryFactory
    {
        PagedList<IndustrialDesignApplicationComplexType> GetPagedListSearchedIndustrialDesignApplicationCT(
            DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters);

        List<IndustrialDesignApplicationComplexType> GetListSearchedIndustrialDesignApplicationCT(
            string userId);
    }
}
