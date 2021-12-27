using System;
using System.Text;
using System.Web;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Input.Button.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.Button
{
    public class BSButton : IBSButton, IHtmlString
    {
        #region private properties
        private string _id;
        private string _actionUrl;
        private string _imageUrl;
        private string _title;
        private string _cssIcon;
        private bool _isDisabled;
        private bool _isBackButton;
        private bool _isExportButton;
        private int _isExportButtonLoadingTimeOut;
        private object _htmlAttributes;
        private string _additionalClasses;
        private string _confirmText;
        private string _confirmRelatedFormId;
        private string _tooltip;

        private ButtonType buttonType;
        private ButtonStyle buttonStyle;
        private ButtonSize buttonSize;
        private ButtonIconAlignment iconAlignment;
        #endregion

        #region ctor
        public BSButton(string id = "")
        {
            this._id = id;
            _isDisabled = false;
            buttonType = ButtonType.button;
            buttonStyle = ButtonStyle.@default;
            buttonSize = ButtonSize.@default;
        }
        #endregion

        #region fluent methods
        public IBSButton SetActionUrl(string url)
        {
            this._actionUrl = url;
            return this;
        }
        public IBSButton SetCustomClasses(string additionalClasses)
        {
            this._additionalClasses = additionalClasses;
            return this;
        }
        public IBSButton SetImageUrl(string imageUrl)
        {
            if (buttonStyle != ButtonStyle.RoundedImage)
            {
                throw new Exception("Button style image olmalıdır.");
            }
            this._imageUrl = imageUrl;
            return this;
        }
        public IBSButton SetIcon(string cssIcon)
        {
            if (buttonStyle == ButtonStyle.RoundedImage)
            {
                throw new Exception("Button style image olmamalıdır.");
            }
            this._cssIcon = cssIcon;
            return this;
        }
        public IBSButton SetIconAlignment(ButtonIconAlignment iconAlignment)
        {
            if (buttonStyle == ButtonStyle.RoundedIcon && buttonStyle == ButtonStyle.RoundedSmallIcon && buttonStyle == ButtonStyle.RoundedImage)
            {
                throw new Exception("Button style Icon Alignment için uygun değil.");
            }
            this.iconAlignment = iconAlignment;
            return this;

        }
        public IBSButton SetSize(ButtonSize buttonSize)
        {
            this.buttonSize = buttonSize;
            return this;
        }
        public IBSButton SetTitle(string title)
        {
            this._title = title;
            return this;
        }
        public IBSButton SetType(ButtonType buttonType)
        {
            this.buttonType = buttonType;
            return this;
        }
        public IBSButton SetStyle(ButtonStyle buttonStyle)
        {
            this.buttonStyle = buttonStyle;
            return this;
        }
        public IBSButton SetDisabled(bool isDisabled)
        {
            _isDisabled = isDisabled;
            return this;
        }
        public IBSButton SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSButton SetBackButton(bool isBackButton)
        {
            _isBackButton = isBackButton;
            return this;
        }

        public IBSButton SetConfirm(string confirmText, string relatedFormId)
        {
            _confirmText = confirmText;
            _confirmRelatedFormId = relatedFormId;
            return this;
        }

        public IBSButton SetExportButton(bool isExport, int timeOutMiliSecond = 5000)
        {
            _isExportButton = isExport;
            _isExportButtonLoadingTimeOut = timeOutMiliSecond;
            return this;
        }

        public IBSButton SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();
            string idHtml = string.Empty;
            string iconHtml = "";
            string buttonClass = "";

            if (_title.IsBlank() && _isBackButton)
                _title = "Geri";

            if (buttonStyle == ButtonStyle.RoundedIcon || buttonStyle == ButtonStyle.RoundedSmallIcon || buttonStyle == ButtonStyle.RoundedImage)
                buttonClass = buttonStyle.ToDisplayName();
            else
                buttonClass = $"btn {buttonStyle.ToDisplayName()}";

            if (buttonSize != ButtonSize.@default)
                buttonClass = $"{buttonClass} {buttonSize.ToDisplayName()}";

            if (!string.IsNullOrWhiteSpace(_additionalClasses))
                buttonClass += " " + _additionalClasses;

            if (_isExportButton)
                buttonClass += " ExportButton";

            if (!string.IsNullOrEmpty(_confirmText))
                buttonClass += " onay";

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = buttonClass });

            if (!_actionUrl.IsBlank())
            {
                if (buttonType == ButtonType.button)
                    attributes = Util.MergeHtmlAttributes(attributes, new { data_url = _actionUrl });
                else if (buttonType == ButtonType.submit)
                    attributes = Util.MergeHtmlAttributes(attributes, new { data_form_action = _actionUrl });
            }

            if (!string.IsNullOrEmpty(_id))
            {
                _id = _id.Replace(" ", "").Replace("-", "_");
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });
            }

            if (!string.IsNullOrEmpty(_confirmText))
            {
                attributes = Util.MergeHtmlAttributes(attributes, new { data_uyari = _confirmText });
                if (!string.IsNullOrEmpty(_confirmRelatedFormId))
                    attributes = Util.MergeHtmlAttributes(attributes, new { data_relatedformid = _confirmRelatedFormId });
            }

            if (_isExportButton)
            {
                if (buttonType == ButtonType.link)
                    attributes = Util.MergeHtmlAttributes(attributes, new { target = "_blank" });
                else
                    attributes = Util.MergeHtmlAttributes(attributes, new { data_exportloadingtimeout = _isExportButtonLoadingTimeOut });
            }

            if (!_isBackButton && buttonStyle == ButtonStyle.RoundedImage)
            {
                if (!_imageUrl.IsBlank())
                    iconHtml = $"<img src='{VirtualPathUtility.ToAbsolute(_imageUrl)}' />";
                else
                    iconHtml = "<i class='fa fa-question'></i>";
            }
            else
            {
                if (!_cssIcon.IsBlank())
                    iconHtml = $"<i class='{_cssIcon}'></i>";
                else if (_isBackButton)
                    iconHtml = "<i class='fa fa-backward'></i>";
            }

            if (!_isBackButton && buttonType != ButtonType.link)
            {
                sb.AppendFormat("<button type='{0}' {1} {2} data-toggle='tooltip' title='{3}'>", buttonType, Util.GetHtmlAttributes(attributes), _isDisabled ? " disabled" : "", _tooltip);
            }
            else
            {
                if (_isBackButton)
                    sb.AppendFormat("<a href='#' onclick='window.history.back();' {0} {1} data-toggle='tooltip' title='{2}'>", Util.GetHtmlAttributes(attributes), _isDisabled ? " disabled" : "", _tooltip);
                else
                    sb.AppendFormat("<a href='{0}' {1} {2} data-toggle='tooltip' title='{3}'>", _actionUrl, Util.GetHtmlAttributes(attributes), _isDisabled ? " disabled" : "", _tooltip);
            }

            if (buttonStyle != ButtonStyle.RoundedIcon && buttonStyle != ButtonStyle.RoundedSmallIcon && buttonStyle != ButtonStyle.RoundedImage)
            {
                if (iconAlignment == ButtonIconAlignment.left)
                    sb.AppendFormat("{0} {1}", iconHtml, _title);
                else
                    sb.AppendFormat("{0} {1}", _title, iconHtml);
            }
            else
            {
                if (buttonStyle != ButtonStyle.RoundedSmallIcon)
                    sb.AppendFormat("{0}<h6>{1}</h6>", iconHtml, _title);
                else
                    sb.Append(iconHtml);
            }

            if (!_isBackButton && buttonType != ButtonType.link)
                sb.AppendFormat("</button>");
            else
                sb.AppendFormat("</a>");

            return sb.ToString();
        }
    }
}