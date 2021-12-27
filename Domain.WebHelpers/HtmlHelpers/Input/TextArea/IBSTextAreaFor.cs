using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextArea
{
    public interface IBSTextAreaFor<TModel>
    {
        IBSTextAreaFor<TModel> SetCustomID(string id);
        IBSTextAreaFor<TModel> Cols(int cols);
        IBSTextAreaFor<TModel> Rows(int rows);
        IBSTextAreaFor<TModel> SetLayout(LayoutType layoutType);
        IBSTextAreaFor<TModel> SetCustomTitle(string customTitle);
        IBSTextAreaFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSTextAreaFor<TModel> SetPlaceholder(string placeholder);
        IBSTextAreaFor<TModel> SetTooltip(string tooltip);
        IBSTextAreaFor<TModel> SetHelpText(string helpText);
        IBSTextAreaFor<TModel> SetDisable(bool isDisable);
        IBSTextAreaFor<TModel> SetReadOnly(bool isReadOnly);
        IBSTextAreaFor<TModel> SetParentCss(string css="");
        IBSTextAreaFor<TModel> SetBindProp(string bindProp);
        IBSTextAreaFor<TModel> SetRequired(string source, string condition);
        IBSTextAreaFor<TModel> SetRequiredExpression(string source, string expression);
        IBSTextAreaFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSTextAreaFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSTextAreaFor<TModel> SetTitleHidden(bool isHidden);
        IBSTextAreaFor<TModel> SetLabelWidth(int labelWidth);
    }
}
