using Domain.DataAccess.Abstract.IndustrialDesign;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.IndustrialDesign
{
    public class IndustrialDesignDAL : UnitOfWork<DomainDBContext>, IIndustrialDesignDAL
    {
        public PagedList<IndustrialDesignApplicationComplexType> GetPagedListSearchedIndustrialDesignApplicationCT(
            DtParameterModel dtParameterModel, List<SearchFilter> searchFilters)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                    	ida.IndustrialDesignApplicationDetailId
                                       ,ida.Title
                                       ,ida.DesignApplicationDate
                                       ,ida.CustomerApplicationId
                                    FROM IndustrialDesignApplicationDetail ida {0}
                                    INNER JOIN CustomerApplication ca {0} ON ida.CustomerApplicationId = ca.CustomerApplicationId
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            var response = GetPagedListSqlQueryWithSearchFilters<IndustrialDesignApplicationComplexType>(
                sqlQuery.ToString(), searchFilters, dtParameterModel);
            return response;
        }

        public List<IndustrialDesignApplicationComplexType> GetListSearchedIndustrialDesignApplicationCT(
            string userId)
        {
            var sqlQuery = new StringBuilder();
            sqlQuery.AppendFormat(@"SELECT
                                    	ida.IndustrialDesignApplicationDetailId
                                       ,ida.Title
                                       ,ida.DesignApplicationDate
                                       ,ida.CustomerApplicationId
                                    FROM IndustrialDesignApplicationDetail ida {0}
                                    INNER JOIN CustomerApplication ca {0} ON ida.CustomerApplicationId = ca.CustomerApplicationId
                                    WHERE 1=1 AND ca.CustomerId={1}", " WITH(NOLOCK) ", userId);

            var response = ExecuteSqlCommand<IndustrialDesignApplicationComplexType>(sqlQuery.ToString());
            return response.ToList();
        }
    }
}
