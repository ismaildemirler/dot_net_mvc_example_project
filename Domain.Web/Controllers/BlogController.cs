using Domain.Business.Abstract.Blog;
using Domain.Entity.Container.Request.Blog;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
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
        public ActionResult Read(Guid id)
        {
            //if(Guid.TryParse(id, out Guid blogContentId))
            //{
                
            var model = _blogService.GetBlogContent(new RequestBlogContent { 
                BlogContentId = id
            });
            return View(model);

            //}
            //return RedirectToAction("Index", "Home");
        }
    }
}