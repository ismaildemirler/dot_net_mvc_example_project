using Domain.Business.Abstract.Beneficiary;
using Domain.Entity.Concrete;
using System;
using System.Linq;
using System.Web.Mvc;
using Domain.Entity.Container.Request.Beneficiary;
using Domain.Infrastructure.Entities;
using Domain.Entity.ComplexType;
using System.Text;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Business.Abstract.Contact;
using Domain.Business.Abstract.Customer;
using Domain.Entity.Container.Request.Customer;
using Domain.Entity.Enum;
using Domain.Business.Abstract.Patent;
using Domain.Web.Areas.User.Models.Patent;
using Domain.Entity.Container.Request.Patent;
using Domain.Business.Abstract.Attachment;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using AutoMapper;
using Domain.Entity.ViewModels.UserPatentApplication;
using Domain.WebHelpers.Models.Shared;
using Domain.Web.Infrastructure;

namespace Domain.Web.Areas.User.Controllers
{
    [DomainAuthorize(Roles = "Customer")]
    public class PatentController : DomainBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPatentService _patentService;
        private readonly IContactService _contactService;
        private readonly ICustomerService _customerService;
        private readonly IBeneficiaryService _beneficiaryService;
        private readonly IAttachmentService _attachmentService;

        public PatentController(
            IMapper mapper,
            IPatentService patentService,
            IBeneficiaryService beneficiaryService,
            IContactService contactService,
            ICustomerService customerService,
            IAttachmentService attachmentService)
        {
            _mapper = mapper;
            _contactService = contactService;
            _patentService = patentService;
            _beneficiaryService = beneficiaryService;
            _customerService = customerService;
            _attachmentService = attachmentService;
        }

        #region PatentApplication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewApplication()
        {
            PatentApplicationViewModelSession = new PatentApplicationViewModel();
            return RedirectToAction("ApplicationStep1");
        }

