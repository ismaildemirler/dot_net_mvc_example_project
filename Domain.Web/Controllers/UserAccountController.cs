using Domain.Business.Abstract.Common;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Domain.Infrastructure.MYS;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Models.Account;
using Domain.Web.Models.UserAccount;
using Domain.WebHelpers.Infrastructre;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Domain.Web.Controllers
{
    //[Authorize]   
    [AllowAnonymous]
    public class UserAccountController : BaseController
    {
        private readonly ISystemUserService _systemUserService;

        public UserAccountController(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Update()
        {
            return View(new UserAccountInfoViewModel());
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(UserAccountInfoViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            ShowMessage("Kullanıcı bilgileriniz güncellenmiştir.", MessageType.success);
            return RedirectToAction("Index", "UserAccount");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }
            
            ShowMessage("Şifreniz güncellenmiştir.", MessageType.success);
            return RedirectToAction("Index", "UserAccount");
        }

        [HttpGet]
        public ActionResult Addresses()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAddress()
        {
            return RedirectToAction("Index", "Home");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult SaveAddress(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            var user = _systemUserService.GetSystemUser(new RequestSystemUser() { EMail = model.EMail, Password = "", UserType = (byte)EnumSystemUserType.Customer });
            if (user != null)
            {
                //MailHelper.SendMail();
            }

            ShowMessage("Yeni şifre oluşturularak mail adresinize gönderilmiştir.", MessageType.success);
            return RedirectToAction("Index", "Home");
        }
    }
}