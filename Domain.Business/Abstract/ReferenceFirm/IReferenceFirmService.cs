using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System;
using System.Collections.Generic;

namespace Domain.Business.Abstract.ReferenceFirm
{
    public interface IReferenceFirmService
    {
        List<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirms();

        PagedList<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirmPagedList(int pageIndex);

        PagedList<Entity.Concrete.ReferenceFirm> GetReferenceFirmPagedList(DtParameterModel dtParameterModel, List<SearchFilter> filters);

        Entity.Concrete.ReferenceFirm GetReferenceFirm(Guid sliderContentId);

        Guid SaveReferenceFirm(Entity.Concrete.ReferenceFirm sliderContent, bool imageDeleted);
    }
}
