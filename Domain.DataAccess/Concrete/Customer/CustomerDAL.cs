using Domain.DataAccess.Abstract.Customer;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Customer
{
    public class CustomerDAL : UnitOfWork<DomainDBContext>, ICustomerDAL
    {
    }
}
