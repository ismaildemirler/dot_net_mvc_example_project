using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Numeric
{
    public interface IBSNumericDropDownListFor<TModel>
    {
        IBSNumericDropDownListFor<TModel> SetMinValue(int minValue = 0);
        IBSNumericDropDownListFor<TModel> SetMaxValue(int maxValue = 100);
        IBSNumericDropDownListFor<TModel> SetIncrementValue(int incrementValue = 1);
        IBSNumericDropDownListFor<TModel> SetLayout(LayoutType layoutType);
        IBSNumericDropDownListFor<TModel> SetCustomTitle(string customTitle);
        IBSNumericDropDownListFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSNumericDropDownListFor<TModel> SetTooltip(string tooltip);
        IBSNumericDropDownListFor<TModel> SetHelpText(string helpText);
        IBSNumericDropDownListFor<TModel> SetDisable(bool isDisable);
        IBSNumericDropDownListFor<TModel> SetParentCss(string css);
        IBSNumericDropDownListFor<TModel> SetOptionLabel(string optionLabel);
        IBSNumericDropDownListFor<TModel> SetBindProp(string bindProp);
        IBSNumericDropDownListFor<TModel> SetRequired(string source, string condition);
        IBSNumericDropDownListFor<TModel> SetRequiredExpression(string source, string expression);
        IBSNumericDropDownListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSNumericDropDownListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSNumericDropDownListFor<TModel> SetTitleHidden(bool isHidden);
        IBSNumericDropDownListFor<TModel> SetCallBackFunction(string functionName);
    }
}
