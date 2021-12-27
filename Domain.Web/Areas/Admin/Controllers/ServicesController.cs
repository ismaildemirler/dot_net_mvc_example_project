using Domain.Business.Abstract.Blog;
using Domain.Business.Abstract.Service;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Blog;
using Domain.Entity.Container.Response.Services;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Models.Blog;
using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class ServicesController : BaseController
    {
        private readonly IServicesService _servicesService;

        public ServicesController(IServicesService servicesService)
        {
            _servicesService = servicesService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetServicesList(DtParameterModel dtParameterModel)
        {
            var list = _servicesService.GetAllServices();
            list.ForEach(f =>
            {
                f.Buttons = $"<a class='dropdown-item grid-btn-delete text-danger' success-callback='GetServicesList' data-url='{Url.Action("Delete", new { id = f.ServiceID.Encrypt() })}'> <i class='fa fa-times'> </i> Sil </a> <a class='dropdown-item update text-primary' href='{Url.Action("Update", new { id = f.ServiceID })}'> <i class='fa fa-edit'> </i> Güncelle </a>";
            });

            return Json(new
            {
                data = list,
                draw = dtParameterModel.Draw,
                recordsTotal = list.Count,
                recordsFiltered = list.Count
            });
        }

        [HttpGet]
        public ActionResult Update(int id = 0)
        {
            ResponseService model = new ResponseService();
            if (id > 0)
            {
                model = _servicesService.GetService(new Entity.Container.Request.Services.RequestService{ ServiceID = id });
            }
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Update(ResponseService model)
        {
            if(!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            byte[] icon = null;


            if (model.FileIcon != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.FileIcon.InputStream.CopyTo(ms);
                    icon = ms.GetBuffer();
                }
            }
            else if (model.IconDeleted || model.ServiceID == 0)
            {
                ShowMessage("Lütfen hizmet ikonu için resim yükleyiniz.", MessageType.warning);
                return View(model);
            }

            byte[] img = null;
            if (model.FileImage != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    model.FileImage.InputStream.CopyTo(ms);
                    img = ms.GetBuffer();
                }
            }
            else if (model.ImageDeleted || model.ServiceID == 0)
            {
                ShowMessage("Lütfen hizmet detay sayfası için resim yükleyiniz.", MessageType.warning);
                return View(model);
            }

            _servicesService.SaveService(new Entity.Container.Request.Services.RequestService {
                ServiceID = model.ServiceID,
                ContentText = model.ContentText,
                DescriptionText = model.DescriptionText,
                Header = model.Header,
                Icon = icon,
                ServiceImage = img,
                IconDeleted = model.IconDeleted,
                ImageDeleted = model.ImageDeleted
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _servicesService.DeleteService(new Entity.Container.Request.Services.RequestService
            {
                ServiceID = id.DecryptToInt32()
            });
            return ShowMessageJSON(Messages.SilmeBasarili, AjaxResultState.Success);
        }
    }
}