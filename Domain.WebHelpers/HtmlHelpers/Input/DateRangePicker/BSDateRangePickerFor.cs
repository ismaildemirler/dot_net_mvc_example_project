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
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DateRangePicker
{
    public class BSDateRangePickerFor<TModel, TProperty> : IBSDateRangePickerFor<TModel>, IHtmlString
    {
        #region private properties
        private LayoutType _layoutType;
        private DateType _dateType;
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expressionStart;
        private Expression<Func<TModel, TProperty>> _expressionEnd;
        private string _parentDivCSS;

        private bool _isReadOnly;

        private string _startDateId;
        private string _startDateTitle;
        private string _startDateHelpText;
        private string _startDateTooltip;
        private object _startDateHtmlAttributes;
        private string _startDateMax;
        private string _startDateMin;
        private string _startDateFunctionName;
        private bool _isStartDateDisable;
        private bool _isStartDateTitleHidden;

        private string _endDateId;
        private string _endDateTitle;
        private string _endDateHelpText;
        private string _endDateTooltip;
        private object _endDateHtmlAttributes;
        private string _endDateMax;
        private string _endDateMin;
        private string _endDateFunctionName;
        private bool _isEndDateDisable;
        private bool _isEndDateTitleHidden;
        #endregion

        public BSDateRangePickerFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expressionStart, Expression<Func<TModel, TProperty>> expressionEnd)
        {
            this._startDateId = "";
            this._endDateId = "";

            this._htmlHelper = htmlHelper;

            this._expressionStart = expressionStart;
            this._expressionEnd = expressionEnd;

            this._isStartDateDisable = false;
            this._isEndDateDisable = false;
        }

        #region fluent methods
        public IBSDateRangePickerFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetDateType(DateType dateType)
        {
            this._dateType = dateType;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                this._parentDivCSS = css;
            }
            return this;
        }

        public IBSDateRangePickerFor<TModel> SetStartDateCustomTitle(string title)
        {
            this._startDateTitle = title;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateHtmlAttributes(object htmlAttributes)
        {
            this._startDateHtmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateMin(DateTime minDate)
        {
            this._startDateMin = minDate.ToString("u");
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateMax(DateTime maxDate)
        {
            this._startDateMax = maxDate.ToString("u");
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateTooltip(string tooltip)
        {
            this._startDateTooltip = tooltip;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateHelpText(string helpText)
        {
            this._startDateHelpText = helpText;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateDisable(bool isDisable)
        {
            this._isStartDateDisable = isDisable;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateTitleHidden(bool isHidden)
        {
            this._isStartDateTitleHidden = isHidden;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetStartDateCallBackFunction(string functionName)
        {
            this._startDateFunctionName = functionName;
            return this;
        }

        public IBSDateRangePickerFor<TModel> SetEndDateCustomTitle(string title)
        {
            this._endDateTitle = title;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateHtmlAttributes(object htmlAttributes)
        {
            this._endDateHtmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateMin(DateTime minDate)
        {
            this._endDateMin = minDate.ToString("u");
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateMax(DateTime maxDate)
        {
            this._endDateMax = maxDate.ToString("u");
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateTooltip(string tooltip)
        {
            this._endDateTooltip = tooltip;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateHelpText(string helpText)
        {
            this._endDateHelpText = helpText;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateDisable(bool isDisable)
        {
            this._isEndDateDisable = isDisable;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateTitleHidden(bool isHidden)
        {
            this._isEndDateTitleHidden = isHidden;
            return this;
        }
        public IBSDateRangePickerFor<TModel> SetEndDateCallBackFunction(string functionName)
        {
            this._endDateFunctionName = functionName;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadataStartDate = ModelMetadata.FromLambdaExpression(_expressionStart, _htmlHelper.ViewData);
            if (_startDateTitle.IsBlank())
                _startDateTitle = metadataStartDate.DisplayName ?? metadataStartDate.PropertyName;

            if (_startDateId.IsBlank())
                _startDateId = metadataStartDate.PropertyName;

            ModelMetadata metadataEndDate = ModelMetadata.FromLambdaExpression(_expressionEnd, _htmlHelper.ViewData);
            if (_endDateTitle.IsBlank())
                _endDateTitle = metadataEndDate.DisplayName ?? metadataEndDate.PropertyName;

            if (_endDateId.IsBlank())
                _endDateId = metadataEndDate.PropertyName;

            var attributesStartDate = Util.MergeHtmlAttributes(_startDateHtmlAttributes, new { @class = "form-control datepicker dateRangePickerStart" });
            attributesStartDate = attributesStartDate.SetTooltipAttributes(_startDateTooltip);
            attributesStartDate = attributesStartDate.SetDisableAttributes(_isStartDateDisable);
            attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_enddateelement = _endDateId });
            attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { autocomplete = "off" });
            if (!_startDateFunctionName.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_callback = _startDateFunctionName }); }
            if (!_startDateMin.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_date_val_min = _startDateMin }); }
            if (!_startDateMax.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_date_val_max = _startDateMax }); }

            if (!_startDateId.IsBlank())
                attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { id = _startDateId });
            if (!_endDateId.IsBlank())
                attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { id = _endDateId });

            var propInfoStartDate = Util.GetPropertyInfo(_expressionStart);
            var displayStartDate = (DisplayAttribute)propInfoStartDate.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
            if (displayStartDate != null && _startDateHelpText.IsBlank())
                _startDateHelpText = displayStartDate.Description;

            var attributesEndDate = Util.MergeHtmlAttributes(_endDateHtmlAttributes, new { @class = "form-control datepicker dateRangePickerEnd" });
            attributesEndDate = attributesEndDate.SetTooltipAttributes(_endDateTooltip);
            attributesEndDate = attributesEndDate.SetDisableAttributes(_isEndDateDisable);
            attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_startdateelement = _startDateId });
            attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { autocomplete = "off" });
            if (!_endDateFunctionName.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_callback = _endDateFunctionName }); }
            if (!_endDateMin.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_date_val_min = _endDateMin }); }
            if (!_endDateMax.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_date_val_max = _endDateMax }); }


            var valueStartDate = DateTime.Parse(ModelMetadata.FromLambdaExpression(_expressionStart, _htmlHelper.ViewData).Model.ToString());
            var valueEndDate = DateTime.Parse(ModelMetadata.FromLambdaExpression(_expressionEnd, _htmlHelper.ViewData).Model.ToString());

            var propInfoEndDate = Util.GetPropertyInfo(_expressionEnd);
            var displayEndDate = (DisplayAttribute)propInfoEndDate.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (displayEndDate != null && _endDateHelpText.IsBlank())
                _endDateHelpText = displayEndDate.Description;

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

            TagBuilder divRow = new TagBuilder("div");
            divRow.AddCssClass("row");

            TagBuilder divColStart = new TagBuilder("div");
            TagBuilder divColEnd = new TagBuilder("div");

            if (_layoutType == LayoutType.vertical)
            {
                divColStart.AddCssClass("col");
                divColStart.InnerHtml = GetDateRangeHelperVertical(_startDateId, _startDateTitle, valueStartDate, attributesStartDate, _startDateHelpText, _isStartDateTitleHidden);

                divColEnd.AddCssClass("col");
                divColEnd.InnerHtml = GetDateRangeHelperVertical(_endDateId, _endDateTitle, valueEndDate, attributesEndDate, _endDateHelpText, _isEndDateTitleHidden);
            }
            else
            {
                divColStart.AddCssClass("col-sm-12");
                divColStart.InnerHtml = GetDateRangeHelperHorizontal(_startDateId, _startDateTitle, valueStartDate, attributesStartDate, _startDateHelpText, _isStartDateTitleHidden);

                divColEnd.AddCssClass("col-sm-12 mt-2s");
                divColEnd.InnerHtml = GetDateRangeHelperHorizontal(_endDateId, _endDateTitle, valueEndDate, attributesEndDate, _endDateHelpText, _isEndDateTitleHidden);
            }

            divRow.InnerHtml = divColStart.ToString();
            divRow.InnerHtml += divColEnd.ToString();

            divParent.InnerHtml = divRow.ToString();
            return divParent.ToString();
        }

        private string GetDateRangeHelperVertical(string id, string title, DateTime value, IDictionary<string, object> htmlAttributes, string helpText, bool isTitleHidden)
        {
            TagBuilder divContainer = new TagBuilder("div");
            divContainer.AddCssClass("form-group m-form__group");

            if (!isTitleHidden)
            {
                TagBuilder label = new TagBuilder("label");
                label.MergeAttribute("for", id);
                label.SetInnerText(title);
                divContainer.InnerHtml = label.ToString();
            }

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

            htmlAttributes = Util.MergeHtmlAttributes(htmlAttributes, new { data_dateformat = strFormat });
            divContainer.InnerHtml += "<div class=\"input-group mb-3\">";
            divContainer.InnerHtml += _htmlHelper.TextBox(id, valueStr, htmlAttributes).ToString();
            divContainer.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";

            TagBuilder divValidation = new TagBuilder("div");
            divValidation.InnerHtml += _htmlHelper.ValidationMessage(id).ToString();

            divContainer.InnerHtml += divValidation.ToString();

            if (!helpText.IsBlank())
            {
                TagBuilder spanHelpText = new TagBuilder("span");
                spanHelpText.AddCssClass("m-form__help");
                spanHelpText.InnerHtml = helpText;
                divContainer.InnerHtml += spanHelpText.ToString();
            }

            return divContainer.ToString();
        }
        private string GetDateRangeHelperHorizontal(string id, string title, DateTime value, IDictionary<string, object> htmlAttributes, string helpText, bool isTitleHidden)
        {
            TagBuilder divContainer = new TagBuilder("div");
            divContainer.AddCssClass("form-group m-form__group row");

            if (!isTitleHidden)
            {
                TagBuilder label = new TagBuilder("label");
                label.AddCssClass("col-form-label col-lg-2 col-sm-12");
                label.SetInnerText(title);
                divContainer.InnerHtml = label.ToString();
            }

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

            htmlAttributes = Util.MergeHtmlAttributes(htmlAttributes, new { data_dateformat = strFormat });

            TagBuilder divInput = new TagBuilder("div");
            divInput.AddCssClass($"{(isTitleHidden ? "" : "col-lg-10 ")}col-sm-12");
            divInput.InnerHtml += "<div class=\"input-group mb-3\">";
            divInput.InnerHtml += _htmlHelper.TextBox(id, valueStr, htmlAttributes).ToString();
            divInput.InnerHtml += "<div class=\"input-group-append\"><span class=\"input-group-text\"><i class=\"fa fa-calendar\"></i></span></div></div>";

            TagBuilder divValidation = new TagBuilder("div");
            divValidation.InnerHtml += _htmlHelper.ValidationMessage(id).ToString();
            divInput.InnerHtml += divValidation.ToString();

            if (!helpText.IsBlank())
            {
                TagBuilder spanHelpText = new TagBuilder("span");
                spanHelpText.AddCssClass("m-form__help");
                spanHelpText.InnerHtml = helpText;
                divInput.InnerHtml += spanHelpText.ToString();
            }

            divContainer.InnerHtml += divInput.ToString();
            return divContainer.ToString();
        }

    }
}