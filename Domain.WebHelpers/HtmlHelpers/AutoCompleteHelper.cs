using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Infrastructure.Web;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Linq.Expressions;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Domain.WebHelpers.HtmlHelpers
{
    public static class AutoCompleteHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="htmlHelper"></param>
        /// <param name="expression"></param>
        /// <param name="ajaxFuction"></param>
        /// <param name="placeholder"></param>
        /// <param name="ekParametreler">genellikle gelen entity için yazılmış entityAra tipinde obje</param>
        /// <param name="tip">Liste veya tree</param>
        /// <param name="birimYetkiKontrol"></param>
        /// <param name="limit">gelecek kayıt sayısı</param>
        /// <param name="callback">seçim yapıldıktan sonra çalışacak js fonksiyon onselect(item : {value: "",text: ""}) </param>
        /// <param name="viewUrl">tip customView seçildi ise mutlaka vir action adresi belirtilmelidir Örnek: /Hesap/index</param>
        /// <param name="disabled">seçim değiştirilebilir mi</param>
        /// <param name="showDescription"></param>
        /// <param name="clearCallback">temizleme yapılınca çağrılacak js func</param>
        /// <param name="multiple"></param>
        /// <param name="connectedWith">ilişkili olduğu diğer autoComplete <b>Html.idFor(exp) ile set edin!</b></param>
        /// <param name="valueLabel"></param>
        /// <returns></returns>
        public static MvcHtmlString AutoComplete<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            Enums.AutocompleteFuction ajaxFuction,
            Enums.AutoCompleteType tip = Enums.AutoCompleteType.List,
            string placeholder = "Aramak için yazın",
            object ekParametreler = null,
            bool birimYetkiKontrol = true,
            int limit = 10,
            string callback = "",
            string viewUrl = "",
            bool disabled = false,
            bool showDescription = false,
            string clearCallback = "",
            bool multiple = false,
            string connectedWith = "",
            string valueLabel = ""
        )
        {
            string targetId = htmlHelper.IdFor(expression).ToString();

            var sb = new StringBuilder("<div class=\"input-group\" id=\"__").Append(targetId);
            sb.Append(disabled ? "\" style=\"width:100%\">\n" : "\" style=\"width:auto\">\n");
            sb.Append("  <span class=\"input-group-addon\"><i class=\"fa fa-search\"></i></span>");

            var url = EnumExtensions.ToDescription(ajaxFuction);


            url += !url.Contains("?") ? $"?limit={limit}&" : $"&limit={limit}&";


            if (ekParametreler != null)
            {
                url += ekParametreler.ToQueryString();
            }

            //if (birimYetkiKontrol)
            //{
            //    url += $"&birimid={YetkiAraci.AktifEkranYetkiliKapsamBirimID.ToInt32()}";
            //}


            //   string modelPropFullName = htmlHelper.NameFor(expression).ToString();

            //    var method = expression.Compile();

            // var value = method(htmlHelper.ViewData.Model);

            //List<string> lstModelPropName = expression.Body.ToString().Split('.').ToList();
            //lstModelPropName.RemoveAt(0);


            //lstModelPropName.ForEach(p => modelPropFullName = string.Format("{0}{1}.", modelPropFullName, p));
            //modelPropFullName = "_" + modelPropFullName.TrimEnd('.').Replace('.', '_');


            var properties = new RouteValueDictionary
            {
                {"placeholder", placeholder},
                {"id", $"_{targetId}"},
                {"title", placeholder},
                {"class", "form-control typeahead "},
                {"autocomplete", "off"},
                {"data-target", targetId},
                {"data-showDesc", showDescription},
                {"data-multiple", multiple},
                {"data-url", url},
                {"data-ajaxDeger", EnumExtensions.ToDescription(((Enums.AutocompleteDegerFuction) ajaxFuction.GetHashCode()))},
                {"data-ajaxCokDeger", EnumExtensions.ToDescription(((Enums.AutocompleteCokDegerFuction) ajaxFuction.GetHashCode()))}
            };


            if (!string.IsNullOrEmpty(callback))
            {
                properties.Add("data-callback", callback);
            }

            if (!string.IsNullOrEmpty(clearCallback))
            {
                properties.Add("data-clearcallback", clearCallback);
            }

            if (ekParametreler != null)
            {
                properties.Add("data-ekParametreler", ekParametreler.ToJson());
            }

            if (!string.IsNullOrEmpty(viewUrl))
            {
                properties.Add("data-viewUrl", viewUrl);
            }

            //if (birimYetkiKontrol)
            //{
            //    properties.Add("data-birimid", YetkiAraci.AktifEkranYetkiliKapsamBirimID.ToInt32());
            //}

            if (disabled)

            {
                properties.Add("disabled", "disabled");
            }


            if (!string.IsNullOrEmpty(connectedWith))
            {
                properties.Add("data-connectedwith", connectedWith.Replace(".", "_"));
            }


            sb.AppendLine(htmlHelper.TextBox($"_{targetId}", valueLabel, properties).ToString());

            sb.AppendLine(htmlHelper.HiddenFor(expression).ToString());

            if (!disabled) //disable yapılmışsa clear ve seçim gösterilmeyecek
            {
                sb.Append("<span class=\"input-group-addon\">")
                  .AppendFormat("<a id='ac_clear_{0}' title=\"Temizle\" class='autocomplete_clear' data-target='{0}'>", targetId)
                  .Append("<i class=\"fa fa-times text-danger\"></i></a>")
                  .Append("</span>");

                if (tip != Enums.AutoCompleteType.None)
                {
                    sb.Append("<span class=\"input-group-addon\">")
                      .Append("<a class=\"listopener\" title=\"")
                      .Append(placeholder)
                      .Append("\" data-target=\"")
                      .Append(targetId)
                      .Append("\" data-type=\"")
                      .Append(tip)
                      .Append("\"><i class=\"fa fa-list-alt\"></i></a>")
                      .Append("</span>");
                }
            }
            sb.AppendLine("</div> ");
            if (multiple)
                sb.Append("<div class=\"ac_selection\" id=\"ac_selection_").Append(targetId).Append("\"></div>");

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}