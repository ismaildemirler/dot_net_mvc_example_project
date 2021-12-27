using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class HtmlEditorHelper
    {
        public static MvcHtmlString HtmlEditorFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            string placeHolder=""
            )
        {

            var name = "_" + Guid.NewGuid().ToString().Replace("-", "");

            var sb = new StringBuilder("<span data-target='").Append(name).Append("' class=\"form-control html-edit \"");


            if (!string.IsNullOrEmpty(placeHolder))
                sb.Append(" title='").Append(placeHolder).Append("' ");
            
            sb.Append("></span>\n");
            
            sb.Append("<div class='hidden'>");
            sb.AppendLine(htmlHelper.TextBoxFor(expression,new {@class=name}).ToString());
            sb.Append("</div>");
            return MvcHtmlString.Create(sb.ToString());
        }

      



    }
}