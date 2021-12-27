using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Routing;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class UrlHelpers
    {
        public static string RedirectUrl(this UrlHelper urlHelper, string controller, string action = "")
        {
            return $"/{controller}{(string.IsNullOrEmpty(action) ? string.Empty : $"/{action}")}";
        }

        public static string EncryptedAction(string actionName, string controllerName, object routeValues = null, string areaName = null)
        {
            var queryString = string.Empty;
            if (routeValues != null)
            {
                var d = new RouteValueDictionary(routeValues);
                for (var i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }
            var actionText = new StringBuilder();

            if (!string.IsNullOrEmpty(areaName))
                actionText.Append("/" + areaName);

            if (controllerName != string.Empty)
                actionText.Append("/" + controllerName);

            if (actionName != "Index")
                actionText.Append("/" + actionName);

            if (queryString != string.Empty)
                actionText.Append("?q=" + queryString.Encrypt());

            return actionText.ToString();
        }

        public static string EncryptedAction(string url, object routeValues = null)
        {
            var queryString = string.Empty;
            if (routeValues != null)
            {
                var d = new RouteValueDictionary();
                if (routeValues is RouteValueDictionary)
                    d = routeValues as RouteValueDictionary;
                else
                    d = new RouteValueDictionary(routeValues);

                for (var i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (queryString != string.Empty)
                url += "?q=" + queryString.Encrypt();

            return url;
        }
    }
}