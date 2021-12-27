using Domain.DataAccess.Abstract.Beneficiary;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;

namespace Domain.DataAccess.Concrete.EntityFramework.Beneficiary
{
    public class BeneficiaryDAL : UnitOfWork<DomainDBContext>, IBeneficiaryDAL
    {
    }
}
