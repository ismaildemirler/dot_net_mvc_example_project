using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public interface IBSCheckBoxFor<TModel>
    {
        IBSCheckBoxFor<TModel> SetCustomID(string id);
        IBSCheckBoxFor<TModel> SetDisable(bool isDisable);
        IBSCheckBoxFor<TModel> SetVisibility(bool isVisible);
        IBSCheckBoxFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSCheckBoxFor<TModel> SetBindProp(string bindProp);
        IBSCheckBoxFor<TModel> SetRequired(string source, string condition);
        IBSCheckBoxFor<TModel> SetRequiredExpression(string source, string expression);
        IBSCheckBoxFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSCheckBoxFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSCheckBoxFor<TModel> SetParentCss(string css = "");
        IBSCheckBoxFor<TModel> SetSolid(bool isSolid);
        IBSCheckBoxFor<TModel> SetState(StateType state = StateType.@default);
        IBSCheckBoxFor<TModel> SetCustomClasses(string customClasses);
        IBSCheckBoxFor<TModel> SetCallBackFunction(string functionName);
        IBSCheckBoxFor<TModel> SetCustomTitle(string customTitle);
        
        string ToHtmlString();
    }
}
