using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class RadioButtonHelper
    {
        public enum RepeatDirection
        {
            Vertical,
            Horizontal
        }
        public static MvcHtmlString RadioButtonListFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, int>> expression,
            List<SelectListItem> lst,
            RepeatDirection direction = RepeatDirection.Horizontal)
        {
            var deleg = expression.Compile();

            var result = "";
            if (htmlHelper.ViewData.Model != null)
                result = deleg(htmlHelper.ViewData.Model).ToString();

            // get the field name
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            // get full field (template may have prefix)
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);

            var sb = new StringBuilder();

            foreach (var radioItem in lst)
            {
                sb.Append("<div class='md-radio'>");
                var itemId = Guid.NewGuid().ToString().Replace("-", "");
                sb.Append(htmlHelper.RadioButton(fullName, radioItem.Value, radioItem.Value == result, new { id = itemId }));
                sb.AppendFormat("<label for='{0}'>", itemId);

                sb.Append("<span></span>");
                sb.AppendFormat("<span class='{0}'></span>", "check");
                sb.AppendFormat("<span class='{0}'></span>", "box");
                sb.Append(radioItem.Text);

                sb.Append("</label>");
                sb.Append("</div>");
            }

            if (direction == RepeatDirection.Horizontal)
            {
                sb.Insert(0, "<div class='md-radio-inline'>");
                sb.Append("</div>");
            }
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString RadioButtonList<TModel>(this HtmlHelper<TModel> htmlHelper,
           string name, string selectedValue,
            List<SelectListItem> lst,
            RepeatDirection direction=RepeatDirection.Horizontal)
        {
            var sb = new StringBuilder();
            foreach (var radioItem in lst)
            {
                sb.Append("<div class='md-radio'>");
                var itemId = Guid.NewGuid().ToString().Replace("-", "");
                sb.Append(htmlHelper.RadioButton(name, radioItem.Value, radioItem.Value == selectedValue, new { id = itemId }));
                sb.AppendFormat("<label for='{0}'>", itemId);

                sb.Append("<span></span>");
                sb.AppendFormat("<span class='{0}'></span>", "check");
                sb.AppendFormat("<span class='{0}'></span>", "box");
                sb.Append(radioItem.Text);

                sb.Append("</label>");
                sb.Append("</div>");
            }

            if (direction == RepeatDirection.Horizontal)
            {
                sb.Insert(0, "<div class='md-radio-inline'>");
                sb.Append("</div>");
            }
            return new MvcHtmlString(sb.ToString());
        }
    }
}