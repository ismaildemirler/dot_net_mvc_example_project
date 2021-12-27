using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Enum
{
    public interface IBSEnumDropDownListFor<TModel, TEnum>
    {
        IBSEnumDropDownListFor<TModel, TEnum> SetCustomID(string id);
        IBSEnumDropDownListFor<TModel, TEnum> SetLayout(LayoutType layoutType);
        IBSEnumDropDownListFor<TModel, TEnum> SetCustomTitle(string customTitle);
        IBSEnumDropDownListFor<TModel, TEnum> SetHtmlAttributes(object htmlAttributes);
        IBSEnumDropDownListFor<TModel, TEnum> SetTooltip(string tooltip);
        IBSEnumDropDownListFor<TModel, TEnum> SetHelpText(string helpText);
        IBSEnumDropDownListFor<TModel, TEnum> SetDisable(bool isDisable);
        IBSEnumDropDownListFor<TModel, TEnum> SetParentCss(string css);
        IBSEnumDropDownListFor<TModel, TEnum> SetOptionLabel(string optionLabel);
        IBSEnumDropDownListFor<TModel, TEnum> SetBindProp(string bindProp);
        IBSEnumDropDownListFor<TModel, TEnum> SetRequired(string source, string condition);
        IBSEnumDropDownListFor<TModel, TEnum> SetRequiredExpression(string source, string expression);
        IBSEnumDropDownListFor<TModel, TEnum> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSEnumDropDownListFor<TModel, TEnum> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSEnumDropDownListFor<TModel, TEnum> SetTitleHidden(bool isHidden);
        IBSEnumDropDownListFor<TModel, TEnum> SetCallBackFunction(string functionName);
    }
}
