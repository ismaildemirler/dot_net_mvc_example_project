using Domain.DataAccess.Abstract.Contact;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Contact
{
    public class ContactDAL : UnitOfWork<DomainDBContext>, IContactDAL
    {
    }
}
