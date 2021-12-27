using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Blog;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System.Collections.Generic;

namespace Domain.Business.Abstract.Blog
{
    public interface IBlogService
    {
        PagedList<BlogContentComplexType> GetBlogPagedList(int pageIndex, int? categoryId = null, bool withContent = false);
        void SaveBlogContent(RequestBlogContent request);
        BlogContent GetBlogContent(RequestBlogContent request);
        List<BlogContentComplexType> GetRecentBlogs();
    }
}
