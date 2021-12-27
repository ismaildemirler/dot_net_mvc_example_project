using Domain.DataAccess.Abstract.Domain;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Domain
{
    public class DomainDAL : UnitOfWork<DomainDBContext>, IDomainDAL
    {
        public List<DomainApplicationComplexType> GetListSearchedDomainApplicationCT(string userId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                        cd.CustomerDomainId
                                       ,cd.StartDate
                                       ,cd.ExpirationDate
                                       ,cd.Period
                                       ,ca.CustomerApplicationId
                                    FROM CustomerDomain cd {0}
                                    INNER JOIN CustomerApplication ca {0} ON cd.CustomerApplicationId = ca.CustomerApplicationId
                                    WHERE 1=1 AND ca.CustomerId={1}", " WITH(NOLOCK) ", userId);

            var response = ExecuteSqlCommand<DomainApplicationComplexType>(sqlQuery.ToString());
            return response.ToList();
        }
    }
}
