using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class CheckBoxHelper
    {
        public static MvcHtmlString SwitchCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, string text = "",
            string onText = "Açık", string offText = "Kapalı",
            object htmlAttributes = null)
        {
            var deleg = expression.Compile();

            var result = false;
            if (htmlHelper.ViewData.Model != null)
                result = deleg(htmlHelper.ViewData.Model);

            // get the field name
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            // get full field (template may have prefix)
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);


            var sb = new StringBuilder();
            sb.Append(text);
            sb.Append(htmlHelper.CheckBox(fullName, result, new { @class = "switch", data_on_text = onText, data_off_text = offText }));
            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString SwitchCheckBox<TModel>(this HtmlHelper<TModel> htmlHelper,
            string name, bool isChecked, string text = "",
            string onText = "Açık", string offText = "Kapalı",
            object htmlAttributes = null)
        {
            var sb = new StringBuilder();
            sb.Append(text);
            sb.Append(htmlHelper.CheckBox(name, isChecked, new { @class = "switch", data_on_text = onText, data_off_text = offText }));
            return new MvcHtmlString(sb.ToString());
        }


        public static MvcHtmlString MdCheckBoxFor<TModel>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, bool>> expression, string id = "",
            string text = "", object attributes = null)
        {

            var deleg = expression.Compile();

            var result = false;
            if (htmlHelper.ViewData.Model != null)
                result = deleg(htmlHelper.ViewData.Model);

            // get the field name
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            // get full field (template may have prefix)
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);

            var sb = new StringBuilder();

            var itemId = string.IsNullOrEmpty(id) ? Guid.NewGuid().ToString().Replace("-", "") : id;

            sb.Append("<div class='md-checkbox'>");

            sb.Append(htmlHelper.CheckBox(fullName, result, new { id = itemId }));
            sb.AppendFormat("<label for='{0}'>", itemId);

            sb.Append("<span></span>");
            sb.AppendFormat("<span class='{0}'></span>", "check");
            sb.AppendFormat("<span class='{0}'></span>", "box");
            sb.Append(text);

            sb.Append("</label>");
            sb.Append("</div>");


            return new MvcHtmlString(sb.ToString());
        }

        public static MvcHtmlString MdCheckBox<TModel>(this HtmlHelper<TModel> htmlHelper,
            string name, bool IsChecked, string text, object attributes = null)
        {
            var sb = new StringBuilder();
            sb.Append("<div class='md-checkbox'>");
            var itemId = Guid.NewGuid().ToString().Replace("-", "");
            sb.Append(htmlHelper.CheckBox(name, IsChecked, new { id = itemId }));
            sb.AppendFormat("<label for='{0}'>", itemId);

            sb.Append("<span></span>");
            sb.AppendFormat("<span class='{0}'></span>", "check");
            sb.AppendFormat("<span class='{0}'></span>", "box");
            sb.Append(text);

            sb.Append("</label>");
            sb.Append("</div>");


            return new MvcHtmlString(sb.ToString());
        }
    }


}