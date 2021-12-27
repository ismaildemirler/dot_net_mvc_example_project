using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker
{
    public interface IBSDatetimePickerFor<TModel>
    {
        IBSDatetimePickerFor<TModel> SetCustomID(string id);
        IBSDatetimePickerFor<TModel> SetLayout(LayoutType layoutType);
        IBSDatetimePickerFor<TModel> SetDateType(DateType dateType);
        IBSDatetimePickerFor<TModel> SetCustomTitle(string customTitle);
        IBSDatetimePickerFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSDatetimePickerFor<TModel> SetTooltip(string tooltip);
        IBSDatetimePickerFor<TModel> SetHelpText(string helpText);
        IBSDatetimePickerFor<TModel> SetDisable(bool isDisable);
        IBSDatetimePickerFor<TModel> SetReadOnly(bool isReadOnly);
        IBSDatetimePickerFor<TModel> SetParentCss(string css);
        IBSDatetimePickerFor<TModel> SetMinDate(string minDate);
        IBSDatetimePickerFor<TModel> SetMaxDate(string maxDate);
        IBSDatetimePickerFor<TModel> SetBindProp(string bindProp);
        IBSDatetimePickerFor<TModel> SetRequired(string source, string condition);
        IBSDatetimePickerFor<TModel> SetRequiredExpression(string source, string expression);
        IBSDatetimePickerFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSDatetimePickerFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSDatetimePickerFor<TModel> SetTitleHidden(bool isHidden);
        IBSDatetimePickerFor<TModel> SetCallBackFunction(string functionName);
        IBSDatetimePickerFor<TModel> SetLabelWidth(int labelWidth);
    }
}
