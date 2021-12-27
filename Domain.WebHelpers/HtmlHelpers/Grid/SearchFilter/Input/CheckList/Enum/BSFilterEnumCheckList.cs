using System.Linq;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Enum
{
    public class BSFilterEnumCheckList<TEnum> : IBSFilterEnumCheckList, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        
        public BSFilterEnumCheckList(string id)
        {
            _inputType = FilterGroupType.Numeric;
            _id = id;
        }

        public IBSFilterEnumCheckList SetTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterEnumCheckList SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        
        public string ToHtmlString()
        {
            TagBuilder panelcontainer = new TagBuilder("div");
            TagBuilder panelheadercontainer = new TagBuilder("div");
            TagBuilder listcontainer = new TagBuilder("div");
            FilterGroupType inputType = FilterGroupType.Numeric;
            
            var values = System.Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            var items = values.Select((value, index) => new SelectListItem { Text = Util.GetEnumDescription(value), Value = ((int)System.Enum.Parse(typeof(TEnum), value.ToString())).ToString() });
            
            panelcontainer.AddCssClass("card");
            panelcontainer.AddCssClass("filter-group");

            panelheadercontainer.AddCssClass("card-header");
            //panelheadercontainer.InnerHtml = string.Format("<h5 class=\"panel-title\"><a href=\"#{0}\" data-toggle=\"collapse\"><i class=\"fa fa-cogs\"></i>{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", elementID, customTitle);
            panelheadercontainer.InnerHtml = string.Format("<h5 class=\"card-title\"><a href=\"#{0}\" data-toggle=\"collapse\">{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", _id.Replace(".", "\\."), _customTitle);

            listcontainer.AddCssClass("collapse");
            listcontainer.AddCssClass("list-group");
            listcontainer.AddCssClass("selectable");
            listcontainer.Attributes.Add("id", _id);
            listcontainer.Attributes.Add("data-type", inputType.ToString());
            listcontainer.Attributes.Add("data-title", _customTitle);

            if (items != null)
            {
                foreach (var item in items)
                {
                    TagBuilder listItem = new TagBuilder("a");
                    listItem.Attributes.Add("href", "#");
                    listItem.Attributes.Add("data-value", item.Value);

                    listItem.AddCssClass("list-group-item");
                    if (item.Selected)
                    {
                        listItem.AddCssClass("selected");
                    }

                    listItem.InnerHtml = string.Format("<i class=\"fa {0} fa-fw\"></i>{1}", item.Selected ? "fa-check-square-o" : "fa-square-o", item.Text);
                    listcontainer.InnerHtml += listItem.ToString();
                }
            }

            panelcontainer.InnerHtml = string.Format("{0}{1}", panelheadercontainer.ToString(), listcontainer.ToString());

            return panelcontainer.ToString();
        }
    }
}