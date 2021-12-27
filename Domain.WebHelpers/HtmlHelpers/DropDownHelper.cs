using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class DropDownHelper
    {
        public static MvcHtmlString MdDropDownListFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, int>> expression,
            SelectList lst,
            bool isDisabled = false, object htmlAttributes = null)
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (isDisabled)
                attrs.Add("disabled", "disabled");

            var sb = new StringBuilder();
            sb.Append(htmlHelper.DropDownListFor(expression, lst, attrs));
            return new MvcHtmlString(sb.ToString());
        }
    }
}
