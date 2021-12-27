using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Enum
{
    public class BSEnumDropDownList<TEnum> : IBSEnumDropDownList<TEnum>, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private LayoutType _layoutType;

        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private object _selectedValue;
        private string _title;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _optionLabel;
        private string _functionName;
        private object _htmlAttributes;

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
        public BSEnumDropDownList(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
            _isDisable = false;
        }
        #endregion

        #region fluent methods
        public IBSEnumDropDownList<TEnum> SetTitle(string customTitle)
        {
            this._title = customTitle;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetSelectedValue(object selectedValue)
        {
            this._selectedValue = selectedValue;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetOptionLabel(string optionLabel)
        {
            this._optionLabel = optionLabel;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSEnumDropDownList<TEnum> SetCallBackFunction(string functionName)
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

            var enumType = Util.GetNonNullableModelType(typeof(TEnum));
            var values = System.Enum.GetValues(enumType).Cast<TEnum>();
            bool isNullable = Util.IsNullable(typeof(TEnum));
            List<SelectListItem> items = new List<SelectListItem>();

            if (_optionLabel.IsBlank())
            {
                _optionLabel = "Seçiniz";
            }

            if (isNullable)
            {
                items.Add(new SelectListItem() { Text = _optionLabel, Value = "", Selected = false });
                if (_selectedValue != null)
                {
                    items.AddRange(values.Select(s => new SelectListItem { Text = Util.GetEnumDescription(s), Value = Convert.ToInt32(s).ToString(), Selected = s.Equals(_selectedValue) }));
                }
                else
                {
                    items[0].Selected = true;
                    items.AddRange(values.Select(s => new SelectListItem { Text = Util.GetEnumDescription(s), Value = Convert.ToInt32(s).ToString() }));
                }
            }
            else
            {
                items.AddRange(values.Select(s => new SelectListItem { Text = Util.GetEnumDescription(s), Value = Convert.ToInt32(s).ToString(), Selected = s.Equals(_selectedValue) }));
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
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, items, attributes).ToString();
                else
                    divContainer.InnerHtml += _htmlHelper.DropDownList(_id, items, _optionLabel, attributes).ToString();

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

                if (_optionLabel.IsBlank())
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, items, attributes).ToString();
                else
                    divInput.InnerHtml += _htmlHelper.DropDownList(_id, items, _optionLabel, attributes).ToString();

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