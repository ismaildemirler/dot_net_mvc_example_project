using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Brand;
using Domain.Entity.Container.Response.Brand;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Brand
{
    public interface IBrandService
    {
        #region BrandApplication
        List<BrandApplicationComplexType> GetAllApplications(string userId, bool isBrand, int number = 10);

        ResponseBrandApplication CreateBrandApplication(RequestBrandApplication request);

        PagedList<BrandApplicationComplexType> GetPagedListSearchedBrandApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters);

        ResponseBrandApplicationDetail GetBrandApplicationDetailByRequest(RequestBrandApplicationDetail request);

        ResponseBrandApplicationDetail GetCustomerApplicationBrandSubClassesByRequest(RequestBrandApplicationDetail request);

        void DeleteBrand(RequestBrandApplicationDetail request);
        #endregion

        #region BrandWatchApplication
        List<BrandForWatchingApplicationComplexType> GetAllBrandWatchApplications(
            string userId, int number = 10);

        List<BrandForWatching> GetAllBrandWatchApplications(int number = 10);

        void DeleteBrandForWatching(RequestBrandForWatching request);

        ResponseBrandForWatching GetBrandForWatchingByRequest(RequestBrandForWatching request);
        
        ResponseBrandWatchingApplication CreateBrandWatchApplication(RequestBrandWatchingApplication request);

        ResponseBrandForWatching CreateBrandForWatchingRegistry(RequestBrandForWatchingRegistration request);

        ResponseBrandWatchingApplicationDetail GetBrandWatchApplicationDetailByRequest(RequestBrandWatchingApplicationDetail request);

        PagedList<BrandForWatchingApplicationComplexType> GetPagedListSearchedBrandForWatchingApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters);

        PagedList<BrandWatchingApplicationDetail> GetNotCompletedPagedListSearchedBrandForWatchingApplicationCT(
            DtParameterModel dtParameterModel,
            RequestBrandWatchingApplicationDetail request);

        void DeleteBrandForWatchingApplication(RequestBrandForWatching request);
        #endregion
    }
}
