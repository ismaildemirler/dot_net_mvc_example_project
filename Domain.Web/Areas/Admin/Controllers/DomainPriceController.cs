using Domain.Business.Abstract.Parameter;
using Domain.Entity.ComplexType;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Parameter;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Web.Areas.Admin.Model.Parameter;
using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [DomainAuthorize(Roles = "Admin")]
    public class DomainPriceController : BaseController
    {
        private readonly IParameterService _parameterService;

        public DomainPriceController(IParameterService parameterService)
        {
            _parameterService = parameterService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetDomainPriceList(DtParameterModel dtParameterModel)
        {
            List<DomainPriceComplexType> list = _parameterService.GetDomainPriceList();

            var data = list.Select(s => new DomainPriceViewModel()
            {
                RefreshPrice = s.RefreshPrice,
                RefreshPriceWithDiscount = s.RefreshPriceWithDiscount,
                RegisterPrice = s.RegisterPrice,
                RegisterPriceWithDiscount = s.RegisterPriceWithDiscount,
                TransferPrice = s.TransferPrice,
                TransferPriceWithDiscount = s.TransferPriceWithDiscount,
                TLDTypeDescription = s.TLDTypeDescription,
                Buttons = RowButtons(s)
            });

            return Json(new
            {
                data = data,
                draw = dtParameterModel.Draw,
                recordsTotal = list.Count,
                recordsFiltered = list.Count,
            });
        }

        private string RowButtons(DomainPriceComplexType entity)
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' success-callback='GetDomainPriceList' data-url='{Url.Action("Delete", new { id = entity.DomainPriceId.Encrypt() })}'> <i class='fa fa-times'> </i> Sil </a>");
            sb.AppendLine($"<a class='dropdown-item update text-primary' success-callback='GetDomainPriceList' data-url='{Url.Action("_Update", new { id = entity.DomainPriceId.Encrypt() })}'> <i class='fa fa-edit'> </i> Güncelle </a>");

            return sb.ToString();
        }

        [HttpGet]
        public ActionResult _Update(string id = "")
        {
            DomainPriceViewModel model = new DomainPriceViewModel();

            if (!string.IsNullOrEmpty(id))
            {
                DomainPrice response = _parameterService.GetDomainPrice(new RequestDomainPrice { DomainPriceId = id.DecryptToInt32() });
                model = new DomainPriceViewModel
                {
                    DomainPriceId = response.DomainPriceId.Encrypt(),
                    RefreshPrice = response.RefreshPrice,
                    RefreshPriceWithDiscount = response.RefreshPriceWithDiscount,
                    RegisterPrice = response.RegisterPrice,
                    RegisterPriceWithDiscount = response.RegisterPriceWithDiscount,
                    TLDTypeId = response.TLDTypeId,
                    TransferPrice = response.TransferPrice,
                    TransferPriceWithDiscount = response.TransferPriceWithDiscount
                };
            }
            else
            {
                model.DomainPriceId = 0.Encrypt();
            }

            model.TLDTypes = _parameterService.GetAllTLDTypes().Select(s => new SelectListItem { Value = s.TLDTypeId.ToString(), Text = s.Description }).ToList();
            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Update(DomainPriceViewModel model)
        {
            _parameterService.UpdateDomainPrice(new RequestDomainPrice
            {
                DomainPrice = new DomainPrice
                {
                    DomainPriceId = model.DomainPriceId.DecryptToInt32(),
                    RefreshPrice = model.RefreshPrice,
                    RefreshPriceWithDiscount = model.RefreshPriceWithDiscount,
                    RegisterPrice = model.RegisterPrice,
                    RegisterPriceWithDiscount = model.RegisterPriceWithDiscount,
                    TLDTypeId = model.TLDTypeId,
                    TransferPrice = model.TransferPrice,
                    TransferPriceWithDiscount = model.TransferPriceWithDiscount
                }
            });
            return ShowMessageJSON(Messages.KayitBasarili, AjaxResultState.Success);
        }

        [HttpPost]
        public ActionResult Delete(string id)
        {
            _parameterService.DeleteDomainPrice(new RequestDomainPrice
            {
                DomainPriceId = id.DecryptToInt32()
            });
            return ShowMessageJSON(Messages.SilmeBasarili, AjaxResultState.Success);
        }
    }
}