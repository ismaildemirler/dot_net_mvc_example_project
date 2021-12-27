using Domain.WebHelpers.Models.Shared;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domain.WebHelpers.Filters
{
    public class HandleAntiforgeryTokenErrorAttribute : HandleErrorAttribute
    {
        private static bool IsAjax(ControllerContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }
        public override void OnException(ExceptionContext filterContext)
        {

            //Todo henüz bitmedi
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
            filterContext.ExceptionHandled = true;
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(new { action = "Login", controller = "Account" }));
        }
    }
}
