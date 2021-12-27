using Domain.Business.Abstract.Beneficiary;
using Domain.Business.Abstract.Brand;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Brand;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Domain.Entity.Container.Request.Beneficiary;
using Domain.Web.Areas.User.Models.Brand;
using Domain.Infrastructure.Entities;
using Domain.Entity.ComplexType;
using System.Text;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Business.Abstract.Contact;
using Domain.Entity.Container.Request.Contact;
using Newtonsoft.Json;
using Domain.Web.Areas.User.Dto.Brand;
using Domain.Business.Abstract.Customer;
using Domain.Entity.Container.Request.Customer;
using Domain.Entity.Enum;
using Domain.Infrastructure.CrossCuttingConcerns.Security.Web;
using AutoMapper;
using Domain.Entity.ViewModels.UserBrandApplication;
using Domain.Entity.ViewModels.UserBrandForWatching;
using Domain.WebHelpers.Models.Shared;
using Domain.Business.Abstract.Attachment;
using Domain.Web.Infrastructure;

namespace Domain.Web.Areas.User.Controllers
{
    [DomainAuthorize(Roles = "Customer")]
    public class BrandController : DomainBaseController
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;
        private readonly IContactService _contactService;
        private readonly IAttachmentService _attachmentService;
        private readonly ICustomerService _customerService;
        private readonly IBeneficiaryService _beneficiaryService;

        public BrandController(
            IMapper mapper,
            IBrandService brandService,
            IAttachmentService attachmentService,
            IBeneficiaryService beneficiaryService,
            IContactService contactService,
            ICustomerService customerService)
        {
            _mapper = mapper;
            _contactService = contactService;
            _brandService = brandService;
            _attachmentService = attachmentService;
            _beneficiaryService = beneficiaryService;
            _customerService = customerService;
        }

        #region BrandApplication
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewApplication()
        {
            BrandApplicationViewModelSession = new BrandApplicationViewModel();
            return RedirectToAction("ApplicationStep1");
        }

