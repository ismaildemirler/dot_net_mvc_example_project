using System.Web.Mvc;
using System;
using Domain.Entity.Concrete;
using Domain.Business.Abstract.IndustrialDesign;
using Domain.Entity.Container.Request.IndustrialDesign;
//using Domain.Entity.ViewModels.UserIndustrialDesignApplication;
using AutoMapper;
using Domain.Web.Areas.User.Models.IndustrialDesign;
using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using System.Linq;
using Domain.Entity.ComplexType;
using System.Text;
using Domain.Business.Abstract.Customer;
using Domain.Entity.Container.Request.Customer;
using Domain.Web.Infrastructure;
using Domain.Entity.ViewModels.UserIndustrialDesignApplication;
using Domain.Entity.Enum;

namespace Domain.Web.Areas.User.Controllers
{
    public class IndustrialDesignController : DomainBaseController
    {
        private readonly IMapper _mapper;
        private readonly ICustomerService _customerService;
        private readonly IIndustrialDesignService _industrialDesignService;

        public IndustrialDesignController(
            IMapper mapper,
            ICustomerService customerService,
            IIndustrialDesignService industrialDesignService)
        {
            _mapper = mapper;
            _customerService = customerService;
            _industrialDesignService = industrialDesignService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetIndustrialDesignApplicationPagedList(
            DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);
            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = CurrentUser.Identity.UserId.ToString(),
                ID = "ca.CustomerId",
                IsNot = false
            });

            var lst = _industrialDesignService
                .GetPagedListSearchedIndustrialDesignApplicationCT(dtParameterModel, filters);

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.IndustrialDesignApplicationDetailId,
                    p.Title,
                    Buttons = ApplicationButtons(p),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(IndustrialDesignApplicationComplexType entity)
        {
            var sb = new StringBuilder();
            
            sb.Append($" <a class='dropdown-item text-primary' href='{Url.Action("IndustrialDesignApplicationDetail", "IndustrialDesign", new { area = "User", industrialDesignApplicationDetailId = entity.IndustrialDesignApplicationDetailId })}'><i class=\"fa fa-eye\"></i>&nbsp; Detay</a>");
            
            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("AddOrUpdateIndustrialDesignApplication", "IndustrialDesign", new { area = "User", industrialDesignApplicationDetailId = entity.IndustrialDesignApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp; Güncelle </a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeleteIndustrialDesign", "IndustrialDesign", new { area = "User", industrialDesignApplicationDetailId = entity.IndustrialDesignApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        public ActionResult NewApplication()
        {
            IndustrialDesignApplicationViewModelSession = null;
            return RedirectToAction("AddOrUpdateIndustrialDesignApplication");
        }

        public IndustrialDesignApplicationViewModel IndustrialDesignApplicationViewModelSession
        {
            get
            {
                if (SessionManager.Get("IndustrialDesignApplicationViewModelSession") == null)
                    SessionManager.Set("IndustrialDesignApplicationViewModelSession",
                        new IndustrialDesignApplicationViewModel());
                return (IndustrialDesignApplicationViewModel)SessionManager.Get("IndustrialDesignApplicationViewModelSession");

            }
            set => SessionManager.Set("IndustrialDesignApplicationViewModelSession", value);
        }

        [HttpGet]
        public ActionResult AddOrUpdateIndustrialDesignApplication(Guid? industrialDesignApplicationDetailId)
        {
            if (industrialDesignApplicationDetailId != null
                && industrialDesignApplicationDetailId != Guid.Empty)
            {
                var industrialDesignFromDb = _industrialDesignService
                    .GetIndustrialDesignApplicationDetailByRequest(
                        new RequestIndustrialDesignApplicationDetail
                        {
                            IndustrialDesignApplicationDetailId = industrialDesignApplicationDetailId.Value
                        })?.IndustrialDesignApplicationDetail;

                IndustrialDesignApplicationViewModelSession.IndustrialDesignApplicationDetail
                    = _mapper.Map<IndustrialDesignApplicationDetailViewModel>(industrialDesignFromDb);

                if (IndustrialDesignApplicationViewModelSession.IndustrialDesignApplicationDetail != null)
                {
                    IndustrialDesignApplicationViewModelSession.CustomerApplication
                        = _customerService.GetCustomerApplicationByRequest(
                            new RequestCustomerApplication
                            {
                                CustomerApplicationId = IndustrialDesignApplicationViewModelSession
                                    .IndustrialDesignApplicationDetail.CustomerApplicationId
                            })?.CustomerApplication;
                }
            }

            return View(IndustrialDesignApplicationViewModelSession);
        }

        [HttpPost]
        public ActionResult AddOrUpdateIndustrialDesignApplication(
            IndustrialDesignApplicationDetailViewModel industrialDesignApplicationDetail)
        {
            var industrialDesignApplicationDetailFromView
                = _mapper.Map<IndustrialDesignApplicationDetail>(industrialDesignApplicationDetail);

            if (AttachmentSession == null || AttachmentSession.AttachmentId == Guid.Empty || AttachmentSession.FormId != (int)EnumFormType.Endustriyel_Tasarim_Fotograf_Dosyasi)
            {
                ShowMessage("Endüstriyel Tasarım Başvuru işlemi için fotoğraf yüklemelisiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return View(new IndustrialDesignApplicationViewModel() { 
                    IndustrialDesignApplicationDetail = industrialDesignApplicationDetail
                });
            }

            var response = _industrialDesignService.CreateIndustrialDesignApplication(
                new RequestIndustrialDesignApplication
                {
                    Attachment = AttachmentSession,
                    IndustrialDesignApplicationDetail = industrialDesignApplicationDetailFromView,
                    CustomerApplication = IndustrialDesignApplicationViewModelSession.CustomerApplication
                });

            IndustrialDesignApplicationViewModelSession.CustomerApplication = response?.CustomerApplication;
            IndustrialDesignApplicationViewModelSession.IndustrialDesignApplicationDetail
                = _mapper.Map<IndustrialDesignApplicationDetailViewModel>(response?.IndustrialDesignApplicationDetail);

            return RedirectToAction("IndustrialDesignApplicationDetail");
        }

        public ActionResult IndustrialDesignApplicationDetail(Guid? industrialDesignApplicationDetailId)
        {
            if (industrialDesignApplicationDetailId != null
                && industrialDesignApplicationDetailId != Guid.Empty)
            {
                var industrialDesignFromDb = _industrialDesignService
                    .GetIndustrialDesignApplicationDetailByRequest(
                        new RequestIndustrialDesignApplicationDetail
                        {
                            IndustrialDesignApplicationDetailId = industrialDesignApplicationDetailId.Value
                        })?.IndustrialDesignApplicationDetail;

                IndustrialDesignApplicationViewModelSession.IndustrialDesignApplicationDetail
                    = _mapper.Map<IndustrialDesignApplicationDetailViewModel>(industrialDesignFromDb);
            }

            return View(IndustrialDesignApplicationViewModelSession);
        }

        [HttpPost]
        public ActionResult DeleteIndustrialDesign(Guid industrialDesignApplicationDetailId)
        {
            bool result = false;
            _industrialDesignService.DeleteIndustrialDesign(
                new RequestIndustrialDesignApplicationDetail
                {
                    IndustrialDesignApplicationDetailId = industrialDesignApplicationDetailId
                });
            result = true;

            return Json(result);
        }
    }
}