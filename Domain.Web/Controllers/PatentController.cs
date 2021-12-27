using AutoMapper;
using Domain.Business.Abstract.Beneficiary;
using Domain.Business.Abstract.Patent;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.Patent;
using Domain.Entity.ViewModels.UserPatentApplication;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Infrastructure;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class PatentController : DomainBaseController
    {
        private readonly IMapper _mapper;
        private readonly IPatentService _patentService;

        public PatentController(IPatentService patentService, IMapper mapper)
        {
            _mapper = mapper;
            _patentService = patentService;
        }

        public ActionResult Index()
        {
            return View(PatentApplicationViewModelSession);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatentApplication(
            CustomerApplication customerApplication,
            PatentApplicationDetailViewModel patentApplicationDetail)
        {
            var patentApplicationDetailFromView
                = _mapper.Map<PatentApplicationDetail>(patentApplicationDetail);

            var code = Domain.Infrastructure.Utilities.Common.Utilities.RandomString(20, true);
            customerApplication.AnonymousApplicationCode = code;
            var response = _patentService.CreatePatentApplication(new RequestPatentApplication
            {
                PatentApplicationDetail = patentApplicationDetailFromView,
                CustomerApplication = customerApplication
            });

            PatentApplicationViewModelSession.CustomerApplication = response.CustomerApplication;
            PatentApplicationViewModelSession.PatentApplicationDetail
                = _mapper.Map<PatentApplicationDetailViewModel>(response.PatentApplicationDetail);

            if (!CurrentUser.IsAuthenticated)
            {
                RedirectUrlSession = Url.Action("ApplicationStep1", "Patent", new { area = "User" });
                return RedirectToAction("Result", "Patent", new { area = "" });
            }
            else
            {
                return RedirectToAction("ApplicationStep1", "Patent", new { area = "User" });
            }
        }

        public ActionResult Result()
        {
            return View(PatentApplicationViewModelSession);
        }
    }
}