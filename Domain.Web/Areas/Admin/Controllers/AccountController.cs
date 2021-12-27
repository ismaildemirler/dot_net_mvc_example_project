using Domain.Business.Abstract.Common;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Domain.Infrastructure.MYS;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Models.Account;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Web.Mvc;

namespace Domain.Web.Areas.Admin.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly ISystemUserService _systemUserService;

        public AccountController(ISystemUserService systemUserService)
        {
            _systemUserService = systemUserService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.KullaniciAdiSifreZorunlu, MessageType.warning);
                return View(model);
            }

            var user = _systemUserService.GetSystemUser(new RequestSystemUser() { EMail = model.EMail, Password = model.Password, UserType = EnumSystemUserType.Admin });
            if (user == null)
            {
                ShowMessage(Messages.KullaniciAdiSifreKontrolEt, MessageType.warning);
                return View(model);
            }

            var identity = new DomainMYSIdentity
            {
                Id = CurrentUser.UserLoginGuid,
                UserId = user.SystemUserId,
                UserName = user.Email,
                Ip = Utilities.GetIp(),
                EMail = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName,
                IsAuthenticated = true,
                Name = Guid.NewGuid().ToString(),
                AuthenticationType = "Forms",
                UserTypeId = (byte)EnumSystemUserType.Admin
            };

            MYSHelper helper = new MYSHelper();
            helper.UserLogin(identity);

            return RedirectToAction("Index", "Home", new { area="Admin" });
        }

        public ActionResult Logout()
        {
            MYSHelper helper = new MYSHelper();
            helper.UserLogout();
            return RedirectToAction("Index", "Home", new { area="" });
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            if (CurrentUser.IsAuthenticated)
            {
                ShowMessage(Messages.KayitliKullaniciGirisYapmis, MessageType.warning);
                return RedirectToAction("Index", "Home", new { area = "Admin" });
            }

            return View(new ForgotPasswordViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult ForgotPassword(ForgotPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            var user = _systemUserService.GetSystemUser(new RequestSystemUser() { EMail = model.EMail, Password = "", UserType = EnumSystemUserType.Admin });
            if (user != null)
            {
                //MailHelper.SendMail();
            }

            ShowMessage(Messages.YeniSifreGonderildi, MessageType.success);
            return RedirectToAction("Index", "Home", new { area="" });
        }
    }
}