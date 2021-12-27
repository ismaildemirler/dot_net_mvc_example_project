using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextArea
{
    public interface IBSTextArea
    {
        IBSTextArea Cols(int cols);
        IBSTextArea Rows(int rows);
        IBSTextArea SetLayout(LayoutType layoutType);
        IBSTextArea SetValue(string value);
        IBSTextArea SetTitle(string title);
        IBSTextArea SetHtmlAttributes(object htmlAttributes);
        IBSTextArea SetPlaceholder(string placeholder);
        IBSTextArea SetTooltip(string tooltip);
        IBSTextArea SetHelpText(string helpText);
        IBSTextArea SetDisable(bool isDisable);
        IBSTextArea SetReadOnly(bool isReadOnly);
        IBSTextArea SetParentCss(string css="");
        IBSTextArea SetBindProp(string bindProp);
        IBSTextArea SetRequired(string source, string condition);
        IBSTextArea SetRequiredExpression(string source, string expression);
        IBSTextArea SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSTextArea SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSTextArea SetTitleHidden(bool isHidden);
    }
}
