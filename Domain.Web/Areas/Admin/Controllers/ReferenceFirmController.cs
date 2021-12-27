using Domain.Business.Abstract.ReferenceFirm;
using Domain.Entity.Concrete;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.DataAccess.EntityFramework.UnitOfWork.PagedList;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Areas.Admin.Models.ReferenceFirm;
using Domain.WebHelpers.Infrastructre;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class ReferenceFirmController : BaseController
    {
        private readonly IReferenceFirmService _referenceFirmService;

        public ReferenceFirmController(IReferenceFirmService referenceFirmService)
        {
            _referenceFirmService = referenceFirmService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetReferenceFirmPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);

            PagedList<ReferenceFirm> lst = _referenceFirmService.GetReferenceFirmPagedList(dtParameterModel, filters);
            return Json(new
            {
                data = lst.Items.Select(s => new ReferenceFirmViewModel()
                {
                    Detail = s.Detail,
                    StateDesc = (s.State == (byte)EnumState.Active ? "Aktif" : "Pasif"),
                    InsertDate = s.InsertDate,
                    Name = s.Name,
                    WorkName = s.WorkName,
                    Buttons = ApplicationButtons(s),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(ReferenceFirm entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a class='dropdown-item grid-btn-insert text-primary' data-action='{Url.Action("Update", new { id = entity.ReferenceFirmId })}'> <i class='fa fa-edit'> </i> Güncelle </a>");
            
            return sb.ToString();
        }

        [HttpGet]
        public ActionResult Update(Guid? id = null)
        {
            ReferenceFirmViewModel model = new ReferenceFirmViewModel();
            
            if(id != null)
            {
                var response = _referenceFirmService.GetReferenceFirm(id.Value);

                model.ReferenceId = response.ReferenceFirmId.Encrypt();
                model.Name = response.Name;
                model.WorkName = response.WorkName;
                model.Detail = response.Detail;
                model.LogoImage = response.LogoImage;
                model.InsertDate = response.InsertDate;
                model.StateDesc = response.State.ToString();
                ViewBag.NewRecord = false;
            }
            else
            {
                model.ReferenceId = Guid.Empty.Encrypt();
                ViewBag.NewRecord = true;
            }

            return View(model);
        }   

        [HttpPost]
        public ActionResult Update(ReferenceFirmViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            var result = Guid.TryParse(model.ReferenceId.Decrypt(), out Guid id);
            if(!result)
            {
                ShowMessage(Messages.KayitBulunamadi, MessageType.warning);
                return View(model);
            }

            byte[] image = null;
            if (model.FileReference != null && model.FileReference.ContentLength > 0)
            {
                if (model.FileReference.ContentType.ToLower() != "image/jpg" &&
                model.FileReference.ContentType.ToLower() != "image/jpeg" &&
                model.FileReference.ContentType.ToLower() != "image/pjpeg" &&
                model.FileReference.ContentType.ToLower() != "image/gif" &&
                model.FileReference.ContentType.ToLower() != "image/x-png" &&
                model.FileReference.ContentType.ToLower() != "image/png")
                {
                    ShowMessage(Messages.GecersizResimUzantisi, MessageType.warning);
                    return View(model);
                }

                Stream fs = model.FileReference.InputStream;
                BinaryReader br = new BinaryReader(fs);
                image = br.ReadBytes((Int32)fs.Length);
            }

            id = _referenceFirmService.SaveReferenceFirm(new ReferenceFirm
            {
                ReferenceFirmId = id,
                LogoImage = image,
                Detail = model.Detail,
                Name = model.Name,
                WorkName = model.WorkName,
                InsertDate = model.InsertDate,
                State = Convert.ToByte(model.StateDesc)
            }, model.ImageDeleted);

            ShowMessage(Messages.KayitBasarili);
            return RedirectToAction("Update", new { id = id });
        }
    }
}