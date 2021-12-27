using Domain.DataAccess.Abstract.ReferenceFirm;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.ReferenceFirm
{
    public class ReferenceFirmDAL : UnitOfWork<DomainDBContext>, IReferenceFirmDAL
    {
        public PagedList<Entity.Concrete.ReferenceFirm> GetActiveReferenceFirmPagedList(int pageIndex)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"select rf.ReferenceFirmId,
                                           rf.Name,
                                           rf.WorkName,
                                           rf.Detail,
                                           rf.InsertDate,
                                           rf.State,
                                           rf.LogoImage
                                    from ReferenceFirm rf {0} 
                                    WHERE 1 = 1 and rf.State = 1", "WITH(NOLOCK)");

            var response = GetPagedListSqlQuery<Entity.Concrete.ReferenceFirm>(sqlQuery.ToString(), prmLst, pageIndex, "InsertDate desc", 50);
            return response;
        }
    }
}
