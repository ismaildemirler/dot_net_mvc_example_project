using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base
{
    public class BSDropDownList : IBSDropDownList, IHtmlString
    {
        #region private properties
        HtmlHelper _htmlHelper;
        IEnumerable<SelectListItem> _selectList;
        LayoutType _layoutType;

        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private string _title;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _functionName;
        private string _optionLabel;
        private object _htmlAttributes;
        private object _selectedValue;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private string _bindProp;

        private string _relatedInputID;
        private string _ajaxUrl;
        private string _callBackFunction;
        private int _labelWidth = 2;
        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        private string _requiredSourceExp;
        private string _requiredExp;
        #endregion

        #region ctor
        public BSDropDownList(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
        }
        #endregion

        #region fluent methods
        public IBSDropDownList SetTitle(string title)
        {
            this._title = title;
            return this;
        }
        public IBSDropDownList SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDropDownList SetSelectedValue(object selectedValue)
        {
            this._selectedValue = selectedValue;
            return this;
        }
        public IBSDropDownList SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDropDownList SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSDropDownList SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSDropDownList SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSDropDownList SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSDropDownList SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }
        public IBSDropDownList SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            this._selectList = selectList;
            return this;
        }
        public IBSDropDownList SetOptionLabel(string optionLabel)
        {
            this._optionLabel = optionLabel;
            return this;
        }
        public IBSDropDownList SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSDropDownList SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSDropDownList SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSDropDownList SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDropDownList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDropDownList SetRelatedInput(string id, string ajaxUrl, string callBackFunction = "")
        {
            this._relatedInputID = id;
            this._ajaxUrl = ajaxUrl;
            this._callBackFunction = callBackFunction;
            return this;
        }
        public IBSDropDownList SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSDropDownList SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input ddl ddl-select" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            if (!_relatedInputID.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_related_input_id = _relatedInputID, data_related_ajax_url = _ajaxUrl, data_related_callback = _callBackFunction });

            if (_selectList == null)
            {
                _selectList = Enumerable.Empty<SelectListItem>();
            }

            if (_selectedValue != null)
            {
                _selectList = new SelectList(_selectList, "value", "text", _selectedValue);
                attributes = Util.MergeHtmlAttributes(attributes, new { data_init_value = _selectedValue });
            }

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

                if (_optionLabel.IsBlank())
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, _selectList, attributes).ToString();
                else
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, _selectList, _optionLabel, attributes).ToString();

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
                    label.AddCssClass($"col-form-label col-lg-{_labelWidth} col-sm-12");
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");

                if (_optionLabel.IsBlank())
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, _selectList, attributes).ToString();
                else
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, _selectList, _optionLabel, attributes).ToString();

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