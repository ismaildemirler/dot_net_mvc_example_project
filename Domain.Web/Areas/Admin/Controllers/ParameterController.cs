using Domain.Business.Abstract.Parameter;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Parameter;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Areas.Admin.Model.Parameter;
using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class ParameterController : BaseController
    {
        private readonly IParameterService _parameterService;

        public ParameterController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        #region TLD

        public ActionResult TLDTypes()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetTLDList(DtParameterModel dtParameterModel)
        {
            var list = _parameterService.GetAllTLDTypes();
            var data = list.Select(s => new TLDTypeViewModel()
            {
                Description = s.Description,
                Buttons = TLDRowButtons(s)
            });

            return Json(new
            {
                data = data,
                draw = dtParameterModel.Draw,
                recordsTotal = list.Count,
                recordsFiltered = list.Count,
            });
        }

        private string TLDRowButtons(PrmTLDType entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' success-callback='GetTLDList' data-url='{Url.Action("DeleteTLDType", new { id = entity.TLDTypeId.Encrypt() })}'> <i class='fa fa-times'> </i> Sil </a>");
            sb.AppendLine($"<a class='dropdown-item update text-primary' success-callback='GetTLDList' data-url='{Url.Action("_UpdateTLDType", new { id = entity.TLDTypeId.Encrypt() })}'> <i class='fa fa-edit'> </i> Güncelle </a>");

            return sb.ToString();
        }

        [HttpGet]
        public ActionResult _UpdateTLDType(string id = "")
        {
            TLDTypeViewModel model = new TLDTypeViewModel();
            
            if (!string.IsNullOrEmpty(id))
            {
                PrmTLDType response = _parameterService.GetTLDType(new RequestTLDType { TLDTypeId = id.DecryptToInt32() });
                model = new TLDTypeViewModel
                {
                    TLDTypeId = response.TLDTypeId.Encrypt(),
                    Description = response.Description
                };
            }
            else
            {
                model.TLDTypeId = 0.Encrypt();
            }

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult UpdateTLDType(TLDTypeViewModel model)
        {
            _parameterService.UpdateTDLType(new RequestTLDType
            {
                TLDTypeId = model.TLDTypeId.DecryptToInt32(),
                Description = model.Description
            });
            return ShowMessageJSON(Messages.KayitBasarili, AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult DeleteTLDType(string id)
        {
            _parameterService.DeleteTLDType(new RequestTLDType { 
                TLDTypeId = id.DecryptToInt32()
            });
            return ShowMessageJSON(Messages.SilmeBasarili, AjaxResultState.Success);
        }

        #endregion
    }
}