using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.DateRangePicker
{
    public class BSDateRangePicker : IBSDateRangePicker, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private LayoutType _layoutType;
        private DateType _dateType;
        private string _parentDivCSS;

        private bool _isReadOnly;

        private DateTime _startDateValue;
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

        private DateTime _endDateValue;
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

        #region ctor
        public BSDateRangePicker(HtmlHelper htmlHelper, string startDateId, string endDateId)
        {
            this._htmlHelper = htmlHelper;
            this._startDateId = startDateId;
            this._isStartDateDisable = false;
            this._endDateId = endDateId;
            this._isEndDateDisable = false;
        }
        #endregion

        #region fluent methods
        public IBSDateRangePicker SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSDateRangePicker SetDateType(DateType dateType)
        {
            this._dateType = dateType;
            return this;
        }
        public IBSDateRangePicker SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                this._parentDivCSS = css;
            }
            return this;
        }

        public IBSDateRangePicker SetStartDateTitle(string title)
        {
            this._startDateTitle = title;
            return this;
        }
        public IBSDateRangePicker SetStartDateHtmlAttributes(object htmlAttributes)
        {
            this._startDateHtmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDateRangePicker SetStartDateMin(string minDate)
        {
            this._startDateMin = minDate;
            return this;
        }
        public IBSDateRangePicker SetStartDateMax(string maxDate)
        {
            this._startDateMax = maxDate;
            return this;
        }
        public IBSDateRangePicker SetStartDateTooltip(string tooltip)
        {
            this._startDateTooltip = tooltip;
            return this;
        }
        public IBSDateRangePicker SetStartDateHelpText(string helpText)
        {
            this._startDateHelpText = helpText;
            return this;
        }
        public IBSDateRangePicker SetStartDateValue(DateTime value)
        {
            this._startDateValue = value;
            return this;
        }
        public IBSDateRangePicker SetStartDateDisable(bool isDisable)
        {
            this._isStartDateDisable = isDisable;
            return this;
        }
        public IBSDateRangePicker SetStartDateTitleHidden(bool isHidden)
        {
            this._isStartDateTitleHidden = isHidden;
            return this;
        }
        public IBSDateRangePicker SetStartDateCallBackFunction(string functionName)
        {
            this._startDateFunctionName = functionName;
            return this;
        }

        public IBSDateRangePicker SetEndDateTitle(string title)
        {
            this._endDateTitle = title;
            return this;
        }
        public IBSDateRangePicker SetEndDateHtmlAttributes(object htmlAttributes)
        {
            this._endDateHtmlAttributes = htmlAttributes;
            return this;
        }
        public IBSDateRangePicker SetEndDateMin(string minDate)
        {
            this._endDateMin = minDate;
            return this;
        }
        public IBSDateRangePicker SetEndDateMax(string maxDate)
        {
            this._endDateMax = maxDate;
            return this;
        }
        public IBSDateRangePicker SetEndDateTooltip(string tooltip)
        {
            this._endDateTooltip = tooltip;
            return this;
        }
        public IBSDateRangePicker SetEndDateHelpText(string helpText)
        {
            this._endDateHelpText = helpText;
            return this;
        }
        public IBSDateRangePicker SetEndDateValue(DateTime value)
        {
            this._endDateValue = value;
            return this;
        }
        public IBSDateRangePicker SetEndDateDisable(bool isDisable)
        {
            this._isEndDateDisable = isDisable;
            return this;
        }
        public IBSDateRangePicker SetEndDateTitleHidden(bool isHidden)
        {
            this._isEndDateTitleHidden = isHidden;
            return this;
        }
        public IBSDateRangePicker SetEndDateCallBackFunction(string functionName)
        {
            this._endDateFunctionName = functionName;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            var attributesStartDate = Util.MergeHtmlAttributes(_startDateHtmlAttributes, new { @class = "form-control datepicker dateRangePickerStart" });
            attributesStartDate = attributesStartDate.SetTooltipAttributes(_startDateTooltip);
            attributesStartDate = attributesStartDate.SetDisableAttributes(_isStartDateDisable);
            attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_enddateelement = _endDateId });
            if (!_startDateFunctionName.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_callback = _startDateFunctionName }); }
            if (!_startDateMin.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_date_val_min = _startDateMin }); }
            if (!_startDateMax.IsBlank()) { attributesStartDate = Util.MergeHtmlAttributes(attributesStartDate, new { data_date_val_max = _startDateMax }); }

            var attributesEndDate = Util.MergeHtmlAttributes(_endDateHtmlAttributes, new { @class = "form-control datepicker dateRangePickerEnd" });
            attributesEndDate = attributesEndDate.SetTooltipAttributes(_endDateTooltip);
            attributesEndDate = attributesEndDate.SetDisableAttributes(_isEndDateDisable);
            attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_startdateelement = _startDateId });
            if (!_endDateFunctionName.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_callback = _endDateFunctionName }); }
            if (!_endDateMin.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_date_val_min = _endDateMin }); }
            if (!_endDateMax.IsBlank()) { attributesEndDate = Util.MergeHtmlAttributes(attributesEndDate, new { data_date_val_max = _endDateMax }); }

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

            if (_startDateTitle.IsBlank())
                _startDateTitle = _startDateId;

            if (_endDateTitle.IsBlank())
                _endDateTitle = _endDateId;

            if (_layoutType == LayoutType.vertical)
            {
                divColStart.AddCssClass("col");
                divColStart.InnerHtml = GetDateRangeHelperVertical(_startDateId, _startDateTitle, _startDateValue, attributesStartDate, _startDateHelpText, _isStartDateTitleHidden);

                divColEnd.AddCssClass("col");
                divColEnd.InnerHtml = GetDateRangeHelperVertical(_endDateId, _endDateTitle, _endDateValue, attributesEndDate, _endDateHelpText, _isEndDateTitleHidden);
            }
            else
            {
                divColStart.AddCssClass("col-sm-12");
                divColStart.InnerHtml = GetDateRangeHelperHorizontal(_startDateId, _startDateTitle, _startDateValue, attributesStartDate, _startDateHelpText, _isStartDateTitleHidden);

                divColEnd.AddCssClass("col-sm-12");
                divColEnd.InnerHtml = GetDateRangeHelperHorizontal(_endDateId, _endDateTitle, _endDateValue, attributesEndDate, _endDateHelpText, _isEndDateTitleHidden);
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

            if(!isTitleHidden)
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

            if(!isTitleHidden)
            {
            TagBuilder label = new TagBuilder("label");
            label.AddCssClass("col-form-label col-lg-3 col-sm-12");
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
            divInput.AddCssClass($"{(isTitleHidden ? "" : "col-lg-9 ")}col-sm-12");
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

            return divContainer.ToString();
        }
    }

}