        [HttpPost]
        public ActionResult GetBrandApplicationPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);
            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = CurrentUser.Identity.UserId.ToString(),
                ID = "ca.CustomerId",
                IsNot = false
            });

            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = ((int)EnumApplicationType.Marka_Basvurusu).ToString(),
                ID = "pat.ApplicationTypeId",
                IsNot = false
            });

            var lst = _brandService.GetPagedListSearchedBrandApplicationCT(dtParameterModel, filters);

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.BrandApplicationDetailId,
                    p.BrandName,
                    p.ApplicationTypeDescription,
                    BeneficiaryName = p.FirmName ?? "Hak Sahibi Eklenmemiş / Yarım Kalmış Başvuru",
                    CityTownName = p.CityName != null ? p.CityName + "/" + p.TownName : "İletişim Bilgisi Eklenmemiş / Yarım Kalmış Başvuru",
                    Buttons = ApplicationButtons(p, p.FirmName == null || p.CityName == null),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string ApplicationButtons(BrandApplicationComplexType entity, bool notCompleted)
        {
            var sb = new StringBuilder();
            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("ApplicationStep1", "Brand", new { area = "User", brandApplicationDetailId = entity.BrandApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp;" + (notCompleted ? "Başvuruya Devam Et" : "Güncelle") + "</a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeleteBrand", "Brand", new { area = "User", brandApplicationDetailId = entity.BrandApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        public ActionResult ApplicationStep1(Guid? brandApplicationDetailId)
        {
            if (brandApplicationDetailId != null && brandApplicationDetailId != Guid.Empty)
            {
                var brandApplicationDetailFromDb = _brandService.GetBrandApplicationDetailByRequest(
                    new RequestBrandApplicationDetail
                    {
                        BrandApplicationDetailId = brandApplicationDetailId.Value
                    })?.BrandApplicationDetail;

                BrandApplicationViewModelSession.BrandApplicationDetail
                    = _mapper.Map<BrandApplicationDetailViewModel>(
                        brandApplicationDetailFromDb);

                if (BrandApplicationViewModelSession.BrandApplicationDetail != null)
                {
                    BrandApplicationViewModelSession.CustomerApplication = _customerService.GetCustomerApplicationByRequest(
                    new RequestCustomerApplication
                    {
                        CustomerApplicationId = BrandApplicationViewModelSession.BrandApplicationDetail.CustomerApplicationId
                    })?.CustomerApplication;

                    var beneficiaryFromDb = _beneficiaryService.GetBeneficiaryByRequest(new RequestBeneficiary
                    {
                        BrandApplicationDetailId = BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId
                    })?.Beneficiary ?? new Beneficiary();

                    BrandApplicationViewModelSession.Beneficiary
                        = _mapper.Map<BeneficiaryViewModel>(beneficiaryFromDb);

                    if (BrandApplicationViewModelSession.BrandApplicationDetail.ContactId != null)
                    {
                        var contactFromDb = _contactService.GetContactByRequest(new RequestContact
                        {
                            ContactId = BrandApplicationViewModelSession.BrandApplicationDetail.ContactId.Value
                        })?.Contact;

                        BrandApplicationViewModelSession.Contact
                            = _mapper.Map<ContactViewModel>(contactFromDb);
                    }

                    var allContactsFromDb = _contactService.GetAllContacts()?.Contacts;
                    BrandApplicationViewModelSession.Contacts
                        = _mapper.Map<List<ContactViewModel>>(allContactsFromDb);

                    BrandApplicationViewModelSession.BrandClassesArray = GetBrandClassIdAray(
                        BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId);
                }
            }
            else if (!BrandApplicationViewModelSession.Contacts.Any())
            {
                var allContactsFromDb = _contactService.GetAllContacts()?.Contacts;
                BrandApplicationViewModelSession.Contacts
                    = _mapper.Map<List<ContactViewModel>>(allContactsFromDb);
            }

            return View(BrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationStep1(
            BrandApplicationDetailViewModel brandApplicationDetail,
            string BrandClassesArray)
        {
            if (brandApplicationDetail.BrandApplicationTypeId == null)
            {
                ShowMessage("Lütfen 'Şahıs Adına' veya 'Şirket Adına' seçeneklerinden birini seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("ApplicationStep1", "Brand", new { area = "User" });
            }

            var guid = Guid.NewGuid();
            if (brandApplicationDetail.BrandApplicationDetailId != null &&
                brandApplicationDetail.BrandApplicationDetailId != Guid.Empty)
            {
                guid = brandApplicationDetail.BrandApplicationDetailId;
            }

            var brandClasses = BrandClassesArray.Split(',');
            var customerAppBrandClasses = new List<CustomerApplicationBrandClasses>();
            foreach (var brandClassItem in brandClasses)
            {
                if (!string.IsNullOrWhiteSpace(brandClassItem))
                {
                    var brandSubClass = CacheManager.GetAllBrandSubClasses
                        .Find(w => w.BrandSubClassesIdentification == brandClassItem);

                    var customerAppBrandClass = new CustomerApplicationBrandClasses
                    {
                        BrandSubClassesId = brandSubClass.BrandSubClassesId,
                        BrandApplicationDetailId = guid
                    };
                    customerAppBrandClasses.Add(customerAppBrandClass);
                }
            }

            BrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId = (int)EnumApplicationType.Marka_Basvurusu;

            var brandApplicationDetailFromView
                = _mapper.Map<BrandApplicationDetail>(
                        brandApplicationDetail
                    );

            var response = _brandService.CreateBrandApplication(new RequestBrandApplication
            {
                BrandApplicationDetailIdForInsert = guid,
                CustomerApplicationBrandClasses = customerAppBrandClasses,
                BrandApplicationDetail = brandApplicationDetailFromView,
                CustomerApplication = BrandApplicationViewModelSession.CustomerApplication
            });

            var brandApplicationDetailFromDb
                = _mapper.Map<BrandApplicationDetailViewModel>(
                         response?.BrandApplicationDetail
                    );

            BrandApplicationViewModelSession.CustomerApplication = response?.CustomerApplication;
            BrandApplicationViewModelSession.BrandApplicationDetail = brandApplicationDetailFromDb;
            BrandApplicationViewModelSession.BrandClassesArray = BrandClassesArray;

            return RedirectToAction("ApplicationStep2");
        }

        public ActionResult ApplicationStep2()
        {
            if (BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId == null
                || BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId == Guid.Empty)
                return View("ApplicationStep2");

            return View(BrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationStep2(BeneficiaryViewModel beneficiary)
        {
            beneficiary.BrandApplicationDetailId
                = BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId;

            var beneficiaryFromView
                = _mapper.Map<Beneficiary>(beneficiary);
            var beneficiaryFromDb = _beneficiaryService.CreateBeneficiaryRegistry(
                new RequestBeneficiaryRegistry
                {
                    Beneficiary = beneficiaryFromView
                })?.Beneficiary;

            BrandApplicationViewModelSession.Beneficiary
                = _mapper.Map<BeneficiaryViewModel>(beneficiaryFromDb);

            return RedirectToAction("ApplicationStep3");
        }

        public ActionResult ApplicationStep3()
        {
            if (BrandApplicationViewModelSession.Beneficiary.BeneficiaryId == null
                || BrandApplicationViewModelSession.Beneficiary.BeneficiaryId == Guid.Empty)
                return View("ApplicationStep2");

            BrandApplicationViewModelSession.Contact = new ContactViewModel();
            return View(BrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ApplicationStep3(ContactViewModel contact)
        {
            var contactFromView
                = _mapper.Map<Contact>(contact);

            var brandApplicationFromView
                = _mapper.Map<BrandApplicationDetail>(
                        BrandApplicationViewModelSession.BrandApplicationDetail
                    );

            var response = _beneficiaryService.CreateBeneficiaryContactRegistry(
                new RequestBeneficiaryContactRegistry
                {
                    BrandApplicationDetail = brandApplicationFromView,
                    Contact = contactFromView
                })?.Contact;

            BrandApplicationViewModelSession.Contact =
                _mapper.Map<ContactViewModel>(response);

            return RedirectToAction("ApplicationStep4");
        }

        public ActionResult ApplicationStep4()
        {
            if (BrandApplicationViewModelSession.Contact.ContactId == null
                || BrandApplicationViewModelSession.Contact.ContactId == Guid.Empty)
                return View("ApplicationStep3");

            return View(BrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinishApplication()
        {
            var brandAttachment = _attachmentService.GetAttachmentsWithoutData(new AttachmentComplexType
            {
                RelatedEntityId = BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId,
                RelatedEntity = EnumRelatedEntity.BrandApplicationDetail
            });

            if (!brandAttachment.Any())
            {
                ShowMessage("Lütfen marka dosyasını yükleyiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("ApplicationStep4", "Brand", new { area = "User" });
            }

            BrandApplicationViewModelSession = null;
            return RedirectToAction("Index");
        }
        #endregion

        #region BrandWatchingApplication

        public ActionResult NewWatchingApplication()
        {
            BrandWatchingApplicationViewModelSession = new BrandWatchingApplicationViewModel();
            return RedirectToAction("WatchingApplicationIndex");
        }

        public BrandWatchingApplicationViewModel BrandWatchingApplicationViewModelSession
        {
            get
            {
                if (SessionManager.Get("BrandWatchingApplicationViewModelSession") == null)
                    SessionManager.Set("BrandWatchingApplicationViewModelSession", new BrandWatchingApplicationViewModel());
                return (BrandWatchingApplicationViewModel)SessionManager.Get("BrandWatchingApplicationViewModelSession");

            }
            set => SessionManager.Set("BrandWatchingApplicationViewModelSession", value);
        }

        public ActionResult WatchingApplicationList()
        {
            return View();
        }

        public ActionResult WatchingApplicationIndex()
        {
            return View();
        }

        public ActionResult WatchingApplicationStep1(Guid? brandWatchingApplicationDetailId)
        {
            if (brandWatchingApplicationDetailId != null && brandWatchingApplicationDetailId != Guid.Empty)
            {
                var brandWatchingApplicationDetailFromDb = _brandService.GetBrandWatchApplicationDetailByRequest(
                    new RequestBrandWatchingApplicationDetail
                    {
                        BrandWatchingApplicationDetailId = brandWatchingApplicationDetailId.Value
                    })?.BrandWatchingApplicationDetail;

                BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail
                    = _mapper.Map<BrandWatchingApplicationDetailViewModel>(
                            brandWatchingApplicationDetailFromDb
                        );

                if (BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail != null)
                {
                    BrandWatchingApplicationViewModelSession.CustomerApplication = _customerService.GetCustomerApplicationByRequest(
                        new RequestCustomerApplication
                        {
                            CustomerApplicationId = BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail.CustomerApplicationId
                        })?.CustomerApplication;

                    var brandForWatchingsFromDb = _brandService.GetBrandForWatchingByRequest(
                        new RequestBrandForWatching
                        {
                            BrandWatchApplicationDetailId =
                                BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId
                        })?.BrandForWatchings;

                    BrandWatchingApplicationViewModelSession.BrandForWatchings
                        = _mapper.Map<List<BrandForWatchingViewModel>>(
                                brandForWatchingsFromDb
                            );
                }
            }

            return View(BrandWatchingApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WatchingApplicationStep1(
            BrandWatchingApplicationDetailViewModel brandWatchingApplicationDetail)
        {
            var brandWatchingApplicationDetailFromView
                = _mapper.Map<BrandWatchingApplicationDetail>(
                        brandWatchingApplicationDetail
                    );

            var response = _brandService.CreateBrandWatchApplication(new RequestBrandWatchingApplication
            {
                BrandWatchingApplicationDetail = brandWatchingApplicationDetailFromView,
            });

            var brandWatchingApplicationDetailFromDb
                = _mapper.Map<BrandWatchingApplicationDetailViewModel>(
                        response.BrandWatchingApplicationDetail
                    );

            BrandWatchingApplicationViewModelSession.CustomerApplication = response.CustomerApplication;
            BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail
                = brandWatchingApplicationDetailFromDb;

            return RedirectToAction("WatchingApplicationStep2");
        }

        public ActionResult WatchingApplicationStep2()
        {
            if (BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId == null
                || BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId == Guid.Empty)
                return RedirectToAction("WatchingApplicationStep1");

            BrandWatchingApplicationViewModelSession.BrandForWatching.BrandWatchingApplicationDetailId =
                BrandWatchingApplicationViewModelSession.BrandWatchingApplicationDetail.BrandWatchingApplicationDetailId;
            return View(BrandWatchingApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WatchingApplicationStep2(BrandForWatchingViewModel brandForWatching)
        {
            var brandForWatchingFromView
                = _mapper.Map<BrandForWatching>(
                        brandForWatching
                    );

            var addedBrandForWatching = _brandService.CreateBrandForWatchingRegistry(
                    new RequestBrandForWatchingRegistration
                    {
                        BrandForWatching = brandForWatchingFromView
                    })?.BrandForWatching;

            var brandForWatchingFromDb
                = _mapper.Map<BrandForWatchingViewModel>(
                        addedBrandForWatching
                    );

            BrandWatchingApplicationViewModelSession.BrandForWatchings.Add(brandForWatchingFromDb);
            return RedirectToAction("WatchingApplicationStep2");
        }

        public ActionResult FinishWatchingApplication(BrandForWatchingViewModel brandForWatching)
        {
            BrandWatchingApplicationViewModelSession = null;
            return RedirectToAction("WatchingApplicationList");
        }

        [HttpPost]
        public ActionResult AddBrandsComingFromApi(string brandForWatchingList)
        {
            bool result = true;
            var brandForWatchings = JsonConvert.DeserializeObject<BrandForWatchingDto>(brandForWatchingList);
            foreach (var item in brandForWatchings.BrandForWatchingList)
            {
                var insertedBrand = _brandService.CreateBrandForWatchingRegistry(new RequestBrandForWatchingRegistration
                {
                    BrandForWatching = item
                })?.BrandForWatching;

                var insertedBrandFromDb
                    = _mapper.Map<BrandForWatchingViewModel>(insertedBrand);

                BrandWatchingApplicationViewModelSession.BrandForWatchings.Add(insertedBrandFromDb);
            }

            ShowMessage("Kayıt işlemi yapıldı");
            return Json(new { success = result });
        }

        public ActionResult DeleteBrandForWatchingItem(Guid brandForWatchingId)
        {
            bool result = false;
            _brandService.DeleteBrandForWatching(new RequestBrandForWatching
            {
                BrandForWatchingId = brandForWatchingId
            });
            result = true;

            BrandWatchingApplicationViewModelSession.BrandForWatchings.Remove(
                BrandWatchingApplicationViewModelSession.BrandForWatchings
                    .FirstOrDefault(f => f.BrandForWatchingId == brandForWatchingId));

            return Json(result);
        }

        public ActionResult DeleteBrandForWatchingDetail(Guid brandWatchingApplicationDetailId)
        {
            bool result = false;
            _brandService.DeleteBrandForWatchingApplication(new RequestBrandForWatching
            {
                BrandWatchApplicationDetailId = brandWatchingApplicationDetailId
            });
            result = true;

            return Json(result);
        }

        [HttpPost]
        public ActionResult GetBrandForWatchingApplicationPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);
            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = CurrentUser.Identity.UserId.ToString(),
                ID = "ca.CustomerId",
                IsNot = false
            });

            var lst = _brandService.GetPagedListSearchedBrandForWatchingApplicationCT(dtParameterModel, filters);

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.BrandWatchingApplicationDetailId,
                    p.BrandName,
                    p.BrandFirmName,
                    p.ClassesToWatch,
                    p.RegistryNumber,
                    p.ApplicationFirmName,
                    p.IdentityNumber,
                    CityTown = p.TownName + "/" + p.CityName,
                    p.Phone,
                    p.Address,
                    Buttons = WatchingApplicationButtons(p),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [HttpPost]
        public ActionResult GetNotCompletedBrandForWatchingApplicationPagedList(DtParameterModel dtParameterModel)
        {
            var lst = _brandService.GetNotCompletedPagedListSearchedBrandForWatchingApplicationCT(dtParameterModel,
                new RequestBrandWatchingApplicationDetail
                {
                    CustomerId = CurrentUser.Identity.UserId
                });

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.FirmName,
                    p.IdentityNumber,
                    CityTown = CacheManager.GetAllCities.FirstOrDefault(f => f.CityId == p.CityId)?.CityName
                        + "/" + CacheManager.GetAllTowns.FirstOrDefault(f => f.TownId == p.TownId)?.TownName,
                    p.Phone,
                    p.Address,
                    Buttons = NotCompletedWatchingApplicationButtons(p),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string WatchingApplicationButtons(BrandForWatchingApplicationComplexType entity)
        {
            var sb = new StringBuilder();

            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("WatchingApplicationStep1", "Brand", new { area = "User", brandWatchingApplicationDetailId = entity.BrandWatchingApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp; Güncelle </a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeleteBrandForWatchingDetail", "Brand", new { area = "User", brandWatchingApplicationDetailId = entity.BrandWatchingApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        [NonAction]
        private string NotCompletedWatchingApplicationButtons(BrandWatchingApplicationDetail entity)
        {
            var sb = new StringBuilder();

            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("WatchingApplicationStep1", "Brand", new { area = "User", brandWatchingApplicationDetailId = entity.BrandWatchingApplicationDetailId })}'><i class=\"fa fa-edit\"></i>&nbsp; Başvuruya Devam Et </a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeleteBrandForWatchingDetail", "Brand", new { area = "User", brandWatchingApplicationDetailId = entity.BrandWatchingApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }
        #endregion

        #region OtherBrandApplication
        public OtherBrandApplicationViewModel OtherBrandApplicationViewModelSession
        {
            get
            {
                if (SessionManager.Get("OtherBrandApplicationViewModelSession") == null)
                    SessionManager.Set("OtherBrandApplicationViewModelSession", new OtherBrandApplicationViewModel());
                return (OtherBrandApplicationViewModel)SessionManager.Get("OtherBrandApplicationViewModelSession");

            }
            set => SessionManager.Set("OtherBrandApplicationViewModelSession", value);
        }

        public ActionResult NewOtherApplication()
        {
            OtherBrandApplicationViewModelSession = new OtherBrandApplicationViewModel();
            return RedirectToAction("OtherApplicationStep1");
        }

        public ActionResult OtherApplicationList()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetOtherBrandApplicationPagedList(DtParameterModel dtParameterModel, string searchFilters = "")
        {
            var filters = SearchFilterHelper.GetSearchFilter(searchFilters);
            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = CurrentUser.Identity.UserId.ToString(),
                ID = "ca.CustomerId",
                IsNot = false
            });

            filters.Add(new SearchFilter
            {
                FilterType = FilterType.Numeric,
                Value = ((int)EnumApplicationType.Marka_Basvurusu).ToString(),
                ID = "pat.ApplicationTypeId",
                IsNot = true
            });

            var lst = _brandService.GetPagedListSearchedBrandApplicationCT(dtParameterModel, filters);

            return Json(new
            {
                data = lst.Items.Select(p => new
                {
                    p.BrandApplicationDetailId,
                    p.BrandName,
                    p.ApplicationTypeDescription,
                    BeneficiaryName = p.FirmName == null && p.ApplicationTypeId != (int)EnumApplicationType.Itiraza_Karsi_Gorus ? "Hak Sahibi Eklenmemiş / Yarım Kalmış Başvuru" : p.FirmName,
                    CityTownName = p.CityName == null && p.ApplicationTypeId != (int)EnumApplicationType.Itiraza_Karsi_Gorus ? "İletişim Bilgisi Eklenmemiş / Yarım Kalmış Başvuru" : p.CityName + "/" + p.TownName,
                    Buttons = OtherBrandApplicationButtons(p, (p.FirmName == null || p.CityName == null) && p.ApplicationTypeId != (int)EnumApplicationType.Itiraza_Karsi_Gorus),
                }),
                draw = dtParameterModel.Draw,
                recordsTotal = lst.TotalCount,
                recordsFiltered = lst.TotalCount
            });
        }

        [NonAction]
        private string OtherBrandApplicationButtons(BrandApplicationComplexType entity, bool notCompleted)
        {
            var sb = new StringBuilder();

            sb.Append($" <a class='dropdown-item grid-btn-movement text-primary' href='{Url.Action("OtherApplicationStep1", "Brand", new { area = "User", customerApplicationId = entity.CustomerApplicationId })}'><i class=\"fa fa-edit\"></i>&nbsp;" + (notCompleted ? "Başvuruya Devam Et" : "Güncelle") + "</a>");

            sb.AppendLine($"<a class='dropdown-item grid-btn-delete text-danger' data-url='{Url.Action("DeleteBrand", "Brand", new { area = "User", brandApplicationDetailId = entity.BrandApplicationDetailId })}'> <i class='fa fa-times'> </i> Sil </a>");

            return sb.ToString();
        }

        public ActionResult OtherApplicationStep1(Guid? customerApplicationId)
        {
            if (customerApplicationId != null && customerApplicationId != Guid.Empty)
            {
                OtherBrandApplicationViewModelSession.CustomerApplication
                    = _customerService.GetCustomerApplicationByRequest(
                        new RequestCustomerApplication
                        {
                            CustomerApplicationId = customerApplicationId.Value
                        })?.CustomerApplication;

                if (OtherBrandApplicationViewModelSession.CustomerApplication != null)
                {
                    var brandApplicationDetailFromDb = _brandService.GetBrandApplicationDetailByRequest(
                        new RequestBrandApplicationDetail
                        {
                            CustomerApplicationId = customerApplicationId.Value
                        })?.BrandApplicationDetail;

                    OtherBrandApplicationViewModelSession.BrandApplicationDetail
                        = _mapper.Map<BrandApplicationDetailOtherViewModel>(brandApplicationDetailFromDb);

                    if (OtherBrandApplicationViewModelSession.BrandApplicationDetail != null)
                    {
                        OtherBrandApplicationViewModelSession.BrandClassesArray = GetBrandClassIdAray(
                            OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId);

                        var beneficiaryFromDb = _beneficiaryService.GetBeneficiaryByRequest(
                                new RequestBeneficiary
                                {
                                    BrandApplicationDetailId = OtherBrandApplicationViewModelSession
                                        .BrandApplicationDetail.BrandApplicationDetailId
                                })?.Beneficiary;

                        OtherBrandApplicationViewModelSession.Beneficiary
                            = _mapper.Map<BeneficiaryOtherViewModel>(beneficiaryFromDb);

                        if (OtherBrandApplicationViewModelSession.Beneficiary == null)
                        {
                            OtherBrandApplicationViewModelSession.Beneficiary = new BeneficiaryOtherViewModel();
                        }

                        if (OtherBrandApplicationViewModelSession.BrandApplicationDetail.ContactId != null)
                        {
                            var contactFromDb = _contactService.GetContactByRequest(
                                new RequestContact
                                {
                                    ContactId = OtherBrandApplicationViewModelSession
                                        .BrandApplicationDetail.ContactId.Value
                                })?.Contact;

                            OtherBrandApplicationViewModelSession.Contact
                                = _mapper.Map<ContactOtherViewModel>(contactFromDb);
                        }


                        var allContacts = _contactService.GetAllContacts()?.Contacts;
                        OtherBrandApplicationViewModelSession.Contacts
                            = _mapper.Map<List<ContactOtherViewModel>>(allContacts);
                    }
                    else
                    {
                        OtherBrandApplicationViewModelSession.BrandApplicationDetail = new BrandApplicationDetailOtherViewModel();
                    }
                }
            }
            else if (!OtherBrandApplicationViewModelSession.Contacts.Any())
            {
                var allContacts = _contactService.GetAllContacts()?.Contacts;
                OtherBrandApplicationViewModelSession.Contacts
                    = _mapper.Map<List<ContactOtherViewModel>>(allContacts);
            }
            return View(OtherBrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherApplicationStep1(CustomerApplication customerApplication)
        {
            OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                = customerApplication.ApplicationTypeId;

            return RedirectToAction("OtherApplicationStep2");
        }

        public ActionResult OtherApplicationStep2()
        {
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId == 0)
                return RedirectToAction("OtherApplicationStep1");

            return View(OtherBrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherApplicationStep2(
            BrandApplicationDetailOtherViewModel brandApplicationDetail,
            string BrandClassesArray)
        {
            #region Validation Control
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                != (int)EnumApplicationType.Veraset_Intikal_Basvurusu &&
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                != (int)EnumApplicationType.Itiraza_Karsi_Gorus)
            {
                if (brandApplicationDetail.BrandApplicationTypeId == 0 ||
                    brandApplicationDetail.BrandApplicationTypeId == null)
                {
                    ShowMessage("Lütfen firma veya şahıs seçeneklerinden birisini seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep2", "Brand", new { area = "User" });
                }
            }
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Marka_Yenileme_Cezali_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Marka_Yenileme_Normal_Basvurusu)
            {
                if (brandApplicationDetail.BrandApplicationDate == null
                    || brandApplicationDetail.BrandApplicationDate == DateTime.MinValue)
                {
                    ShowMessage("Lütfen başvuru tarihinizi seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep2", "Brand", new { area = "User" });
                }
            }
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                != (int)EnumApplicationType.Itiraza_Karsi_Gorus)
            {
                if (string.IsNullOrWhiteSpace(BrandClassesArray))
                {
                    ShowMessage("Lütfen sınıf seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep2", "Brand", new { area = "User" });
                }
            }
            if (string.IsNullOrWhiteSpace(brandApplicationDetail.RegistryNumber))
            {
                ShowMessage("Lütfen başvuru/tescil numaranızı yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep2", "Brand", new { area = "User" });
            }
            #endregion

            var guid = Guid.NewGuid();
            if (brandApplicationDetail.BrandApplicationDetailId != null &&
                brandApplicationDetail.BrandApplicationDetailId != Guid.Empty)
            {
                guid = brandApplicationDetail.BrandApplicationDetailId;
            }

            var customerAppBrandClasses = new List<CustomerApplicationBrandClasses>();
            if (BrandClassesArray != null)
            {
                var brandClasses = BrandClassesArray.Split(',');
                foreach (var brandClassItem in brandClasses)
                {
                    if (!string.IsNullOrWhiteSpace(brandClassItem))
                    {
                        var brandSubClass = CacheManager.GetAllBrandSubClasses
                            .Find(w => w.BrandSubClassesIdentification == brandClassItem);

                        var customerAppBrandClass = new CustomerApplicationBrandClasses
                        {
                            BrandSubClassesId = brandSubClass.BrandSubClassesId,
                            BrandApplicationDetailId = guid
                        };
                        customerAppBrandClasses.Add(customerAppBrandClass);
                    }
                }
            }

            var brandApplicationDetailFromView
                = _mapper.Map<BrandApplicationDetail>(brandApplicationDetail);

            var response = _brandService.CreateBrandApplication(new RequestBrandApplication
            {
                BrandApplicationDetailIdForInsert = guid,
                CustomerApplicationBrandClasses = customerAppBrandClasses,
                BrandApplicationDetail = brandApplicationDetailFromView,
                CustomerApplication = OtherBrandApplicationViewModelSession.CustomerApplication
            });

            var brandApplicationDetailFromDb
                = _mapper.Map<BrandApplicationDetailOtherViewModel>(response?.BrandApplicationDetail);

            OtherBrandApplicationViewModelSession.CustomerApplication = response?.CustomerApplication;
            OtherBrandApplicationViewModelSession.BrandApplicationDetail = brandApplicationDetailFromDb;
            OtherBrandApplicationViewModelSession.BrandClassesArray = BrandClassesArray ?? "";

            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Itiraza_Karsi_Gorus)
            {
                return RedirectToAction("OtherApplicationList");
            }
            else
            {
                return RedirectToAction("OtherApplicationStep3");
            }
        }

        public ActionResult OtherApplicationStep3()
        {
            if (OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId == null
                || OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId == Guid.Empty)
                return RedirectToAction("OtherApplicationStep2");

            return View(OtherBrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherApplicationStep3(
            BeneficiaryOtherViewModel beneficiary)
        {
            OtherBrandApplicationViewModelSession.Beneficiary = beneficiary;

            #region Validation Control
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Devir_Islemi_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Veraset_Intikal_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Lisans_Islemi_Basvurusu)
            {
                if (string.IsNullOrWhiteSpace(beneficiary.PreviousFirmName))
                {
                    return RedirectJSON("/User/Brand/OtherApplicationStep3",
                       "Lütfen firma unvanını yazınız!",
                       AjaxResultState.Fail);


                }
            }
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                 == (int)EnumApplicationType.Marka_Yayin_Kararina_Itiraz_Islemi)
            {
                if (string.IsNullOrWhiteSpace(beneficiary.ExtraBrandName))
                {
                    ShowMessage("Lütfen markanızın adını yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
                if (string.IsNullOrWhiteSpace(beneficiary.ExtraBrandRegistryNumber))
                {
                    ShowMessage("Lütfen markanızın başvuru/tescil numarasını yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
            }
            if (OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationTypeId
                    == (int)EnumBrandApplicationType.Firma)
            {
                if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Nevi_Degisikligi_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Unvan_Degisikligi_Basvurusu)
                {
                    if (string.IsNullOrWhiteSpace(beneficiary.PreviousFirmName))
                    {
                        ShowMessage("Lütfen eski şirket unvanını yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                        return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                    }
                    if (string.IsNullOrWhiteSpace(beneficiary.FirmName))
                    {
                        ShowMessage("Lütfen yeni şirket unvanını yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                        return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                    }
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(beneficiary.FirmName))
                    {
                        ShowMessage("Lütfen şirket unvanını yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                        return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                    }
                }
            }
            else if (OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationTypeId
                    == (int)EnumBrandApplicationType.Sahis ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                    == (int)EnumApplicationType.Veraset_Intikal_Basvurusu)
            {
                if (string.IsNullOrWhiteSpace(beneficiary.FirmName))
                {
                    ShowMessage("Lütfen ad ve soyadınızı yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
                if (string.IsNullOrWhiteSpace(beneficiary.TCNumber))
                {
                    ShowMessage("Lütfen T.C. kimlik numaranızı yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
                if (string.IsNullOrWhiteSpace(beneficiary.BirthPlace))
                {
                    ShowMessage("Lütfen doğum yerinizi yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
                if (beneficiary.BirthDate == null || beneficiary.BirthDate == DateTime.MinValue)
                {
                    ShowMessage("Lütfen doğum tarihinizi seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
            }
            if (OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Adres_Degisikligi_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Nevi_Degisikligi_Basvurusu ||
                OtherBrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId
                == (int)EnumApplicationType.Unvan_Degisikligi_Basvurusu)
            {
                if (string.IsNullOrWhiteSpace(beneficiary.PreviousOfficialAddress))
                {
                    ShowMessage("Lütfen eski adresinizi yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
                if (string.IsNullOrWhiteSpace(beneficiary.Address))
                {
                    ShowMessage("Lütfen yeni adresinizi yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(beneficiary.Address))
                {
                    ShowMessage("Lütfen resmi adresinizi yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                    return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
                }
            }
            if (string.IsNullOrWhiteSpace(beneficiary.TaxOffice))
            {
                ShowMessage("Lütfen vergi dairenizi yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            if (string.IsNullOrWhiteSpace(beneficiary.TaxNumber))
            {
                ShowMessage("Lütfen vergi numaranızı yazınız!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            if (beneficiary.CityId == 0 || beneficiary.CityId == null)
            {
                ShowMessage("Lütfen ilinizi seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            if (beneficiary.TownId == 0 || beneficiary.TownId == null)
            {
                ShowMessage("Lütfen ilçenizi seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            if (string.IsNullOrWhiteSpace(beneficiary.PhoneNumber))
            {
                ShowMessage("Lütfen telefon numaranızı seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            if (string.IsNullOrWhiteSpace(beneficiary.Fax))
            {
                ShowMessage("Lütfen fax numaranızı seçiniz!", WebHelpers.Infrastructre.MessageType.warning);
                return RedirectToAction("OtherApplicationStep3", "Brand", new { area = "User" });
            }
            #endregion

            beneficiary.BrandApplicationDetailId
                = OtherBrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId;

            var beneficiaryFromView = _mapper.Map<Beneficiary>(beneficiary);
            var beneficiaryFromDb = _beneficiaryService.CreateBeneficiaryRegistry(
                new RequestBeneficiaryRegistry
                {
                    Beneficiary = beneficiaryFromView
                })?.Beneficiary;

            OtherBrandApplicationViewModelSession.Beneficiary
                = _mapper.Map<BeneficiaryOtherViewModel>(beneficiaryFromDb);
            return RedirectToAction("OtherApplicationStep4");
        }

        public ActionResult OtherApplicationStep4()
        {
            if (OtherBrandApplicationViewModelSession.Beneficiary.BeneficiaryId == null
                || OtherBrandApplicationViewModelSession.Beneficiary.BeneficiaryId == Guid.Empty)
                return RedirectToAction("OtherApplicationStep3");

            OtherBrandApplicationViewModelSession.Contact = new ContactOtherViewModel();
            return View(OtherBrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult OtherApplicationStep4(
            ContactOtherViewModel contact)
        {
            var brandApplicationFromView
                = _mapper.Map<BrandApplicationDetail>(
                        OtherBrandApplicationViewModelSession.BrandApplicationDetail
                    );

            var contactFromView = _mapper.Map<Contact>(
                        contact
                    );

            var response = _beneficiaryService.CreateBeneficiaryContactRegistry(
                new RequestBeneficiaryContactRegistry
                {
                    BrandApplicationDetail = brandApplicationFromView,
                    Contact = contactFromView
                });

            OtherBrandApplicationViewModelSession = null;

            return RedirectToAction("OtherApplicationList");
        }
        #endregion

        #region CommonFunctions

        public ActionResult GetContactInfos(string contactIdString)
        {
            Guid contactId = Guid.Parse(contactIdString);
            var contact = _contactService.GetContactByRequest(
                new RequestContact
                {
                    ContactId = contactId
                })?.Contact;

            var towns = CacheManager.GetAllTowns.Where(f => f.CityId == contact.CityId);

            return Json(new { contact = contact, towns = towns }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteBrand(Guid brandApplicationDetailId)
        {
            bool result = false;
            _brandService.DeleteBrand(new RequestBrandApplicationDetail
            {
                BrandApplicationDetailId = brandApplicationDetailId
            });
            result = true;

            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteContact(Guid contactId)
        {
            bool result = false;
            _contactService.DeleteContact(new RequestContact
            {
                ContactId = contactId,
                BrandApplicationDetail = BrandApplicationViewModelSession.BrandApplicationDetail
            });
            result = true;

            return Json(result);
        }

        [NonAction]
        private string GetBrandClassIdAray(Guid brandApplicationDetailId)
        {
            string brandClassesArray = "";
            var customerAppBrandSubClasses = _brandService.GetCustomerApplicationBrandSubClassesByRequest(
                        new RequestBrandApplicationDetail
                        {
                            BrandApplicationDetailId = brandApplicationDetailId
                        })?.CustomerApplicationBrandClasses;

            foreach (var customerAppBrandSubClass in customerAppBrandSubClasses)
            {
                var brandSubClass = CacheManager.GetAllBrandSubClasses
                    .FirstOrDefault(f => f.BrandSubClassesId == customerAppBrandSubClass.BrandSubClassesId);
                brandClassesArray += brandSubClass.BrandSubClassesIdentification + ",";
            }
            return brandClassesArray;
        }
        #endregion
    }
}