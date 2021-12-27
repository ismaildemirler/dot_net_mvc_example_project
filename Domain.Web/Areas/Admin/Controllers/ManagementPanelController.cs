using Domain.Business.Abstract.Management;
using Domain.Entity.ComplexType;
using Domain.Entity.Container.Request.Manage;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Web.Areas.Admin.Model.ManagementPanel;
using Domain.WebHelpers.Infrastructre;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class ManagementPanelController : BaseController
    {
        private readonly IManagementService _managementService;

        public ManagementPanelController(IManagementService managementService)
        {
            _managementService = managementService;
        }

        [HttpGet]
        public ActionResult AboutPage()
        {
            AboutPageViewModel model = new AboutPageViewModel();
            model.Content = _managementService.GetAboutPageContent(); 
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AboutPage(AboutPageViewModel model)
        {
            _managementService.SaveAboutPageContent(new RequestManagement
            {
                AboutPageContent = model.Content
            });
            return ShowMessageJSON(Messages.KayitBasarili);
        }

        [HttpGet]
        public ActionResult ContactPage()
        {
            ContactPageViewModel model = new ContactPageViewModel();
            ContactPageComplexType entity = _managementService.GetContactPageInformation();
            if(entity != null)
            {
                model.FirmAddress = entity.FirmAddress;
                model.FirmPhoneNumber = entity.FirmPhoneNumber;
                model.FirmEMail = entity.FirmEMail;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult ContactPage(ContactPageViewModel model)
        {
            _managementService.SaveContactPageInformation(new RequestManagement {
                FirmAddress = model.FirmAddress,
                FirmPhoneNumber = model.FirmPhoneNumber,
                FirmEMail = model.FirmEMail
            });
            return ShowMessageJSON(Messages.KayitBasarili);
        }
    }
}