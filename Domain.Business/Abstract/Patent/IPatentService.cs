using Domain.Entity.ComplexType;
using Domain.Entity.Container.Request.Patent;
using Domain.Entity.Container.Response.Patent;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Patent
{
    public interface IPatentService
    {
        #region PatentApplication
        List<PatentApplicationComplexType> GetAllApplications(string userId, int number = 10);

        ResponsePatentApplication CreatePatentApplication(RequestPatentApplication request);

        PagedList<PatentApplicationComplexType> GetPagedListSearchedPatentApplicationCT(DtParameterModel dtParameterModel,
           List<SearchFilter> searchFilters);

        ResponsePatentApplicationDetail GetPatentApplicationDetailByRequest(RequestPatentApplicationDetail request);

        void DeletePatent(RequestPatentApplicationDetail request);
        #endregion
    }
}
