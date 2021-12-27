using Domain.DataAccess.Abstract.Menus;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Menus
{
    public class MenuDAL : UnitOfWork<DomainDBContext>, IMenuDAL
    {
    }
}
