using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextBox
{
    public interface IBSTextBoxFor<TModel>
    {
        IBSTextBoxFor<TModel> SetCustomID(string id);
        IBSTextBoxFor<TModel> SetLayout(LayoutType layoutType);
        IBSTextBoxFor<TModel> SetCustomTitle(string customTitle);
        IBSTextBoxFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSTextBoxFor<TModel> SetPlaceholder(string placeholder);
        IBSTextBoxFor<TModel> SetTooltip(string tooltip);
        IBSTextBoxFor<TModel> SetHelpText(string helpText);
        IBSTextBoxFor<TModel> SetDisable(bool isDisable);
        IBSTextBoxFor<TModel> SetReadOnly(bool isReadOnly);
        IBSTextBoxFor<TModel> SetParentCss(string css="");
        IBSTextBoxFor<TModel> SetBindProp(string bindProp);
        IBSTextBoxFor<TModel> SetRequired(string source, string condition);
        IBSTextBoxFor<TModel> SetRequiredExpression(string source, string expression);
        IBSTextBoxFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSTextBoxFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSTextBoxFor<TModel> SetCustomMask(string mask = "");
        IBSTextBoxFor<TModel> SetMaskType(MaskType maskType);
        IBSTextBoxFor<TModel> SetRange(int minNumber, long maxNumber, string rangeMessage = "");
        IBSTextBoxFor<TModel> SetCharacterSize(int size, CharacterType characterType = CharacterType.All);
        IBSTextBoxFor<TModel> SetTitleHidden(bool isHidden);
        IBSTextBoxFor<TModel> SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSTextBoxFor<TModel> SetMultipleAddOn(bool isMultipleAddOn);
        IBSTextBoxFor<TModel> SetLabelWidth(int labelWidth);
        IBSTextBoxFor<TModel> SetHidden(bool isHidden);
        IBSTextBoxFor<TModel> SetValue(object value);
    }
}
