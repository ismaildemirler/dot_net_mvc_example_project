using Domain.DataAccess.Abstract.Parameter;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.Parameter
{
    public class ParameterDAL : UnitOfWork<DomainDBContext>, IParameterDAL
    {
        public List<DomainPriceComplexType> GetDomainPriceList()
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"select dp.*, tld.Description as TLDTypeDescription from DomainPrice dp {0} 
                                    inner join PrmTLDType tld {0} on dp.TLDTypeId = tld.TLDTypeId 
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            return ExecuteSqlCommand<DomainPriceComplexType>(sqlQuery.ToString(), prmLst.ToArray()).ToList();
        }
    }
}
