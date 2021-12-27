using Domain.WebHelpers.Filters;
using System.Web.Mvc;

namespace Domain.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomHandleErrorAttribute
            {
                View = "~/Views/Error/Index.cshtml"
            });
            filters.Add(new NoCacheAttribute());
        }
    }
}
