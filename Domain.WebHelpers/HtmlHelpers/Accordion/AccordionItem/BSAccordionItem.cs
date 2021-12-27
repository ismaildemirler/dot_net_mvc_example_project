using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem
{
    public class BSAccordionItem : IBSAccordionItem
    {
        private string _ajaxUrl;
        private EnumAccordiomItemStyles _style;
        private string _icon;
        private string _title;
        private string _parentId;
        private string _id;
        private string _method;
        private bool _collapsed;

        public BSAccordionItem()
        {
            _method = FormMethod.Get.ToString();
            _collapsed = true;
        }

        public IBSAccordionItem SetAjaxUrl(string ajaxUrl)
        {
            _ajaxUrl = ajaxUrl;
            return this;
        }

        public IBSAccordionItem SetStyle(EnumAccordiomItemStyles style)
        {
            _style = style;
            return this;
        }

        public IBSAccordionItem SetIcon(string icon)
        {
            _icon = icon;
            return this;
        }

        public IBSAccordionItem SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public IBSAccordionItem SetMethod(FormMethod method)
        {
            _method = method.ToString();
            return this;
        }

        public IBSAccordionItem SetCollapse(bool collapsed)
        {
            _collapsed = collapsed;
            return this;

        }

        public void SetParentId(string parentId, string id)
        {
            _parentId = parentId;
            _id = id;
        }

        public string ToHtmlString()
        {
            var accordionId = _id;
            var divContainer = new TagBuilder("div");

            divContainer.AddCssClass("m-accordion__item");

            switch (_style)
            {
                case EnumAccordiomItemStyles.Default:
                    break;
                case EnumAccordiomItemStyles.Primary:
                    divContainer.AddCssClass("m-accordion__item--primary");
                    break;
                case EnumAccordiomItemStyles.Success:
                    divContainer.AddCssClass("m-accordion__item--success");
                    break;
                case EnumAccordiomItemStyles.Info:
                    divContainer.AddCssClass("m-accordion__item--info");
                    break;
                case EnumAccordiomItemStyles.Warning:
                    divContainer.AddCssClass("m-accordion__item--warning");
                    break;
                case EnumAccordiomItemStyles.Danger:
                    divContainer.AddCssClass("m-accordion__item--danger");
                    break;
                case EnumAccordiomItemStyles.Focus:
                    divContainer.AddCssClass("m-accordion__item--focus");
                    break;
                case EnumAccordiomItemStyles.Brand:
                    divContainer.AddCssClass("m-accordion__item--brand");
                    break;
                case EnumAccordiomItemStyles.Accent:
                    divContainer.AddCssClass("m-accordion__item--accent");
                    break;
                case EnumAccordiomItemStyles.Metal:
                    divContainer.AddCssClass("m-accordion__item--metal");
                    break;
                case EnumAccordiomItemStyles.Light:
                    divContainer.AddCssClass("m-accordion__item--light");
                    break;
                default:
                    break;
            }


            var divHead = new TagBuilder("div");

            divHead.AddCssClass("m-accordion__item-head");
            if (_collapsed)
                divHead.AddCssClass("collapsed");

            divHead.MergeAttribute("role", "tab");
            divHead.MergeAttribute("id", $"m_{accordionId}_head");
            divHead.MergeAttribute("data-toggle", "collapse");
            divHead.MergeAttribute("href", $"#m_{accordionId}_body");
            divHead.MergeAttribute("aria-expanded", _collapsed ? "false" : "true");

            if (!string.IsNullOrEmpty(_icon))
            {
                var spanIcon = new TagBuilder("span");
                spanIcon.AddCssClass("m-accordion__item-icon");

                var iIcon = new TagBuilder("i");
                iIcon.AddCssClass($"{_icon}");
                spanIcon.InnerHtml = iIcon.ToString();

                divHead.InnerHtml += spanIcon;
            }

            var spanTitle = new TagBuilder("span");
            spanTitle.AddCssClass("m-accordion__item-title");
            spanTitle.InnerHtml = _title;

            var spanMode = new TagBuilder("span");
            spanMode.AddCssClass("m-accordion__item-mode");

            divHead.InnerHtml += spanTitle;
            divHead.InnerHtml += spanMode;

            var divBody = new TagBuilder("div");
            divBody.AddCssClass($"m-accordion__item-body  ajax_m-accordion__item-body");
            divBody.AddCssClass(_collapsed ? "collapse" : "show");

            divBody.MergeAttribute("role", "tabpanel");
            divBody.MergeAttribute("id", $"m_{accordionId}_body");
            divBody.MergeAttribute("aria-labelledby", $"m_{accordionId}_head");
            divBody.MergeAttribute("data-parent", $"#{_parentId}");

            var divBodyContent = new TagBuilder("div");
            divBodyContent.AddCssClass("m-accordion__item-content");

            var divBodyInnerContent = new TagBuilder("div");
            divBodyInnerContent.AddCssClass("ajaxAccordion");
            divBodyInnerContent.MergeAttribute("id", $"container_{accordionId}");
            divBodyInnerContent.MergeAttribute("data-url", _ajaxUrl);
            divBodyInnerContent.MergeAttribute("data-method", _method);

            divBodyContent.InnerHtml = divBodyInnerContent.ToString();
            divBody.InnerHtml = divBodyContent.ToString();

            divContainer.InnerHtml = divHead.ToString();
            divContainer.InnerHtml += divBody.ToString();

            return divContainer.ToString();
        }
    }
}
