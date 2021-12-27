using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.MYS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace Domain.Infrastructure.MYS
{
    public class ActionAuthorizeAttribute : FilterAttribute, IAuthorizationFilter
    {
        public List<Guid> AuthIds { get; set; }
        public ActionAuthorizeAttribute(params string[] _authId)
        {
            AuthIds = new List<Guid>();
            foreach (var authId in _authId)
            {
                if (!Guid.TryParse(authId, out Guid result))
                    throw new AuthenticationException("Geçersiz Yetki(Auth) Parametresi");

                AuthIds.Add(result);
            }
        }
        public void OnAuthorization(AuthorizationContext filterContext)
        {
//#if !DEBUG
            var noSecurity = filterContext.ActionDescriptor.GetCustomAttributes(typeof(NoAuthRequireAttribute), true);
            if (noSecurity.Length == 0)
            {
                var isAuth = true; // AuthorizationManager.HasAnyAuth(AuthIds);
                if (!isAuth)
                {
                    var noAuthorizeUrl = ConfigurationManager.AppSettings["noAuthorizeUrl"];
                    //var client = MYSWcfProxy<INotAuthorizeLogService>.CreateChannel();
                    //var authorizeLog = new NotAuthorizeLog
                    //{
                    //    AppId = Utilities.GetAppId(),
                    //    ViewOrAuthId = AuthIds.FirstOrDefault(),
                    //    Ip = Utilities.GetIp(),
                    //    UserId = CurrentUser.Identity.UserId,
                    //    TcKimlikNo = CurrentUser.Identity.TcKimlikNo,
                    //    TimeStamp = DateTime.Now
                    //};
                    //client.NotAuthorizeLogSave(authorizeLog);

                    if (noAuthorizeUrl == null)
                        throw new AuthenticationException("Bu işleme Yetkiniz Bulunmamaktadır...");
                    if (HttpContext.Current.Request.IsAjaxRequest())
                    {
                        throw  new BusinessException("Bu işleme Yetkiniz Bulunmamaktadır...");
                        //filterContext.Result = new ContentResult
                        //{
                        //    Content = JsonConvert.SerializeObject(new AjaxResult
                        //    {
                        //        State = 0,
                        //        Message = "Bu işleme Yetkiniz Bulunmamaktadır...",
                        //        RedirectUrl = noAuthorizeUrl
                        //    }),
                        //    ContentType = "application/json"
                        //};
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult(VirtualPathUtility.ToAbsolute(noAuthorizeUrl));
                        //filterContext.Result = new RedirectToRouteResult("Default",
                        //    new RouteValueDictionary { { "controller", noAuthorizeUrl.Split('/')[0] }, { "action", noAuthorizeUrl.Split('/')[1] } });
                    }
                }
            }
//#endif
        }
    }
}
