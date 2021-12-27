using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public class BSFilterRadioList : IBSFilterRadioList, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        private IEnumerable<SelectListItem> _selectList;
        private string _tooltip;
        
        public BSFilterRadioList(string id)
        {
            _id = id;
            _inputType = FilterGroupType.Numeric;
        }

        public IBSFilterRadioList SetTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterRadioList SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterRadioList SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterRadioList SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            _selectList = selectList;
            return this;
        }
        public IBSFilterRadioList SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }

        public string ToHtmlString()
        {
            TagBuilder panelcontainer = new TagBuilder("div");
            TagBuilder panelheadercontainer = new TagBuilder("div");
            TagBuilder listcontainer = new TagBuilder("div");

            panelcontainer.AddCssClass("card");
            panelcontainer.AddCssClass("filter-group");

            panelheadercontainer.AddCssClass("card-header");
            if(!_tooltip.IsBlank())
            {
                panelheadercontainer.MergeAttribute("data-toggle", "tooltip");
                panelheadercontainer.MergeAttribute("data-placement", "top");
                panelheadercontainer.MergeAttribute("title", _tooltip);
            }
            //panelheadercontainer.InnerHtml = string.Format("<h5 class=\"panel-title\"><a href=\"#{0}\" data-toggle=\"collapse\"><i class=\"fa fa-cogs\"></i>{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", elementID, customTitle);
            panelheadercontainer.InnerHtml = string.Format("<h5 class=\"card-title\"><a href=\"#{0}\" data-toggle=\"collapse\">{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", _id.Replace(".", "\\."), _customTitle);

            listcontainer.AddCssClass("collapse");
            listcontainer.AddCssClass("list-group");
            listcontainer.AddCssClass("selectable");
            listcontainer.AddCssClass("radio");
            listcontainer.Attributes.Add("id", _id);
            listcontainer.Attributes.Add("data-type", _inputType.ToString());
            listcontainer.Attributes.Add("data-title", _customTitle);

            if (_selectList != null)
            {
                foreach (var item in _selectList)
                {
                    TagBuilder listItem = new TagBuilder("a");
                    listItem.Attributes.Add("href", "#");
                    listItem.Attributes.Add("data-value", item.Value);

                    listItem.AddCssClass("list-group-item");
                    if (item.Selected)
                    {
                        listItem.AddCssClass("selected");
                    }

                    listItem.InnerHtml = string.Format("<i class=\"fa {0} fa-fw\"></i>{1}", item.Selected ? "fa-circle" : "fa-circle-o", item.Text);
                    listcontainer.InnerHtml += listItem.ToString();
                }
            }

            panelcontainer.InnerHtml = string.Format("{0}{1}", panelheadercontainer.ToString(), listcontainer.ToString());

            return panelcontainer.ToString();
        }
    }
}