using Domain.Business.Abstract.Service;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class ServicesController : Controller
    {
        private readonly IServicesService service;

        public ServicesController(IServicesService service)
        {
            this.service = service;
        }

        // GET: Hizmetlerimiz
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Detail(string id)
        {
            var model = service.GetService(new Entity.Container.Request.Services.RequestService { ServiceID = id.DecryptToInt32() });
            return View(model);
        }
    }
}