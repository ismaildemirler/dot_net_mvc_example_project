using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Cascading
{
    public class BSFilterCascadingCheckList : IBSFilterCascadingCheckList, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        IEnumerable<SelectListItem> _selectList;
        string _targetElementID;
        string _targetUrl;

        public BSFilterCascadingCheckList(string id)
        {
            _id = id;
        }

        public IBSFilterCascadingCheckList SetTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterCascadingCheckList SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterCascadingCheckList SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterCascadingCheckList SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            _selectList = selectList;
            return this;
        }
        public IBSFilterCascadingCheckList SetTargetElemetID(string targetElemetID)
        {
            _targetElementID = targetElemetID;
            return this;
        }
        public IBSFilterCascadingCheckList SetTargetUrl(string targetUrl)
        {
            _targetUrl = targetUrl;
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
            //panelheadercontainer.InnerHtml = string.Format("<h5 class=\"panel-title\"><a href=\"#{0}\" data-toggle=\"collapse\"><i class=\"fa fa-cogs\"></i>{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", elementID, customTitle);
            panelheadercontainer.InnerHtml = string.Format("<h5 class=\"card-title\"><a href=\"#{0}\" data-toggle=\"collapse\">{1}<i class=\"pull-right fa fa-caret-down\"></i></a></h5>", _id.Replace(".", "\\."), _customTitle);

            listcontainer.AddCssClass("collapse");
            listcontainer.AddCssClass("list-group");
            listcontainer.AddCssClass("selectable");
            listcontainer.Attributes.Add("id", _id);
            listcontainer.Attributes.Add("data-type", _inputType.ToString());
            listcontainer.Attributes.Add("data-title", _customTitle);
            listcontainer.Attributes.Add("data-targetid", _targetElementID);
            listcontainer.Attributes.Add("data-targeturl", _targetUrl);
            
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

                    listItem.InnerHtml = string.Format("<i class=\"fa {0} fa-fw\"></i>{1}", item.Selected ? "fa-check-square-o" : "fa-square-o", item.Text);
                    listcontainer.InnerHtml += listItem.ToString();
                }
            }

            panelcontainer.InnerHtml = string.Format("{0}{1}", panelheadercontainer.ToString(), listcontainer.ToString());
            return panelcontainer.ToString();
        }
    }
}