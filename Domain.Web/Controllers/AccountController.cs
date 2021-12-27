using Domain.Business.Abstract.Common;
using Domain.Entity.Concrete;
using Domain.Entity.Container.Request.SystemUsers;
using Domain.Entity.Enum;
using Domain.Entity.Util;
using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Infrastructure.MYS;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Web.Models.Account;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Web.Mvc;

namespace Domain.Web.Controllers
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

            var user = _systemUserService.GetSystemUser(new RequestSystemUser() { EMail = model.EMail, Password = model.Password, UserType = EnumSystemUserType.Customer });
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
                UserTypeId = (byte)EnumSystemUserType.Customer
            };

            MYSHelper helper = new MYSHelper();
            helper.UserLogin(identity);

            if (RedirectUrlSession != null && !string.IsNullOrWhiteSpace(RedirectUrlSession))
            {
                return Redirect(RedirectUrlSession);
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Logout()
        {
            MYSHelper helper = new MYSHelper();
            helper.UserLogout();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult Register()
        {
            if(CurrentUser.IsAuthenticated)
            {
                ShowMessage(Messages.KayitliKullaniciGirisYapmis, MessageType.warning);
                return RedirectToAction("Index" ,"Home");
            }

            return View(new RegisterViewModel());
        }
        
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ShowMessage(Messages.AlanlariKontrolEt, MessageType.warning);
                return View(model);
            }

            var systemUserId = _systemUserService.CreateSystemUser(new RequestSystemUser()
            {
                SystemUserInfo = new SystemUser()
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Password = model.Password,
                    StateId = (byte)EnumSystemUserState.Active,
                    CreationDate = DateTime.Now,
                    PhoneNumber = model.PhoneNumber,
                    Gender = 1,
                    UserTypeId = (byte)EnumSystemUserType.Customer
                }
            });

            var identity = new DomainMYSIdentity
            {
                Id = CurrentUser.UserLoginGuid,
                UserId = systemUserId,
                UserName = model.Email,
                Ip = Utilities.GetIp(),
                EMail = model.Email,
                LastName = model.LastName,
                FirstName = model.FirstName,
                IsAuthenticated = true,
                Name = Guid.NewGuid().ToString(),
                AuthenticationType = "Forms"
            };

            MYSHelper helper = new MYSHelper();
            helper.UserLogin(identity);

            ShowMessage(Messages.KullaniciKaydiOlusturuldu);
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            if (CurrentUser.IsAuthenticated)
            {
                ShowMessage(Messages.KayitliKullaniciGirisYapmis, MessageType.warning);
                return RedirectToAction("Index", "Home");
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

            var user = _systemUserService.GetSystemUser(new RequestSystemUser() { EMail = model.EMail, Password = "", UserType = (byte)EnumSystemUserType.Customer });
            if (user != null)
            {
                //MailHelper.SendMail();
            }

            ShowMessage(Messages.YeniSifreGonderildi, MessageType.success);
            return RedirectToAction("Index", "Home");
        }
    }
}