using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Brand
{
    public interface IBrandDAL: IRepositoryFactory
    {
        List<BrandApplicationComplexType> GetListSearchedBrandApplicationCT(string userId, bool isBrand);

        PagedList<BrandApplicationComplexType> GetPagedListSearchedBrandApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters);

        PagedList<BrandForWatchingApplicationComplexType> GetPagedListSearchedBrandForWatchingApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters);

        List<BrandForWatchingApplicationComplexType> GetListSearchedBrandForWatchingApplicationCT(
            string userId);
    }
}
