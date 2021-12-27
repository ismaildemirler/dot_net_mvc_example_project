using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class MultiSelectHelper
    {
        public static MvcHtmlString MultiSelect<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, List<SelectListItem> lst, object htmlAttributes = null)
        {
            var sb = new StringBuilder();
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!attrs.ContainsKey("class"))
            {
                attrs.Add("class", "form-control select2-multiple");
            }

            attrs.Add("multiple", "multiple");
            attrs.Add("data-toggle", "select2");

            sb.AppendLine(htmlHelper.ListBoxFor(expression, lst, attrs).ToString());

            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString MultiSelect(this HtmlHelper htmlHelper,
            string name, List<SelectListItem> lst, object htmlAttributes = null)
        {
            var sb = new StringBuilder();
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!attrs.ContainsKey("class"))
            {
                attrs.Add("class", "form-control select2-multiple");
            }

            attrs.Add("multiple", "multiple");
            attrs.Add("data-toggle", "select2");

            sb.AppendLine(htmlHelper.ListBox(name, lst, attrs).ToString());

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}