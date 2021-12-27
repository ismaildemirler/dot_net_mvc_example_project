using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker
{
    public class BSDateTimePickerFor<TModel, TProperty> : IBSDatetimePickerFor<TModel>, IHtmlString
    {
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;
        private LayoutType _layoutType;
        private DateType _dateType;
        private int _labelWidth;

        private bool _isReadOnly;
        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private string _customTitle;
        private object _htmlAttributes;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _functionName;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private bool _isAlsoRequired;
        private string _bindProp;
        private string _maxDate;
        private string _minDate;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;

        public BSDateTimePickerFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this._id = "";
            this._expression = expression;
            this._htmlHelper = htmlHelper;
            this._isReadOnly = false;
            this._isDisable = false;
            this._labelWidth = 2;
        }

        public IBSDatetimePickerFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetDateType(DateType dateType)
        {
            this._dateType = dateType;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetReadOnly(bool isReadOnly)
        {
            this._isReadOnly = isReadOnly;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetMinDate(string minDate)
        {
            this._minDate = minDate;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetMaxDate(string maxDate)
        {
            this._maxDate = maxDate;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDatetimePickerFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }

        public IBSDatetimePickerFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }

        public IBSDatetimePickerFor<TModel> SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control datepicker" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);
            attributes = attributes.SetReadOnlyAttributes(_isReadOnly);

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            if (!_minDate.IsBlank()) { attributes = Util.MergeHtmlAttributes(attributes, new { data_date_val_min = _minDate }); }
            if (!_maxDate.IsBlank()) { attributes = Util.MergeHtmlAttributes(attributes, new { data_date_val_max = _maxDate }); }

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            DateTime value = DateTime.MinValue;
            if (ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData).Model != null)
                value = DateTime.Parse(ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData).Model.ToString());

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

            string strFormat = "L";
            var valueStr = (value == null || value == DateTime.MinValue ? "" : value.ToString("dd.MM.yyyy"));

            if (_dateType == DateType.DatetimeLocal)
            {
                strFormat = "";
                valueStr = (value == null || value == DateTime.MinValue ? "" : value.ToString());
            }
            else if (_dateType == DateType.Month)
            {
                strFormat = "MM/YYYY";
                valueStr = (value == null || value == DateTime.MinValue ? "" : value.ToString("MM.yyyy"));
            }
            else if (_dateType == DateType.Time)
            {
                strFormat = "HH:mm";
                valueStr = (value == null || value == DateTime.MinValue ? "" : value.ToString("HH:mm"));
            }

            attributes = Util.MergeHtmlAttributes(attributes, new { data_dateformat = strFormat });
            attributes = Util.MergeHtmlAttributes(attributes, new { autocomplete = "off" });

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
                divContainer.InnerHtml += "<div class=\"input-group mb-3\">";

                attributes = Util.MergeHtmlAttributes(attributes, new { @Value = valueStr == null ? "" : valueStr.ToString() });
                
                divContainer.InnerHtml += _htmlHelper.TextBoxFor(_expression, attributes).ToString();
                divContainer.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";

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
                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");
                divInput.InnerHtml += "<div class=\"input-group mb-3\">";
                //divInput.InnerHtml += _htmlHelper.TextBox(_id, valueStr, attributes).ToString();
                attributes = Util.MergeHtmlAttributes(attributes, new { @Value = valueStr == null ? "" : valueStr.ToString() });

                divInput.InnerHtml += _htmlHelper.TextBoxFor(_expression, attributes).ToString();

                divInput.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";
             

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