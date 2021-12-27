using Domain.DataAccess.Abstract.Common;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Common
{
    public class SystemUserDAL : UnitOfWork<DomainDBContext>, ISystemUserDAL
    {
        public SystemUser GetSystemUser(RequestSystemUser request)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"SELECT 
                                        su.SystemUserId, 
                                        su.StateId, 
                                        su.FirstName, 
                                        su.LastName, 
                                        su.Gender,
                                        su.UserName, 
                                        su.Password,
                                        su.Email, 
                                        su.CreationDate, 
                                        su.UpdateDate, 
                                        su.LastLoginDate
                                    FROM SystemUser su {0}
                                    WHERE 1=1 ", " WITH(NOLOCK) ");

            var param = new SqlParameter("prmUserName", request.EMail);
            sqlQuery.AppendFormat(" AND {0} = @{1} ", "su.UserName", param.ParameterName);
            prmLst.Add(param);

            return ExecuteSqlCommand<SystemUser>(sqlQuery.ToString(), prmLst.ToArray()).FirstOrDefault();
        }

        public PagedList<SystemUserComplexType> GetUserPagedList(DtParameterModel dtParameterModel, List<SearchFilter> searchFilters)
        {
            var sqlQuery = new StringBuilder();
            
            sqlQuery.AppendFormat(@"select su.*, ut.Description as UserType, st.Description as State from SystemUser su {0} 
                                    inner join PrmUserType ut {0} on su.UserTypeId = ut.UserTypeId 
                                    inner join PrmState st {0} on su.StateId = st.StateId WHERE 1 = 1 ", "WITH(NOLOCK)");

            var response = GetPagedListSqlQueryWithSearchFilters<SystemUserComplexType>(sqlQuery.ToString(), searchFilters, dtParameterModel);
            return response;
        }
    }
}
