using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class DateInputHelper
    {
        public static MvcHtmlString DateInputFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression, object htmlAttributes = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            string format = "dd/MM/yyyy",
            string formatJs = "dd.mm.yyyy")
        {

            var deleg = expression.Compile();


            if (htmlHelper.ViewData.Model != null)
            {
                var result = deleg(htmlHelper.ViewData.Model);
                var value = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData).Model;
                
            }


            // get the field name
            var fieldName = ExpressionHelper.GetExpressionText(expression);
            // get full field (template may have prefix)
            var fullName = htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(fieldName);

            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!attrs.ContainsKey("class"))
            {
                attrs.Add("class", "form-control datePicker");
            }

            if (minDateTime.HasValue)
            {
                attrs.Add("data-date-startDate", minDateTime.Value.ToString(format));
            }

            if (maxDateTime.HasValue)
            {
                attrs.Add("data-date-endDate", maxDateTime.Value.ToString(format));
            }

            attrs.Add("data-date-format", formatJs);
            attrs.Add("data-datatype", "tarih");

            var sb = new StringBuilder("<div class=\".date input-group \">\n");
            sb.AppendLine("<div class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></div>");
            sb.AppendLine(htmlHelper.TextBoxFor(expression, $"{{0:{format}}}", attrs).ToString());
            sb.AppendLine("</div> ");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString DateInput(this HtmlHelper htmlHelper,
            string name,
            DateTime value,
            object htmlAttributes = null,
            DateTime? minDateTime = null,
            DateTime? maxDateTime = null,
            string format = "dd/MM/yyyy",
            string formatJs = "dd.mm.yyyy")
        {
            var attrs = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            if (!attrs.ContainsKey("class"))
            {
                attrs.Add("class", "form-control datePicker");
            }

            if (minDateTime.HasValue)
            {
                attrs.Add("data-date-startDate", minDateTime.Value.ToString(format));
            }

            if (maxDateTime.HasValue)
            {
                attrs.Add("data-date-endDate", maxDateTime.Value.ToString(format));
            }

            attrs.Add("data-date-format", formatJs);
            attrs.Add("data-datatype", "tarih");

            var sb = new StringBuilder("<span class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></span>");
            sb.AppendLine(htmlHelper.TextBox(name, value == DateTime.MinValue ? "" : value.ToShortDateString(), attrs).ToString());
            return MvcHtmlString.Create(sb.ToString());
        }


        public static MvcHtmlString DateRangeInput<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> startDateExpression,
            Expression<Func<TModel, TProperty>> endDateExpression,
            object startDateHtmlAttributes = null,
            DateTime? startDateMinDateTime = null,
            DateTime? startDateMaxDateTime = null,
            object endDateHtmlAttributes = null,
            DateTime? endDateMinDateTime = null,
            DateTime? endDateMaxDateTime = null,
            string format = "dd.MM.yyyy",
            string formatJs = "dd.mm.yyyy")
        {
            var startDateAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(startDateHtmlAttributes);

            if (!startDateAttrs.ContainsKey("class"))
                startDateAttrs.Add("class", "form-control datePicker1");

            if (startDateMinDateTime.HasValue)
                startDateAttrs.Add("data-date-startDate", startDateMinDateTime.Value.ToString(format));

            if (startDateMaxDateTime.HasValue)
                startDateAttrs.Add("data-date-endDate", startDateMaxDateTime.Value.ToString(format));

            startDateAttrs.Add("data-date-format", formatJs);
            startDateAttrs.Add("data-datatype", "tarih");
            startDateAttrs.Add("data-enddateelement", endDateExpression.Name);

            var endDateAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(endDateHtmlAttributes);

            if (!endDateAttrs.ContainsKey("class"))
                endDateAttrs.Add("class", "form-control datePicker2");

            if (endDateMinDateTime.HasValue)
                endDateAttrs.Add("data-date-startDate", endDateMinDateTime.Value.ToString(format));

            if (endDateMaxDateTime.HasValue)
                endDateAttrs.Add("data-date-endDate", endDateMaxDateTime.Value.ToString(format));

            endDateAttrs.Add("data-date-format", formatJs);
            endDateAttrs.Add("data-datatype", "tarih");
            endDateAttrs.Add("data-startdateelement", startDateExpression.Name);

            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"row\">");
            sb.AppendLine("<div class=\"col-md-6 col-sm-6 col-xs-6\">");
            sb.AppendLine("<div class=\"input-group \">");
            sb.AppendLine("<div class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></div>");
            sb.AppendLine(htmlHelper.TextBoxFor(startDateExpression, $"{{0:{format}}}", startDateAttrs).ToString());
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");


            sb.AppendLine("<div class=\"col-md-6 col-sm-6 col-xs-6\">");
            sb.AppendLine("<div class=\"input-group \">");
            sb.AppendLine("<div class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></div>");
            sb.AppendLine(htmlHelper.TextBoxFor(endDateExpression, $"{{0:{format}}}", endDateAttrs).ToString());
            sb.AppendLine("</div> ");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

        public static MvcHtmlString DateRangeInput(this HtmlHelper htmlHelper,
            string startDateName,
            DateTime startDateValue,
            string endDateName,
            DateTime endDateValue,
            object startDateHtmlAttributes = null,
            DateTime? startDateMinDateTime = null,
            DateTime? startDateMaxDateTime = null,
            object endDateHtmlAttributes = null,
            DateTime? endDateMinDateTime = null,
            DateTime? endDateMaxDateTime = null,
            string format = "dd.MM.yyyy",
            string formatJs = "dd.mm.yyyy")
        {
            var startDateAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(startDateHtmlAttributes);

            if (!startDateAttrs.ContainsKey("class"))
                startDateAttrs.Add("class", "form-control datePicker1");

            if (startDateMinDateTime.HasValue)
                startDateAttrs.Add("data-date-startDate", startDateMinDateTime.Value.ToString(format));

            if (startDateMaxDateTime.HasValue)
                startDateAttrs.Add("data-date-endDate", startDateMaxDateTime.Value.ToString(format));

            startDateAttrs.Add("data-date-format", formatJs);
            startDateAttrs.Add("data-datatype", "tarih");
            startDateAttrs.Add("data-enddateelement", endDateName);

            var endDateAttrs = HtmlHelper.AnonymousObjectToHtmlAttributes(endDateHtmlAttributes);

            if (!endDateAttrs.ContainsKey("class"))
                endDateAttrs.Add("class", "form-control datePicker2");

            if (endDateMinDateTime.HasValue)
                endDateAttrs.Add("data-date-endDate", endDateMinDateTime.Value.ToString(format));

            if (endDateMaxDateTime.HasValue)
                endDateAttrs.Add("data-date-endDate", endDateMaxDateTime.Value.ToString(format));

            endDateAttrs.Add("data-date-format", formatJs);
            endDateAttrs.Add("data-datatype", "tarih");
            endDateAttrs.Add("data-startdateelement", startDateName);

            var sb = new StringBuilder();
            sb.AppendLine("<div class=\"row\">");
            sb.AppendLine("<div class=\"col-md-6 col-sm-6 col-xs-6\">");
            sb.AppendLine("<div class=\"input-group \">");
            sb.AppendLine("<div class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></div>");
            sb.AppendLine(htmlHelper.TextBox(startDateName, startDateValue == DateTime.MinValue ? "" : startDateValue.ToShortDateString(), startDateAttrs).ToString());
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");


            sb.AppendLine("<div class=\"col-md-6 col-sm-6 col-xs-6\">");
            sb.AppendLine("<div class=\"input-group \">");
            sb.AppendLine("<div class=\"input-group-addon\"><i class=\"fa fa-calendar\"></i></div>");
            sb.AppendLine(htmlHelper.TextBox(endDateName, endDateValue == DateTime.MinValue ? "" : endDateValue.ToShortDateString(), endDateAttrs).ToString());
            sb.AppendLine("</div> ");
            sb.AppendLine("</div>");
            sb.AppendLine("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }
    }
}