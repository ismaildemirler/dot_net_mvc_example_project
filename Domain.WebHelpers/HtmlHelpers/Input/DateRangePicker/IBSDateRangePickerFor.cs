using System;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DateRangePicker
{
    public interface IBSDateRangePickerFor<TModel>
    {
        IBSDateRangePickerFor<TModel> SetLayout(LayoutType layoutType);
        IBSDateRangePickerFor<TModel> SetDateType(DateType dateType);
        IBSDateRangePickerFor<TModel> SetParentCss(string css);

        IBSDateRangePickerFor<TModel> SetStartDateCustomTitle(string title);
        IBSDateRangePickerFor<TModel> SetStartDateHtmlAttributes(object htmlAttributes);
        IBSDateRangePickerFor<TModel> SetStartDateMin(DateTime minDate);
        IBSDateRangePickerFor<TModel> SetStartDateMax(DateTime maxDate);
        IBSDateRangePickerFor<TModel> SetStartDateTooltip(string tooltip);
        IBSDateRangePickerFor<TModel> SetStartDateHelpText(string helpText);
        IBSDateRangePickerFor<TModel> SetStartDateDisable(bool isDisable);
        IBSDateRangePickerFor<TModel> SetStartDateTitleHidden(bool isHidden);
        IBSDateRangePickerFor<TModel> SetStartDateCallBackFunction(string functionName);

        IBSDateRangePickerFor<TModel> SetEndDateCustomTitle(string title);
        IBSDateRangePickerFor<TModel> SetEndDateHtmlAttributes(object htmlAttributes);
        IBSDateRangePickerFor<TModel> SetEndDateMin(DateTime minDate);
        IBSDateRangePickerFor<TModel> SetEndDateMax(DateTime maxDate);
        IBSDateRangePickerFor<TModel> SetEndDateTooltip(string tooltip);
        IBSDateRangePickerFor<TModel> SetEndDateHelpText(string helpText);
        IBSDateRangePickerFor<TModel> SetEndDateDisable(bool isDisable);
        IBSDateRangePickerFor<TModel> SetEndDateTitleHidden(bool isHidden);
        IBSDateRangePickerFor<TModel> SetEndDateCallBackFunction(string functionName);
    }
}
