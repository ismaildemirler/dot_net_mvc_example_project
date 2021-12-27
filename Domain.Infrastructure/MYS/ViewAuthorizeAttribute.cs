using Domain.Infrastructure.MYS.Infrastructure;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Authentication;
using System.Web;
using System.Web.Mvc;

namespace Domain.Infrastructure.MYS
{
    public class ViewAuthorizeAttribute : AuthorizeAttribute
    {
        public List<Guid> ViewIds { get; set; }
        public ViewAuthorizeAttribute(params string[] _viewId)
        {
            ViewIds = new List<Guid>();
            foreach (var viewId in _viewId)
            {
                if (!Guid.TryParse(viewId, out Guid result))
                    throw new AuthenticationException("Geçersiz Yetki(View) Parametresi");

                ViewIds.Add(result);
            }
        }
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //#if !DEBUG

            var noSecurity = filterContext.Controller.GetType().GetCustomAttributes(typeof(NoAuthRequireAttribute), true);
            if (noSecurity.Length == 0)
            {
                var isAuth = true; //AuthorizationManager.HasAnyView(ViewIds);
                if (!isAuth)
                {
                    var noAuthorizeUrl = ConfigurationManager.AppSettings["noAuthorizeUrl"];
                    //var client = MYSWcfProxy<INotAuthorizeLogService>.CreateChannel();
                    //var authorizeLog = new NotAuthorizeLog
                    //{
                    //    AppId = Utilities.GetAppId(),
                    //    ViewOrAuthId = ViewIds.FirstOrDefault(),
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
                        filterContext.Result = new ContentResult
                        {
                            Content = JsonConvert.SerializeObject(new AjaxResult
                            {
                                State = 0,
                                Message = "Bu işleme Yetkiniz Bulunmamaktadır...",
                                RedirectUrl = noAuthorizeUrl
                            }),
                            ContentType = "application/json"
                        };
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
    public class NoAuthRequireAttribute : Attribute { }
    public class NoLoginAttribute : Attribute { }
}
