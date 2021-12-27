using Domain.Entity.Util;
using Domain.Web.Models.Contact;
using Domain.WebHelpers.Infrastructre;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class ContactController : BaseController
    {   
        [HttpGet]
        public ActionResult Index()
        {
            var management = CacheManager.GetManagement;

            return View(new ContactUsViewModel
            { 
                FirmAddress = management.FirmAddress,
                FirmEMail = management.FirmEMail,
                FirmPhoneNumber = management.FirmPhoneNumber
            });
        }

        [HttpPost]
        public ActionResult Index(ContactUsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.info);
                return RedirectToAction("Index");
            }

            //SendMail

            ShowMessage(Messages.MesajIletildi, MessageType.info);
            return RedirectToAction("Index");
        }
    }
}