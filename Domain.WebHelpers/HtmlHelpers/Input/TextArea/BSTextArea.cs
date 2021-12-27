using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextArea
{
    public class BSTextArea : IBSTextArea, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;

        private string _id;
        private string _value;
        private string _title;
        private string _placeholder;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private object _htmlAttributes;
        private LayoutType _layoutType;

        private int _cols;
        private int _rows;

        private bool _isReadOnly;
        private bool _isDisable;
        private bool _isTitleHidden;

        private string _bindProp;
        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        private string _requiredSourceExp;
        private string _requiredExp;
        #endregion

        #region ctor
        public BSTextArea(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
            _isReadOnly = false;
            _isDisable = false;
            _rows = 3;
        }
        #endregion

        #region fluent methods
        public IBSTextArea Cols(int cols)
        {
            this._cols = cols;
            return this;
        }
        public IBSTextArea Rows(int rows)
        {
            this._rows = rows;
            return this;
        }
        public IBSTextArea SetTitle(string title)
        {
            this._title = title;
            return this;
        }
        public IBSTextArea SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSTextArea SetValue(string value)
        {
            this._value = value;
            return this;
        }
        public IBSTextArea SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSTextArea SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSTextArea SetPlaceholder(string placeholder)
        {
            this._placeholder = placeholder;
            return this;
        }
        public IBSTextArea SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSTextArea SetReadOnly(bool isReadOnly)
        {
            this._isReadOnly = isReadOnly;
            return this;
        }
        public IBSTextArea SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSTextArea SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSTextArea SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSTextArea SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSTextArea SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSTextArea SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextArea SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextArea SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input" });

            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);
            attributes = attributes.SetReadOnlyAttributes(_isReadOnly);
            attributes = attributes.SetPlaceHolderAttributes(_placeholder);

            attributes = Util.MergeHtmlAttributes(attributes, new { rows = _rows });

            TagBuilder divParent = new TagBuilder("div");
            divParent.AddCssClass("m-form p-2");

            bool isColAdded = false;
            if (!_parentDivCSS.IsBlank())
            {
                divParent.AddCssClass(_parentDivCSS);
                if (_parentDivCSS.Contains("col "))
                    isColAdded = true;
            }
            if (!isColAdded)
                divParent.AddCssClass("col");

            if (!_displaySource.IsBlank())
            {
                divParent.MergeAttribute("data-display-source", _displaySource);
                divParent.MergeAttribute("data-display-condition", _displayCondition);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredAttributes(_displaySource, _displayCondition);
            }

            if (!_displaySourceExp.IsBlank())
            {
                divParent.MergeAttribute("data-display-source-exp", _displaySourceExp);
                divParent.MergeAttribute("data-display-expression", _displayExp);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredExpressionAttributes(_displaySourceExp, _displayExp);
            }

            if (_title.IsBlank())
                _title = _id;

            TagBuilder divContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
            {
                divContainer.AddCssClass("form-group m-form__group");

                if (!_isTitleHidden)
                {
                    TagBuilder label = new TagBuilder("label");
                    label.MergeAttribute("for", _id);
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                divContainer.InnerHtml += _htmlHelper.TextArea(_id, _value == null ? "" : _value.ToString(), _rows, _cols, attributes).ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();

                divContainer.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    TagBuilder spanHelpText = new TagBuilder("span");
                    spanHelpText.AddCssClass("m-form__help");
                    spanHelpText.InnerHtml = _helpText;
                    divContainer.InnerHtml += spanHelpText.ToString();
                }
            }
            else
            {
                divContainer.AddCssClass("form-group m-form__group row");

                if (!_isTitleHidden)
                {
                    TagBuilder label = new TagBuilder("label");
                    label.AddCssClass("col-form-label col-lg-2 col-sm-12");
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : "col-lg-10 ")}col-sm-12");
                divInput.InnerHtml += _htmlHelper.TextArea(_id, _value == null ? "" : _value.ToString(), _rows, _cols, attributes).ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();
                divInput.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    TagBuilder spanHelpText = new TagBuilder("span");
                    spanHelpText.AddCssClass("m-form__help");
                    spanHelpText.InnerHtml = _helpText;
                    divInput.InnerHtml += spanHelpText.ToString();
                }
                divContainer.InnerHtml += divInput.ToString();
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();
        }
    }
}