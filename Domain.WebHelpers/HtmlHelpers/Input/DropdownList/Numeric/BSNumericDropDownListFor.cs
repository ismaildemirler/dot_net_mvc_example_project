using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Numeric
{
    public class BSNumericDropDownListFor<TModel, TProperty> : IBSNumericDropDownListFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;
        private LayoutType _layoutType;

        private int _minValue;
        private int _maxValue;
        private int _incrementValue;

        private bool _isDisable;
        private bool _isSelectedValue;
        private bool _isTitleHidden;

        private string _id;
        private string _customTitle;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _optionLabel;
        private string _functionName;
        private object _htmlAttributes;
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
        public BSNumericDropDownListFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this._htmlHelper = htmlHelper;
            this._expression = expression;
            this._minValue = 0;
            this._maxValue = 100;
            this._incrementValue = 1;
        }
        #endregion

        #region fluent methods
        public IBSNumericDropDownListFor<TModel> SetMinValue(int minValue = 0)
        {
            this._minValue = minValue;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetMaxValue(int maxValue = 100)
        {
            this._maxValue = maxValue;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetIncrementValue(int incrementValue = 1)
        {
            this._incrementValue = incrementValue;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetSelectedValue(int selectedValue)
        {
            _isSelectedValue = true;
            this._selectedValue = selectedValue;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetOptionLabel(string optionLabel)
        {
            this._optionLabel = optionLabel;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSNumericDropDownListFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input ddl ddl-select" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            List<SelectListItem> selectList = new List<SelectListItem>();
            for (int i = _minValue; i <= _maxValue; i += _incrementValue)
            {
                if (_isSelectedValue && i == _selectedValue)
                    selectList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString(), Selected = true });
                else
                    selectList.Add(new SelectListItem() { Value = i.ToString(), Text = i.ToString() });
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

            if (_customTitle.IsBlank())
                _customTitle = metadata.DisplayName ?? metadata.PropertyName;

            if (_id.IsBlank())
                _id = metadata.PropertyName;

            System.Type type = typeof(TModel);
            var propInfo = Util.GetPropertyInfo(_expression);
            string name = propInfo.Name;
            var display = (DisplayAttribute)propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (display != null && _helpText.IsBlank())
                _helpText = display.Description;

            TagBuilder divContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
            {
                divContainer.AddCssClass("form-group m-form__group");

                if (!_isTitleHidden)
                {
                    TagBuilder label = new TagBuilder("label");
                    label.MergeAttribute("for", _id);
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                if (_optionLabel.IsBlank())
                    divContainer.InnerHtml += _htmlHelper.DropDownListFor(_expression, selectList, attributes).ToString();
                else
                    divContainer.InnerHtml += _htmlHelper.DropDownListFor(_expression, selectList, _optionLabel, attributes).ToString();


                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

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
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : "col-lg-10 ")}col-sm-12");

                if (_optionLabel.IsBlank())
                    divInput.InnerHtml += _htmlHelper.DropDownListFor(_expression, selectList, attributes).ToString();
                else
                    divInput.InnerHtml += _htmlHelper.DropDownListFor(_expression, selectList, _optionLabel, attributes).ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

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