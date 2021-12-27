using System;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker
{
    public interface IBSDatetimePicker
    {
        IBSDatetimePicker SetValue(DateTime value);
        IBSDatetimePicker SetLayout(LayoutType layoutType);
        IBSDatetimePicker SetDateType(DateType dateType);
        IBSDatetimePicker SetTitle(string title);
        IBSDatetimePicker SetHtmlAttributes(object htmlAttributes);
        IBSDatetimePicker SetTooltip(string tooltip);
        IBSDatetimePicker SetHelpText(string helpText);
        IBSDatetimePicker SetDisable(bool isDisable);
        IBSDatetimePicker SetReadOnly(bool isReadOnly);
        IBSDatetimePicker SetParentCss(string css);
        IBSDatetimePicker SetMinDate(string minDate);
        IBSDatetimePicker SetMaxDate(string maxDate);
        IBSDatetimePicker SetBindProp(string bindProp);
        IBSDatetimePicker SetRequired(string source, string condition);
        IBSDatetimePicker SetRequiredExpression(string source, string expression);
        IBSDatetimePicker SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSDatetimePicker SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSDatetimePicker SetTitleHidden(bool isHidden);
        IBSDatetimePicker SetCallBackFunction(string functionName);
        IBSDatetimePicker SetLabelWidth(int labelWidth);
    }
}
