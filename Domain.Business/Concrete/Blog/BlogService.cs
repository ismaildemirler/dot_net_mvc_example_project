using Domain.Business.Abstract.Blog;
using Domain.DataAccess.Abstract.Blog;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Blog;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using System;
using System.Collections.Generic;

namespace Domain.Business.Concrete.Blog
{
    public class BlogService : IBlogService
    {
        private readonly IBlogDAL _blogDAL;
        private readonly IRepository<BlogContent> _repBlog;

        public BlogService(IBlogDAL blogDAL)
        {
            _blogDAL = blogDAL;
            _repBlog = _blogDAL.GetRepository<BlogContent>();
        }

        public PagedList<BlogContentComplexType> GetBlogPagedList(int pageIndex, int? categoryId = null, bool withContent = false)
        {
            return _blogDAL.GetBlogPagedList(pageIndex, categoryId, withContent);
        }

        public BlogContent GetBlogContent(RequestBlogContent request)
        {
            return _repBlog.Find(request.BlogContentId);
        }

        public void SaveBlogContent(RequestBlogContent request)
        {
            if(request.BlogContentId.HasValue && request.BlogContentId.Value != Guid.Empty)
            {
                BlogContent entity = _repBlog.Find(request.BlogContentId.Value);
                if (entity == null)
                    throw new BusinessException(Messages.KayitBulunamadi);

                entity.CategoryId = request.CategoryId;
                entity.Contents = request.Contents;
                entity.UpdateDate = DateTime.Now;
                entity.ShortContent = request.ShortContent;
                entity.Title = request.Title;
                entity.PostImage = request.PostImage;

                _repBlog.Update(entity);
            }
            else
            {
                BlogContent entity = new BlogContent()
                {
                    BlogContentId = Guid.NewGuid(),
                    CategoryId = request.CategoryId,
                    Contents = request.Contents,
                    PostDate = DateTime.Now,
                    ShortContent = request.ShortContent,
                    Title = request.Title,
                    PostImage = request.PostImage,
                    StateId = (byte)EnumState.Active
                };

                _repBlog.Insert(entity);
            }
        }

        public List<BlogContentComplexType> GetRecentBlogs()
        {
            return _blogDAL.GetRecentBlogs();
        }
    }
}
