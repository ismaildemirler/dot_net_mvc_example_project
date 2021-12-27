using Domain.Entity.ComplexType;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System.Collections.Generic;

namespace Domain.DataAccess.Abstract.Blog
{
    public interface IBlogDAL : IRepositoryFactory
    {
        PagedList<BlogContentComplexType> GetBlogPagedList(int pageIndex, int? categoryId = null, bool withContent = false);
        List<BlogContentComplexType> GetRecentBlogs();
    }
}
