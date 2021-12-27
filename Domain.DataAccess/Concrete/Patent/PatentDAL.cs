using Domain.DataAccess.Abstract.Patent;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Patent
{
    public class PatentDAL : UnitOfWork<DomainDBContext>, IPatentDAL
    {
        public PagedList<PatentApplicationComplexType> GetPagedListSearchedPatentApplicationCT(DtParameterModel dtParameterModel,
            List<SearchFilter> searchFilters)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                    	p.PatentApplicationDetailId
                                       ,p.Title
                                       ,p.Summary
                                       ,p.CustomerApplicationId
                                       ,bf.BeneficiaryId
                                       ,bf.FirmName
                                       ,bf.Address
                                       ,bf.CityId
                                       ,bf.TownId
                                       ,bf.PhoneNumber
                                       ,bf.PersonName
                                       ,bf.TaxNumber
                                       ,bf.TaxOffice
                                       ,ca.ApplicationTypeId
                                    FROM PatentApplicationDetail p {0}
                                    INNER JOIN CustomerApplication ca {0} ON p.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN Beneficiary bf {0} ON p.PatentApplicationDetailId = bf.PatentApplicationDetailId
                                    INNER JOIN PrmApplicationType pat {0} ON ca.ApplicationTypeId = pat.ApplicationTypeId
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            var response = GetPagedListSqlQueryWithSearchFilters<PatentApplicationComplexType>(sqlQuery.ToString(), searchFilters, dtParameterModel);
            return response;
        }

        public List<PatentApplicationComplexType> GetListSearchedPatentApplicationCT(string userId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                    	p.PatentApplicationDetailId
                                       ,p.Title
                                       ,p.Summary
                                       ,p.CustomerApplicationId
                                       ,bf.BeneficiaryId
                                       ,bf.FirmName
                                       ,bf.Address
                                       ,bf.CityId
                                       ,bf.TownId
                                       ,bf.PhoneNumber
                                       ,bf.PersonName
                                       ,bf.TaxNumber
                                       ,bf.TaxOffice
                                       ,ca.ApplicationTypeId
                                    FROM PatentApplicationDetail p {0}
                                    INNER JOIN CustomerApplication ca {0} ON p.CustomerApplicationId = ca.CustomerApplicationId
                                    LEFT JOIN Beneficiary bf {0} ON p.PatentApplicationDetailId = bf.PatentApplicationDetailId
                                    INNER JOIN PrmApplicationType pat {0} ON ca.ApplicationTypeId = pat.ApplicationTypeId
                                    WHERE 1=1 AND ca.CustomerId={1} ", " WITH(NOLOCK) ", userId);

            var response = ExecuteSqlCommand<PatentApplicationComplexType>(sqlQuery.ToString());
            return response.ToList();
        }
    }
}
