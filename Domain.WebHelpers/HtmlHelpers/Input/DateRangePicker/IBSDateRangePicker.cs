using System;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DateRangePicker
{
    public interface IBSDateRangePicker
    {
        IBSDateRangePicker SetLayout(LayoutType layoutType);
        IBSDateRangePicker SetDateType(DateType dateType);
        IBSDateRangePicker SetParentCss(string css);

        IBSDateRangePicker SetStartDateTitle(string title);
        IBSDateRangePicker SetStartDateHtmlAttributes(object htmlAttributes);
        IBSDateRangePicker SetStartDateMin(string minDate);
        IBSDateRangePicker SetStartDateMax(string maxDate);
        IBSDateRangePicker SetStartDateTooltip(string tooltip);
        IBSDateRangePicker SetStartDateHelpText(string helpText);
        IBSDateRangePicker SetStartDateValue(DateTime value);
        IBSDateRangePicker SetStartDateDisable(bool isDisable);
        IBSDateRangePicker SetStartDateTitleHidden(bool isHidden);
        IBSDateRangePicker SetStartDateCallBackFunction(string functionName);

        IBSDateRangePicker SetEndDateTitle(string title);
        IBSDateRangePicker SetEndDateHtmlAttributes(object htmlAttributes);
        IBSDateRangePicker SetEndDateMin(string minDate);
        IBSDateRangePicker SetEndDateMax(string maxDate);
        IBSDateRangePicker SetEndDateTooltip(string tooltip);
        IBSDateRangePicker SetEndDateHelpText(string helpText);
        IBSDateRangePicker SetEndDateValue(DateTime value);
        IBSDateRangePicker SetEndDateDisable(bool isDisable);
        IBSDateRangePicker SetEndDateTitleHidden(bool isHidden);
        IBSDateRangePicker SetEndDateCallBackFunction(string functionName);
    }
}