        [HttpPost]
        public ActionResult GetPatentApplicationPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);
            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = CurrentUser.Identity.UserId.ToString(),
                ID = "ca.CustomerId",
                IsNot = false
            });

            var lst = _patentService.GetPagedListSearchedPatentApplicationCT(dtParameterModel, filters);

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.PatentApplicationDetailId,
                    p.Title,
                    p.Summary,
                    BeneficiaryName = p.FirmName ?? "Hak Sahibi Eklenmemiş / Yarım Kalmış Başvuru",
                    p.TaxNumber,
                    p.PersonName,
                    TaxInfo = p.TaxOffice + "/" + p.TaxNumber,
                    Buttons = ApplicationButtons(p, p.FirmName == null || p.CityName == null),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(PatentApplicationComplexType entity, bool notCompleted)
        {
            var sb = new StringBuilder();
            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("ApplicationStep1", "Patent", new { area = "User", patentApplicationDetailId = entity.PatentApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp;" + (notCompleted ? "Başvuruya Devam Et" : "Güncelle") + "</a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeletePatent", "Patent", new { area = "User", patentApplicationDetailId = entity.PatentApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        public ActionResult ApplicationStep1(Guid? patentApplicationDetailId)
        {
            RedirectUrlSession = null;
            if (patentApplicationDetailId != null && patentApplicationDetailId != Guid.Empty)
            {
                var patentFromDb = _patentService.GetPatentApplicationDetailByRequest(
                    new RequestPatentApplicationDetail
                    {
                        PatentApplicationDetailId = patentApplicationDetailId.Value
                    })?.PatentApplicationDetail;

                PatentApplicationViewModelSession.PatentApplicationDetail
                    = _mapper.Map<PatentApplicationDetailViewModel>(patentFromDb);

                if (PatentApplicationViewModelSession.PatentApplicationDetail != null)
                {
                    PatentApplicationViewModelSession.CustomerApplication
                        = _customerService.GetCustomerApplicationByRequest(
                            new RequestCustomerApplication
                            {
                                CustomerApplicationId = PatentApplicationViewModelSession
                                    .PatentApplicationDetail.CustomerApplicationId
                            })?.CustomerApplication;


                    var beneficiaryFromDb = _beneficiaryService.GetBeneficiaryByRequest(
                          new RequestBeneficiary
                          {
                              PatentApplicationDetailId = PatentApplicationViewModelSession
                                  .PatentApplicationDetail.PatentApplicationDetailId
                          })?.Beneficiary ?? new Beneficiary();

                    PatentApplicationViewModelSession.Beneficiary
                        = _mapper.Map<BeneficiaryPatentViewModel>(beneficiaryFromDb);
                }
            }

            return View(PatentApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationStep1(
            PatentApplicationDetailViewModel patentApplicationDetail)
        {
            var patentApplicationDetailFromView
                = _mapper.Map<PatentApplicationDetail>(patentApplicationDetail);
            var response = _patentService.CreatePatentApplication(new RequestPatentApplication
            {
                PatentApplicationDetail = patentApplicationDetailFromView,
                CustomerApplication = PatentApplicationViewModelSession.CustomerApplication
            });

            PatentApplicationViewModelSession.CustomerApplication = response?.CustomerApplication;
            PatentApplicationViewModelSession.PatentApplicationDetail
                = _mapper.Map<PatentApplicationDetailViewModel>(response?.PatentApplicationDetail);

            return RedirectToAction("ApplicationStep2");
        }

        public ActionResult ApplicationStep2()
        {
            if (PatentApplicationViewModelSession.PatentApplicationDetail.PatentApplicationDetailId == null
                || PatentApplicationViewModelSession.PatentApplicationDetail.PatentApplicationDetailId == Guid.Empty)
                return View("ApplicationStep2");

            return View(PatentApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationStep2(BeneficiaryPatentViewModel beneficiary)
        {
            beneficiary.PatentApplicationDetailId
                = PatentApplicationViewModelSession.PatentApplicationDetail.PatentApplicationDetailId;

            var beneficiaryFromView = _mapper.Map<Beneficiary>(beneficiary);
            var beneficiaryFromDb = _beneficiaryService.CreateBeneficiaryRegistry(
                new RequestBeneficiaryRegistry
                {
                    Beneficiary = beneficiaryFromView
                })?.Beneficiary;

            PatentApplicationViewModelSession.Beneficiary
                = _mapper.Map<BeneficiaryPatentViewModel>(beneficiaryFromDb);
            return RedirectToAction("ApplicationStep3");
        }

        public ActionResult ApplicationStep3()
        {
            return View(PatentApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishApplication(Guid patentApplicationDetailId, 
            bool sendWithEmail)
        {
            var patentExplanationAttachment = _attachmentService.GetAttachmentsWithoutData(new AttachmentComplexType
            {
                RelatedEntityId = patentApplicationDetailId,
                RelatedEntity = EnumRelatedEntity.PatentApplicationDetail
            });

            if (patentExplanationAttachment.Count < 2 && !sendWithEmail)
            {
                ShowMessage("Lütfen tüm dosyaları yükleyiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("ApplicationStep3", "Patent", new { area = "User" });
            }

            var patentApplicationDetail = _patentService.GetPatentApplicationDetailByRequest(
                new RequestPatentApplicationDetail
                {
                    PatentApplicationDetailId = patentApplicationDetailId
                })?.PatentApplicationDetail;
            patentApplicationDetail.Mail = sendWithEmail;

            var response = _patentService.CreatePatentApplication(new RequestPatentApplication
            {
                PatentApplicationDetail = patentApplicationDetail,
                CustomerApplication = PatentApplicationViewModelSession.CustomerApplication
            });
            PatentApplicationViewModelSession = null;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult DeletePatent(Guid patentApplicationDetailId)
        {
            bool result = false;
            _patentService.DeletePatent(new RequestPatentApplicationDetail
            {
                PatentApplicationDetailId = patentApplicationDetailId
            });
            result = true;

            return Json(result);
        }

        #endregion
    }
}