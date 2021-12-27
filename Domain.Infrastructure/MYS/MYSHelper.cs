using Domain.Infrastructure.CrossCuttingConcerns.Security;
using Domain.Infrastructure.ElasticSearch;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging;
using Newtonsoft.Json;
using System;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace Domain.Infrastructure.MYS
{
    public class MYSHelper
    {
        public void UserLogout()
        {
            FormsAuthentication.SignOut();
            HttpContext.Current.Session.RemoveAll();
            HttpContext.Current.Session.Clear();
            HttpContext.Current.Session.Abandon();

            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie1);

            SessionStateSection sessionStateSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
            HttpCookie cookie2 = new HttpCookie(sessionStateSection.CookieName, "");
            cookie2.Expires = DateTime.Now.AddYears(-1);
            HttpContext.Current.Response.Cookies.Add(cookie2);

            HttpContext.Current.User = null;
            Thread.CurrentPrincipal = null;
        }

        public void UserLogin(DomainMYSIdentity identity)
        {
            var prin = new GenericPrincipal(identity, new string[] { });
            Thread.CurrentPrincipal = prin;
            HttpContext.Current.User = prin;

            var srUserData = JsonConvert.SerializeObject(identity, Formatting.Indented);

            var authTicket = new FormsAuthenticationTicket(1, identity.UserName, DateTime.Now, DateTime.Now.AddMinutes(30), false, srUserData);
            var encTicket = FormsAuthentication.Encrypt(authTicket);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        #region Logging
        public void ErrorLogSave(RequestErrorLog request)
        {
            var errorLogEntity = new ErrorLog {
                ErrorMessage = request.ErrorMessage,
                Ip = request.Ip,
                ServerName = Environment.MachineName,
                ErrorDetail = request.ErrorDetail,
                ErrorTime = request.ErrorTime,
                MethodName = request.MethodName,
                UserId = request.UserId ?? 0
            };
            
            ElasticProvider<ErrorLog> elasticProvider = new ElasticProvider<ErrorLog>();
            elasticProvider.Insert(errorLogEntity);
        }
        #endregion
    }
}
