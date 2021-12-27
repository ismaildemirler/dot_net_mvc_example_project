using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;

namespace Domain.DataAccess.Abstract.ReferenceFirm
{
    public interface IReferenceFirmDAL : IRepositoryFactory
    {
        PagedList<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirmPagedList(int pageIndex);
    }
}
