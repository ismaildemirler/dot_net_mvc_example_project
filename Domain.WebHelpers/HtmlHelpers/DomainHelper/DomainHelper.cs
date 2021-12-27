using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.DomainHelper
{
    public static class Helpers
    {
        public static IDomain Domain(this HtmlHelper helper)
        {
            return new Domain(helper);
        }
        public static IDomain<TModel> DomainFor<TModel>(this HtmlHelper<TModel> helper) where TModel : class
        {
            return new Domain<TModel>(helper);
        }
    }
}
