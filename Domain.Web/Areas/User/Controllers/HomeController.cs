using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Web.Infrastructure;
using System.Web.Mvc;

namespace Domain.Web.Areas.User.Controllers
{
    [DomainAuthorize(Roles = "Customer")]
    public class HomeController : DomainBaseController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}