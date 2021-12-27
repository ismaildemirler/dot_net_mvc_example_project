using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Tab.Enum;
using Domain.WebHelpers.HtmlHelpers.Tab.TabItem;

namespace Domain.WebHelpers.HtmlHelpers.Tab
{
    public class BSTab : IBSTab, IHtmlString
    {
        private readonly string _id;
        private List<IBSTabItem> _items;
        private object _htmlAttributes;
        private EnumTabStyles _style;

        public BSTab(string id)
        {
            _id = id;
            _items = new List<IBSTabItem>();
            _style = EnumTabStyles.LineTab;
        }

        public IBSTab SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }

        public IBSTab SetItems(List<IBSTabItem> items)
        {
            _items = items;
            return this;
        }

        public IBSTab SetStyle(EnumTabStyles style)
        {
            _style = style;
            return this;
        }

        public string ToHtmlString()
        {
            var sb = new StringBuilder();
            var divContainer = new TagBuilder("div");
            divContainer.AddCssClass("alert");
            var ulTabs = new TagBuilder("ul");
            if (_htmlAttributes != null)
            {
                var attributes = Util.MergeHtmlAttributes(_htmlAttributes, null);
                ulTabs.MergeAttributes(attributes);
            }

            ulTabs.AddCssClass("m-tabs-line nav nav-tabs");

            switch (_style)
            {
                case EnumTabStyles.LineTab:
                    break;
                case EnumTabStyles.BoldLineTab:
                    ulTabs.AddCssClass("m-tabs-line--2x");
                    break;
                case EnumTabStyles.LineTab_Success:
                    ulTabs.AddCssClass("m-tabs-line--success");
                    break;
                case EnumTabStyles.BoldLineTab_Success:
                    ulTabs.AddCssClass("m-tabs-line--success m-tabs-line--2x");
                    break;
                case EnumTabStyles.LineTab_Danger:
                    ulTabs.AddCssClass("m-tabs-line--danger");
                    break;
                case EnumTabStyles.BoldLineTab_Danger:
                    ulTabs.AddCssClass("m-tabs-line--danger m-tabs-line--2x");
                    break;
                case EnumTabStyles.LineTab_Info:
                    ulTabs.AddCssClass("m-tabs-line--info");
                    break;
                case EnumTabStyles.BoldLineTab_Info:
                    ulTabs.AddCssClass("m-tabs-line--info m-tabs-line--2x");
                    break;
                case EnumTabStyles.LineTab_Warning:
                    ulTabs.AddCssClass("m-tabs-line--warning");
                    break;
                case EnumTabStyles.BoldLineTab_Warning:
                    ulTabs.AddCssClass("m-tabs-line--warning m-tabs-line--2x");
                    break;
                default:
                    break;
            }

            var divTabContent = new TagBuilder("div");
            divTabContent.AddCssClass("tab-content");

            for (var i = 0; i < _items.Count; i++)
            {
                var item = _items[i];
                item.SetId($"m-tab_{_id}_{i}");
                ulTabs.InnerHtml += item.ToHtmlStringTab();
                divTabContent.InnerHtml += item.ToHtmlStringContent();
            }

            var sbJs = new StringBuilder();
            sbJs.Append(@" <script type='text/javascript'>
                                    $(function () {
                                        $('.ajaxTabBtn').each(function (i) {
                                            if ($(this).hasClass('active')) {
                                                GetAjaxTabPartialData($(this));
                                            }
                                        });
                                        $('.ajaxTabBtn').on('shown.bs.tab',
                                            function (e) {
                                                GetAjaxTabPartialData($(this));
                                            });
                                    });        
                           </script>");
            divContainer.InnerHtml += ulTabs.ToString();
            divContainer.InnerHtml += divTabContent.ToString();
            sb.Append(divContainer);
            sb.Append(sbJs);
            return sb.ToString();
        }
    }
}
