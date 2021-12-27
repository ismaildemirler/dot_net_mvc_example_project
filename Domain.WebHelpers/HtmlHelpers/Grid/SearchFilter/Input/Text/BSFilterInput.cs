using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Text
{
    public class BSFilterInput : IBSFilterInput, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _modalHtmlAttributes;

        public BSFilterInput(string id)
        {
            _id = id;
            _inputType = FilterGroupType.Text;
        }

        public IBSFilterInput SetTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterInput SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterInput SetHtmlAttributes(object htmlAttributes)
        {
            _modalHtmlAttributes = htmlAttributes;
            return this;
        }

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();

            TagBuilder panelcontainer = new TagBuilder("div");
            TagBuilder panelheadercontainer = new TagBuilder("div");
            TagBuilder listcontainer = new TagBuilder("div");

            panelcontainer.AddCssClass("card");
            panelcontainer.AddCssClass("filter-group");

            panelheadercontainer.AddCssClass("card-header");
            panelheadercontainer.InnerHtml = string.Format("<h5 class=\"card-title\"><a href=\"#{0}\" data-toggle=\"collapse\">{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", _id.Replace(".", "\\."), _customTitle);

            listcontainer.AddCssClass("collapse");
            listcontainer.AddCssClass("list-group");
            listcontainer.AddCssClass("selectable");
            listcontainer.AddCssClass("inputfilter");
            listcontainer.Attributes.Add("id", _id);
            listcontainer.Attributes.Add("data-type", _inputType.ToString());
            listcontainer.Attributes.Add("data-title", _customTitle);

            sb.Append("<div class='input-group'>");
            if (_inputType == FilterGroupType.Date)
            {
                sb.Append("<input class='input-sm form-control datepicker' data-dateformat='L'>");
            }
            else
            {
                sb.AppendFormat("<input type='{0}' class='input-sm form-control'>", _inputType);
            }
            sb.Append("<div class='input-group-append'><button class='btn btn-input btn-sm' type='button'><i class='fa fa-plus'></i></button></div>");
            sb.Append("</div>");

            listcontainer.InnerHtml = sb.ToString();

            panelcontainer.InnerHtml = string.Format("{0}{1}", panelheadercontainer.ToString(), listcontainer.ToString());
            
            return panelcontainer.ToString();
        }
    }
}