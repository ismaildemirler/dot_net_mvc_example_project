using Domain.WebHelpers.Infrastructre;
using Domain.WebHelpers.Models.Shared;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace Domain.WebHelpers.Filters
{
    public class CustomHandleErrorAttribute : HandleErrorAttribute
    {
        private readonly ErrorLogger _errorLogger;
        public CustomHandleErrorAttribute()
        {
            _errorLogger = new ErrorLogger();
        }

        public static List<string> DeniedErrorList => new List<string>
        {
            "Cannot redirect after HTTP headers have been sent.",
            "HTTP üst bilgileri gönderildikten sonra, yeniden yönlendirilemiyor.",
            "Server cannot append header after HTTP headers have been sent."
        };

        private static bool IsAjax(ControllerContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public override void OnException(ExceptionContext filterContext)
        {
            var controllerName = (string)filterContext.RouteData.Values["controller"];
            var actionName = (string)filterContext.RouteData.Values["action"];

            if (!filterContext.Exception.Message.Contains("|NotificationException") &&
                !(filterContext.Exception is HttpAntiForgeryException))
            {
                if (!DeniedErrorList.Select(s=>s.ToUpper(new CultureInfo("tr-TR"))).Contains(filterContext.Exception.Message.ToUpper(new CultureInfo("tr-TR"))))
                    _errorLogger.HataLogla(filterContext.Exception, controllerName + "." + actionName);
            }

            else //NotificationException içinde exception varsa logla
            {
                if (filterContext.Exception.InnerException != null)
                {
                    if (!DeniedErrorList.Select(s => s.ToUpper(new CultureInfo("tr-TR"))).Contains(filterContext.Exception.Message.ToUpper(new CultureInfo("tr-TR"))))
                        _errorLogger.HataLogla(filterContext.Exception.InnerException, controllerName + "." + actionName);
                }
            }

            if (IsAjax(filterContext))
            {
                filterContext.ExceptionHandled = true;
                filterContext.HttpContext.Response.Clear();
                filterContext.HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
                filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

                var errorMessage = filterContext.Exception.Message.Replace("|NotificationException", "");

                filterContext.Result = new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new AjaxResult { Result = AjaxResultState.Fail, Message = errorMessage, RedirectUrl = "" }),
                    ContentType = "application/json"
                };
            }
            else
            {
#if DEBUG
                base.OnException(filterContext);
#else
                //404 sayfaları (partiallar dahil değil)
                if (!filterContext.IsChildAction && filterContext.Exception is System.InvalidOperationException)
                {
                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.StatusCode = 404;
                    filterContext.Result = new HttpNotFoundResult();
                }

                else //diğer tüm hatalar 
                {
                    //base.OnException(filterContext);
                    var model = new HandleErrorInfo(filterContext.Exception, controllerName, actionName);

                    filterContext.Result = new ViewResult
                    {
                        ViewName = View,
                        MasterName = Master,
                        ViewData = new ViewDataDictionary<HandleErrorInfo>(model),
                        TempData = filterContext.Controller.TempData
                    };

                    filterContext.ExceptionHandled = true;
                    filterContext.HttpContext.Response.Clear();
#if !DEBUG
                    filterContext.HttpContext.Response.StatusCode = 500;
#endif
                    filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                }
#endif
            }
        }
    }
}