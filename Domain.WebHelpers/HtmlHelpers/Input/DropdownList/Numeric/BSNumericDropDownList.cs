using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Numeric
{
    public class BSNumericDropDownList : IBSNumericDropDownList,IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private object _htmlAttributes;
        private LayoutType _layoutType;

        private int _minValue;
        private int _maxValue;
        private int _incrementValue;

        private bool _isDisable;
        private bool _isSelectedValue;
        private bool _isTitleHidden;

        private string _id;
        private string _title;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _optionLabel;
        private string _functionName;
        private int _selectedValue;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private bool _isAlsoRequired;
        private string _bindProp;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        #endregion

        #region ctor
        public BSNumericDropDownList(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
            this._minValue = 1;
            this._maxValue = 100;
            this._incrementValue = 1;
        }
        #endregion

        #region fluent methods
        public IBSNumericDropDownList SetMinValue(int minValue = 0)
        {
            this._minValue = minValue;
            return this;
        }
        public IBSNumericDropDownList SetMaxValue(int maxValue = 100)
        {
            this._maxValue = maxValue;
            return this;
        }
        public IBSNumericDropDownList SetIncrementValue(int incrementValue = 1)
        {
            this._incrementValue = incrementValue;
            return this;
        }
        public IBSNumericDropDownList SetTitle(string customTitle)
        {
            this._title = customTitle;
            return this;
        }
        public IBSNumericDropDownList SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSNumericDropDownList SetSelectedValue(int selectedValue)
        {
            _isSelectedValue = true;
            this._selectedValue = selectedValue;
            return this;
        }
        public IBSNumericDropDownList SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSNumericDropDownList SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSNumericDropDownList SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSNumericDropDownList SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSNumericDropDownList SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSNumericDropDownList SetOptionLabel(string optionLabel)
        {
            this._optionLabel = optionLabel;
            return this;
        }
        public IBSNumericDropDownList SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSNumericDropDownList SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSNumericDropDownList SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSNumericDropDownList SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSNumericDropDownList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSNumericDropDownList SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSNumericDropDownList SetCallBackFunction(string functionName)
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

            List<SelectListItem> selectList = new List<SelectListItem>();
            for (int i = _minValue; i <= _maxValue; i += _incrementValue)
            {
                if (_isSelectedValue && i == _selectedValue)
                {
                    selectList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                }
                else
                {
                    selectList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
                }
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
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, selectList, attributes).ToString();
                else
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, selectList, _optionLabel, attributes).ToString();

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
                    label.AddCssClass("col-form-label col-lg-3 col-sm-12");
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : "col-lg-9 ")}col-sm-12");

                if (_optionLabel.IsBlank())
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, selectList, attributes).ToString();
                else
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, selectList, _optionLabel, attributes).ToString();

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