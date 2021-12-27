using Domain.DataAccess.Abstract.Management;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Management
{
    public class ManagementDAL : UnitOfWork<DomainDBContext>, IManagementDAL
    {
    }
}
