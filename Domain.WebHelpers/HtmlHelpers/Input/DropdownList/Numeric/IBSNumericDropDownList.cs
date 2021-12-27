using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Numeric
{
    public interface IBSNumericDropDownList
    {
        IBSNumericDropDownList SetMinValue(int minValue = 0);
        IBSNumericDropDownList SetMaxValue(int maxValue = 100);
        IBSNumericDropDownList SetIncrementValue(int incrementValue = 1);
        IBSNumericDropDownList SetLayout(LayoutType layoutType);
        IBSNumericDropDownList SetSelectedValue(int value);
        IBSNumericDropDownList SetTitle(string customTitle);
        IBSNumericDropDownList SetHtmlAttributes(object htmlAttributes);
        IBSNumericDropDownList SetTooltip(string tooltip);
        IBSNumericDropDownList SetHelpText(string helpText);
        IBSNumericDropDownList SetDisable(bool isDisable);
        IBSNumericDropDownList SetParentCss(string css);
        IBSNumericDropDownList SetOptionLabel(string optionLabel);
        IBSNumericDropDownList SetBindProp(string bindProp);
        IBSNumericDropDownList SetRequired(string source, string condition);
        IBSNumericDropDownList SetRequiredExpression(string source, string expression);
        IBSNumericDropDownList SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSNumericDropDownList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSNumericDropDownList SetTitleHidden(bool isHidden);
        IBSNumericDropDownList SetCallBackFunction(string functionName);
    }
}
