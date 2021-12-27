using Domain.Business.Abstract.Blog;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Blog;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Models.Blog;
using Domain.WebHelpers.Infrastructre;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class BlogController : BaseController
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index(int pageIndex = 1, int? categoryId = null)
        {
            ViewBag.CategoryId = categoryId;
            ViewBag.PageIndex = pageIndex;
            return View();
        }

        [HttpGet]
        public ActionResult Update(Guid? id = null)
        {
            BlogContentViewModel model = new BlogContentViewModel();
            
            if (id.HasValue)
            {
                BlogContent response = _blogService.GetBlogContent(new RequestBlogContent { BlogContentId = id.Value });
                
                model.BlogContentId = response.BlogContentId;
                model.CategoryId = response.CategoryId;
                model.Contents = response.Contents;
                model.ShortContent = response.ShortContent;
                model.Title = response.Title;
            }

            model.BlogCategories = CacheManager.GetBlogCategories.Select(s => new SelectListItem { Text = s.Description, Value = s.BlogCategoryId.ToString() }).ToList();
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(BlogContentViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                model.BlogCategories = CacheManager.GetBlogCategories.Select(s => new SelectListItem { Text = s.Description, Value = s.BlogCategoryId.ToString() }).ToList();
                return View(model);
            }

            byte[] img = null;
            if(model.PostImage != null)
            {
            using (MemoryStream ms = new MemoryStream())
            {
                model.PostImage.InputStream.CopyTo(ms);
                img = ms.GetBuffer();
            }
            }

            _blogService.SaveBlogContent(new RequestBlogContent { 
                BlogContentId = model.BlogContentId,
                CategoryId = model.CategoryId,
                Contents = model.Contents,
                ShortContent = model.ShortContent,
                Title = model.Title,
                PostImage = img
            });

            return RedirectToAction("Index");
        }
    }
}