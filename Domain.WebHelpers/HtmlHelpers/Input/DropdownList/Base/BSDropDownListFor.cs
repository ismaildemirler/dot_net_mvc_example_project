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

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base
{
    public class BSDropDownListFor<TModel, TProperty> : IBSDropDownListFor<TModel>, IHtmlString
    {
        #region private properties
        HtmlHelper<TModel> _htmlHelper;
        Expression<Func<TModel, TProperty>> _expression;
        IEnumerable<SelectListItem> _selectList;
        LayoutType _layoutType;

        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private string _customTitle;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _optionLabel;
        private string _functionName;
        private object _htmlAttributes;
        private object _selectedValue;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private bool _isAlsoRequired;
        private string _bindProp;

        private string _relatedInputID;
        private string _ajaxUrl;
        private string _callBackFunction;
        private int _labelWidth = 2;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        #endregion

        #region ctor
        public BSDropDownListFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this._expression = expression;
            this._htmlHelper = htmlHelper;
            this._isDisable = false;
            this._optionLabel = "";
            this._id = "";
        }
        #endregion

        #region fluent methods
        public IBSDropDownListFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }
        public IBSDropDownListFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSDropDownListFor<TModel> SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }
        public IBSDropDownListFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDropDownListFor<TModel> SetSelectedValue(object selectedValue)
        {
            this._selectedValue = selectedValue;
            return this;
        }
        public IBSDropDownListFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDropDownListFor<TModel> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSDropDownListFor<TModel> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSDropDownListFor<TModel> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSDropDownListFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSDropDownListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            this._selectList = selectList;
            return this;
        }
        public IBSDropDownListFor<TModel> SetOptionLabel(string optionLabel)
        {
            this._optionLabel = optionLabel;
            return this;
        }
        public IBSDropDownListFor<TModel> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSDropDownListFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSDropDownListFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSDropDownListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDropDownListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDropDownListFor<TModel> SetRelatedInput(string id, string ajaxUrl, string callBackFunction)
        {
            this._relatedInputID = id;
            this._ajaxUrl = ajaxUrl;
            this._callBackFunction = callBackFunction;
            return this;
        }
        public IBSDropDownListFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }

        public IBSDropDownListFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            //var modelValue = metadata.Model;
            //var value = "";

            //if (modelValue != null)
            //{
            //    if (modelValue.GetType() == typeof(int))
            //    {
            //        value = ((int)modelValue).ToString();
            //    }
            //    else if (modelValue.GetType() == typeof(string))
            //    {
            //        value = modelValue.ToString();
            //    }
            //}

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input ddl ddl-select" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);

            if (!_relatedInputID.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_related_input_id = _relatedInputID, data_related_ajax_url = _ajaxUrl, data_related_callback = _callBackFunction });

            if (!_relatedInputID.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_related_input_id = _relatedInputID, data_related_ajax_url = _ajaxUrl, data_related_callback = _callBackFunction });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

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

            if (_selectList == null)
                _selectList = Enumerable.Empty<SelectListItem>();

            var value = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData).Model;
            if (value != null)
            {
                _selectList.ToList().ForEach(w => w.Selected = false);

                if (_selectList.Any(w => w.Value == value.ToString()))
                    _selectList.FirstOrDefault(w => w.Value == value.ToString()).Selected = true;
                attributes = Util.MergeHtmlAttributes(attributes, new { data_init_value = value });
            }

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
                    divContainer.InnerHtml += _htmlHelper.DropDownListFor(_expression, _selectList, attributes).ToString();
                else
                    divContainer.InnerHtml += _htmlHelper.DropDownListFor(_expression, _selectList, _optionLabel, attributes).ToString();


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
                    label.AddCssClass($"col-form-label col-lg-{_labelWidth} col-sm-12");
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                int constantWidth = 12 - _labelWidth;
                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");

                if (_optionLabel.IsBlank())
                    divInput.InnerHtml += _htmlHelper.DropDownListFor(_expression, _selectList, attributes).ToString();
                else
                    divInput.InnerHtml += _htmlHelper.DropDownListFor(_expression, _selectList, _optionLabel, attributes).ToString();

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
