using Domain.Business.Abstract.Log;
using Domain.Business.Abstract.Service;
using Domain.Infrastructure.Entities;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Models.Log;
using Domain.WebHelpers.Infrastructre;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    [AllowAnonymous]
    public class HomeController : BaseController
    {
        private readonly ILogService _logService;
        private readonly IServicesService _servicesService;

        public HomeController(ILogService logService, IServicesService servicesService)
        {
            _logService = logService;
            _servicesService = servicesService;
        }

        public ActionResult Index()
        { 
            return View();
        }


        public ActionResult BrandQuickApply(string brandName, List<int> brandClassIds)
        {
            Session.Add("BrandName", brandName);
            Session.Add("BrandClassIds", brandClassIds);

            if (CurrentUser.IsAuthenticated && CurrentUser.Identity != null)
            {
                return RedirectToAction("ApplicationStep1", "Brand", new { area="User" });
            }
            else
            {
                ShowMessage("Lütfen başvuru işlemine devam etmek için giriş yapınız.", MessageType.success);
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpPost]
        public ActionResult GetErrorLogList(DtParameterModel dtParameterModel)
        {
            List<ErrorLogViewModel> list = new List<ErrorLogViewModel>();
            var response = _logService.GetLast100ElasticErrorLogList();
            if (response != null)
            {
                list = response.Logs.Select(s => new ErrorLogViewModel()
                {
                    ElasticId = s.ElasticId,
                    ErrorDetail = s.ErrorDetail,
                    ErrorMessage = s.ErrorMessage,
                    ErrorTime = s.ErrorTime,
                    Ip = s.Ip,
                    MethodName = s.MethodName,
                    ServerName = s.ServerName,
                    UserId = s.UserId
                }).ToList();
            }
            return Json(new { data = list, draw = dtParameterModel.Draw, recordsTotal = response == null ? 0 : list.Count, recordsFiltered = response == null ? 0 : list.Count });
        }

        public ActionResult Log()
        {
            return View();
        }
    }
}