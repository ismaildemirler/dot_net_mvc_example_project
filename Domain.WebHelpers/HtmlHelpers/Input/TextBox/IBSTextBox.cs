using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextBox
{
    public interface IBSTextBox
    {
        IBSTextBox SetLayout(LayoutType layoutType);
        IBSTextBox SetValue(object value);
        IBSTextBox SetTitle(string title);
        IBSTextBox SetHtmlAttributes(object htmlAttributes);
        IBSTextBox SetPlaceholder(string placeholder);
        IBSTextBox SetTooltip(string tooltip);
        IBSTextBox SetHelpText(string helpText);
        IBSTextBox SetDisable(bool isDisable);
        IBSTextBox SetReadOnly(bool isReadOnly);
        IBSTextBox SetParentCss(string css="");
        IBSTextBox SetBindProp(string bindProp);
        IBSTextBox SetRequired(string source, string condition);
        IBSTextBox SetRequiredExpression(string source, string expression);
        IBSTextBox SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSTextBox SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSTextBox SetCustomMask(string mask = "");
        IBSTextBox SetMaskType(MaskType maskType);
        IBSTextBox SetCharacterSize(int size, CharacterType characterType = CharacterType.All);
        IBSTextBox SetTitleHidden(bool isHidden);
        IBSTextBox SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSTextBox SetMultipleAddOn(bool isMultipleAddOn);
        IBSTextBox SetHidden(bool hidden);
        IBSTextBox SetLabelWidth(int labelWidth);
    }
}
