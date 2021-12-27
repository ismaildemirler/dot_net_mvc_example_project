using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Domain.WebHelpers.Filters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var decryptedParameters = new Dictionary<string, object>();
            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                var encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                var decrptedString = encryptedQueryString.Decrypt();
                var paramsArrs = decrptedString.Split('?');

                foreach (var t in paramsArrs)
                {
                    var paramArr = t.Split('=');
                    decryptedParameters.Add(paramArr[0], paramArr[1]);
                }
            }
            for (var i = 0; i < decryptedParameters.Count; i++)
            {
                filterContext.ActionParameters[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }
            base.OnActionExecuting(filterContext);
        }
    }

    public class EncryptedActionModelBinder<T> : IModelBinder
    {
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var decryptedParameters = new Dictionary<string, object>();
            if (HttpContext.Current.Request.QueryString.Get("q") != null)
            {
                var encryptedQueryString = HttpContext.Current.Request.QueryString.Get("q");
                var decrptedString = encryptedQueryString.Decrypt();
                var paramsArrs = decrptedString.Split('?');

                foreach (var t in paramsArrs)
                {
                    var paramArr = t.Split('=');
                    decryptedParameters.Add(paramArr[0], paramArr[1]);
                }
            }
            var type = typeof(T);

            for (var i = 0; i < decryptedParameters.Count; i++)
            {
                type.GetProperty(decryptedParameters.Keys.ElementAt(i)).SetValue(type, decryptedParameters.Values.ElementAt(i));
            }
            return type;
        }
    }
}