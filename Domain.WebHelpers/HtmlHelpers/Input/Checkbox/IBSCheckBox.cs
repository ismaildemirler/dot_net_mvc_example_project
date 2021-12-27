using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public interface IBSCheckBox
    {
        IBSCheckBox SetName(string name);
        IBSCheckBox SetChecked(bool isChecked);
        IBSCheckBox SetDisable(bool isDisable);
        IBSCheckBox SetToolTip(string tooltip);
        IBSCheckBox SetVisibility(bool isVisible);
        IBSCheckBox SetTitle(string title);
        IBSCheckBox SetTooltip(string tooltip);
        IBSCheckBox SetHtmlAttributes(object htmlAttributes);
        IBSCheckBox SetBindProp(string bindProp);
        IBSCheckBox SetRequired(string source, string condition);
        IBSCheckBox SetRequiredExpression(string source, string expression);
        IBSCheckBox SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSCheckBox SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSCheckBox SetParentCss(string css = "");
        IBSCheckBox SetSolid(bool isSolid);
        IBSCheckBox SetState(StateType state = StateType.@default);
        IBSCheckBox SetCallBackFunction(string functionName);
    }
}
