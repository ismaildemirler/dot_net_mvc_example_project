using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker
{
    public class BSDateTimePicker : IBSDatetimePicker, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private LayoutType _layoutType;
        private DateType _dateType;
        private int _labelWidth;

        private bool _isReadOnly;
        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private DateTime _value;
        private string _title;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _functionName;
        private object _htmlAttributes;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private string _bindProp;
        private string _maxDate;
        private string _minDate;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        #endregion

        #region ctor
        public BSDateTimePicker(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
            this._isReadOnly = false;
            this._isDisable = false;
            this._labelWidth = 2;
        }
        #endregion

        #region fluent methods
        public IBSDatetimePicker SetValue(DateTime value)
        {
            this._value = value;
            return this;
        }
        public IBSDatetimePicker SetTitle(string title)
        {
            this._title = title;
            return this;
        }
        public IBSDatetimePicker SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDatetimePicker SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDatetimePicker SetDateType(DateType dateType)
        {
            this._dateType = dateType;
            return this;
        }
        public IBSDatetimePicker SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSDatetimePicker SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSDatetimePicker SetReadOnly(bool isReadOnly)
        {
            this._isReadOnly = isReadOnly;
            return this;
        }
        public IBSDatetimePicker SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSDatetimePicker SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                this._parentDivCSS = css;
            }
            return this;
        }
        public IBSDatetimePicker SetMinDate(string minDate)
        {
            this._minDate = minDate;
            return this;
        }
        public IBSDatetimePicker SetMaxDate(string maxDate)
        {
            this._maxDate = maxDate;
            return this;
        }
        public IBSDatetimePicker SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSDatetimePicker SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSDatetimePicker SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSDatetimePicker SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDatetimePicker SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSDatetimePicker SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSDatetimePicker SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }
        public IBSDatetimePicker SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
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

            string strFormat = "L";
            var valueStr = (_value == null || _value == DateTime.MinValue ? "" : _value.ToString("dd.MM.yyyy"));

            if (_dateType == DateType.DatetimeLocal)
            {
                strFormat = "";
                valueStr = (_value == null || _value == DateTime.MinValue ? "" : _value.ToString());
            }
            else if (_dateType == DateType.Month)
            {
                strFormat = "MM/YYYY";
                valueStr = (_value == null || _value == DateTime.MinValue ? "" : _value.ToString("MM.yyyy"));
            }
            else if (_dateType == DateType.Time)
            {
                strFormat = "HH:mm";
                valueStr = (_value == null || _value == DateTime.MinValue ? "" : _value.ToString("HH:mm"));
            }

            attributes = Util.MergeHtmlAttributes(attributes, new { data_dateformat = strFormat });

            if (_title.IsBlank())
                _title = _id;

            TagBuilder divContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
            {
                divContainer.AddCssClass("form-group m-form__group");

                if(!_isTitleHidden)
                {
                TagBuilder label = new TagBuilder("label");
                label.MergeAttribute("for", _id);
                label.SetInnerText(_title);
                divContainer.InnerHtml = label.ToString();
                }
                divContainer.InnerHtml += "<div class=\"input-group mb-3\">";
                divContainer.InnerHtml += _htmlHelper.TextBox(_id, valueStr, attributes).ToString();
                divContainer.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";

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

                if(!_isTitleHidden)
                {
                TagBuilder label = new TagBuilder("label");
                label.AddCssClass($"col-form-label col-lg-{_labelWidth} col-sm-12");
                label.SetInnerText(_title);
                divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");
                divInput.InnerHtml += "<div class=\"input-group mb-3\">";
                divInput.InnerHtml += _htmlHelper.TextBox(_id, _value == null ? "" : valueStr, attributes).ToString();
                divInput.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";

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