using AutoMapper;
using Domain.Business.Abstract.Brand;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Brand;
using Domain.Entity.Enum;
using Domain.Entity.ViewModels.UserBrandApplication;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Infrastructure;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class BrandController : DomainBaseController
    {
        private readonly IMapper _mapper;
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService, IMapper mapper)
        {
            _mapper = mapper;
            _brandService = brandService;
        }

        public ActionResult Index()
        {
            return View(BrandApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BrandApplication(
            string BrandClassesArray,
            CustomerApplication customerApplication,
            BrandApplicationDetailViewModel brandApplicationDetail)
        {
            var code = Domain.Infrastructure.Utilities.Common.Utilities.RandomString(20, true);
            BrandApplicationViewModelSession.CustomerApplication.AnonymousApplicationCode = code;
            BrandApplicationViewModelSession.CustomerApplication.Email
                = customerApplication.Email;
            BrandApplicationViewModelSession.CustomerApplication.PhoneNumber
                = customerApplication.PhoneNumber;
            BrandApplicationViewModelSession.CustomerApplication.CustomerName
                = customerApplication.CustomerName;

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
                    int item = Convert.ToInt32(brandClassItem);
                    var brandSubClasses = CacheManager.GetAllBrandSubClasses
                        .Where(w => w.BrandClassesId == item);

                    foreach (var brandSubClass in brandSubClasses)
                    {
                        var customerAppBrandClass = new CustomerApplicationBrandClasses
                        {
                            BrandSubClassesId = brandSubClass.BrandSubClassesId,
                            BrandApplicationDetailId = guid
                        };
                        customerAppBrandClasses.Add(customerAppBrandClass);

                        BrandApplicationViewModelSession.BrandClassesArray 
                            += brandSubClass.BrandSubClassesIdentification + ",";
                    }
                }
            }

            BrandApplicationViewModelSession.CustomerApplication.ApplicationTypeId = (int)EnumApplicationType.Marka_Basvurusu;

            var brandApplicationDetailFromView
                = _mapper.Map<BrandApplicationDetail>(brandApplicationDetail);

            var response = _brandService.CreateBrandApplication(new RequestBrandApplication
            {
                BrandApplicationDetailIdForInsert = guid,
                CustomerApplicationBrandClasses = customerAppBrandClasses,
                BrandApplicationDetail = brandApplicationDetailFromView,
                CustomerApplication = BrandApplicationViewModelSession.CustomerApplication
            });

            BrandApplicationViewModelSession.CustomerApplication = response.CustomerApplication;
            BrandApplicationViewModelSession.BrandApplicationDetail
                = _mapper.Map<BrandApplicationDetailViewModel>(response.BrandApplicationDetail);

            if (!CurrentUser.IsAuthenticated)
            {
                RedirectUrlSession = Url.Action("ApplicationStep1", "Brand", new { area = "User", brandApplicationDetailId = BrandApplicationViewModelSession.BrandApplicationDetail.BrandApplicationDetailId });
                return RedirectToAction("Result", "Brand", new { area = "" });
            }
            else
            {
                return RedirectToAction("ApplicationStep1", "Brand", new { area = "User" });
            }
        }

        public ActionResult Result()
        {
            return View(BrandApplicationViewModelSession);
        }
    }
}