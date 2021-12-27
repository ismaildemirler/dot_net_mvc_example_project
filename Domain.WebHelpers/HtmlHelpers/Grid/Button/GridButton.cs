using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.Button
{
    public class GridButton : IGridButton
    {
        private string _id;
        private string _title;
        private string _tooltip;
        private string _actionURL;
        private string _cssClass;
        private string _icon;
        private GridButtonType _buttonType;
        private string _jsFunction;
        private object _htmlAttributes;

        public GridButton(GridButtonType buttonType)
        {
            _buttonType = buttonType;
        }

        public string ID { get { return _id; } }
        public string Title { get { return _title; } }
        public string Tooltip { get { return _tooltip; } }
        public string ActionUrl { get { return _actionURL; } }
        public string CssClass { get { return _cssClass; } }
        public string Icon { get { return _icon; } }
        public GridButtonType ButtonType { get { return _buttonType; } }
        public string JsFunction { get { return _jsFunction; } }
        public string HtmlAttributes { get { return _jsFunction; } }

        public IGridButton SetID(string id)
        {
            _id = id;
            return this;
        }
        public IGridButton SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IGridButton SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }
        public IGridButton SetActionUrl(string actionUrl)
        {
            _actionURL = actionUrl;
            return this;
        }
        public IGridButton SetCssClass(string cssClass)
        {
            _cssClass = cssClass;
            return this;
        }
        public IGridButton SetIcon(string icon)
        {
            _icon = icon;
            return this;
        }
        public IGridButton SetButtonType(GridButtonType buttonType)
        {
            _buttonType = buttonType;
            return this;
        }

        public IGridButton SetJsFuntion(string jsFunction)
        {
            _jsFunction = jsFunction;
            return this;
        }

        public IGridButton SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }

        public override string ToString()
        {
            string idContent = "";
            string tooltipContent = "";
            string dataContent = "";
            string iconContent = "";
            string actionContent = "";
            string typeContent = "";
            string btnColorCss = "btn-default";

            #region add tooltip
            if (_tooltip.IsBlank())
            {
                switch (_buttonType)
                {
                    case GridButtonType.insert:
                        _tooltip = "Kayıt Ekle";
                        break;
                    case GridButtonType.export:
                        _tooltip = "Yazdır";
                        break;
                }
            }
            #endregion

            #region add icon
            if (_icon.IsBlank())
            {
                switch (_buttonType)
                {
                    case GridButtonType.insert:
                        _icon = "fa-plus";
                        break;
                    case GridButtonType.export:
                        _icon = "fa-download";
                        break;
                }
            }
            #endregion

            #region add html attributes
            if (!_id.IsBlank())
                idContent = $" id='{_id}'";

            if (!_actionURL.IsBlank())
                actionContent = $" data-action='{_actionURL}'";

            if (!_tooltip.IsBlank())
                tooltipContent = $" data-toggle='m-popover' data-offset='10px 10px' data-placement='right' data-content='{_tooltip}'";
            #endregion

            #region add button color
            switch (_buttonType)
            {
                case GridButtonType.insert:
                    btnColorCss = "btn-default";
                    break;
                case GridButtonType.export:
                    btnColorCss = "btn-secondary";
                    break;
            }
            #endregion

            if (_title.IsBlank())
            {
                if (_icon.IsBlank())
                    _icon = "fa-gear";

                iconContent = $"<i class='fa {_icon}'></i>";
            }
            else
            {
                if (!_icon.IsBlank())
                    iconContent = $"<i class='fa {_icon} xs-rmargin'></i>";
            }

            if (_buttonType == GridButtonType.insert)
                typeContent = "grid-btn-insert";
            else if (_buttonType == GridButtonType.export)
                typeContent = "grid-btn-export";
            else if (_buttonType == GridButtonType.custom)
                typeContent = "grid-btn-custom";

            if (!_cssClass.IsBlank())
                btnColorCss = _cssClass;

            var attr = Util.GetHtmlAttributes(Util.MergeHtmlAttributes(_htmlAttributes, ""));

            return
                $"<li class='m-portlet__nav-item'> <a{idContent} {actionContent} class='btn {btnColorCss} {typeContent} ml-2' {tooltipContent} {attr} {dataContent}>{iconContent} {_title}</a> </li>";
        }
    }
}