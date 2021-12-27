using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Enum
{
    public interface IBSEnumDropDownList<TEnum> 
    {
        IBSEnumDropDownList<TEnum> SetLayout(LayoutType layoutType);
        IBSEnumDropDownList<TEnum> SetSelectedValue(object selectedValue);
        IBSEnumDropDownList<TEnum> SetTitle(string customTitle);
        IBSEnumDropDownList<TEnum> SetHtmlAttributes(object htmlAttributes);
        IBSEnumDropDownList<TEnum> SetTooltip(string tooltip);
        IBSEnumDropDownList<TEnum> SetHelpText(string helpText);
        IBSEnumDropDownList<TEnum> SetDisable(bool isDisable);
        IBSEnumDropDownList<TEnum> SetParentCss(string css);
        IBSEnumDropDownList<TEnum> SetOptionLabel(string optionLabel);
        IBSEnumDropDownList<TEnum> SetBindProp(string bindProp);
        IBSEnumDropDownList<TEnum> SetRequired(string source, string condition);
        IBSEnumDropDownList<TEnum> SetRequiredExpression(string source, string expression);
        IBSEnumDropDownList<TEnum> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSEnumDropDownList<TEnum> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSEnumDropDownList<TEnum> SetTitleHidden(bool isHidden);
        IBSEnumDropDownList<TEnum> SetCallBackFunction(string functionName);
    }
}
