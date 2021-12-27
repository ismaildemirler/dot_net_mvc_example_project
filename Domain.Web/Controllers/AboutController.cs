using Domain.Business.Abstract.Management;
using Domain.Web.Areas.Admin.Model.ManagementPanel;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class AboutController : Controller
    {
        private readonly IManagementService _managementService;

        public AboutController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        public ActionResult Index()
        {
            AboutPageViewModel model = new AboutPageViewModel();
            model.Content = _managementService.GetAboutPageContent();
            return View(model);
        }
    }
}