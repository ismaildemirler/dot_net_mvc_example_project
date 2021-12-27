using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Range
{
    public class BSFilterRange : IBSFilterRange, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _modalHtmlAttributes;

        public BSFilterRange(string id)
        {
            _id = id;
            _inputType = FilterGroupType.Text;
        }

        public IBSFilterRange SetTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterRange SetInputType(FilterGroupType inputType = FilterGroupType.Text)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterRange SetHtmlAttributes(object htmlAttributes)
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
            listcontainer.AddCssClass("rangefilter");
            listcontainer.Attributes.Add("id", _id);
            listcontainer.Attributes.Add("data-type", _inputType.ToString());
            listcontainer.Attributes.Add("data-title", _customTitle);

            if (_inputType == FilterGroupType.Date)
            {
                sb.Append("<div class='input-group input-daterange'>");
                var endElmId = Guid.NewGuid().ToString();
                sb.AppendFormat("<input type='text' class='input-sm form-control datepicker valuemin dateRangeFilterStart' data-dateformat='L' data-enddateelement='{0}'>", endElmId);
                sb.Append("<div class='input-group-append'><span class='input-group-text'> - </span></div>");
                sb.AppendFormat("<input type='text' class='input-sm form-control datepicker valuemax' data-dateformat='L' id={0}>", endElmId);
            }
            else
            {
                sb.Append("<div class='input-group'>");
                sb.AppendFormat("<input type='{0}' class='input-sm form-control valuemin' >", _inputType);
                sb.Append("<div class='input-group-append'><span class='input-group-text'> - </span></div>");
                sb.AppendFormat("<input type='{0}' class='input-sm form-control valuemax' >", _inputType);
            }
            sb.Append("<div class='input-group-append'><span> <button class='btn btn-sm btn-range' type='button'><i class='fa fa-plus'></i></button></span> </div>");
            sb.Append("</div>");

            listcontainer.InnerHtml = sb.ToString();

            panelcontainer.InnerHtml = string.Format("{0}{1}", panelheadercontainer.ToString(), listcontainer.ToString());

            return panelcontainer.ToString();
        }
    }
}