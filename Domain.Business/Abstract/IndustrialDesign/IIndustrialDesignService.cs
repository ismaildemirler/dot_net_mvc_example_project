using Domain.Entity.ComplexType;
using Domain.Entity.Container.Request.IndustrialDesign;
using Domain.Entity.Container.Response.IndustrialDesign;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.Business.Abstract.IndustrialDesign
{
    public interface IIndustrialDesignService
    {
        #region IndustrialDesignApplication

        List<IndustrialDesignApplicationComplexType> GetAllApplications(string userId, int number = 10);

        ResponseIndustrialDesignApplication CreateIndustrialDesignApplication(
            RequestIndustrialDesignApplication request);

        PagedList<IndustrialDesignApplicationComplexType> GetPagedListSearchedIndustrialDesignApplicationCT(
            DtParameterModel dtParameterModel, List<SearchFilter> searchFilters);

        ResponseIndustrialDesignApplicationDetail GetIndustrialDesignApplicationDetailByRequest(
            RequestIndustrialDesignApplicationDetail request);

        void DeleteIndustrialDesign(RequestIndustrialDesignApplicationDetail request);
        #endregion
    }
}
