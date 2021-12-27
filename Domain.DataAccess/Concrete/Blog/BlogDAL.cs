using Domain.DataAccess.Abstract.Blog;
using Domain.DataAccess.Concrete.DBContext;
using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Domain.DataAccess.Concrete.EntityFramework.Blog
{
    public class BlogDAL : UnitOfWork<DomainDBContext>, IBlogDAL
    {
        public PagedList<BlogContentComplexType> GetBlogPagedList(int pageIndex, int? categoryId = null, bool withContent = false)
        {
            var sqlQuery = new StringBuilder();
            var prmLst = new List<SqlParameter>();

            sqlQuery.AppendFormat(@"select bc.BlogContentId,
	                                       bc.CategoryId,
	                                       bc.PostDate,
	                                       bc.PostImage,
	                                       bc.ShortContent,
	                                       bc.Title,
	                                       c.Description as CategoryName
	                                       {1}
                                    from BlogContent bc {0} inner join PrmBlogCategory c {0} on bc.CategoryId = c.BlogCategoryId 
                                    WHERE 1 = 1 ", "WITH(NOLOCK)",  withContent ? ",bc.Contents" : "");

            if (categoryId.HasValue)
            {
                var param = new SqlParameter("prmCategoryId", categoryId.Value);
                sqlQuery.AppendFormat(" AND {0} = @{1} ", "bc.CategoryId", param.ParameterName);
                prmLst.Add(param);
            }

            var response = GetPagedListSqlQuery<BlogContentComplexType>(sqlQuery.ToString(), prmLst, pageIndex, "PostDate desc", 5);
            return response;
        }

        public List<BlogContentComplexType> GetRecentBlogs()
        {
            var sqlQuery = new StringBuilder();

            sqlQuery.AppendFormat(@"select top 3 bc.BlogContentId,
	                                       bc.CategoryId,
	                                       bc.PostDate,
	                                       bc.PostImage,
	                                       bc.ShortContent,
	                                       bc.Title,
	                                       c.Description as CategoryName
                                    from BlogContent bc {0} inner join PrmBlogCategory c {0} on bc.CategoryId = c.BlogCategoryId 
                                    order by PostDate desc ", "WITH(NOLOCK)");

            var response = ExecuteSqlCommand<BlogContentComplexType>(sqlQuery.ToString(), new List<SqlParameter>().ToArray()).ToList();
            return response;
        }
    }
}
