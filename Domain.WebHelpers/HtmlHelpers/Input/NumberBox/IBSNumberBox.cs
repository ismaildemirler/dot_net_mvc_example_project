using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.NumberBox
{
    public interface IBSNumberBox
    {
        IBSNumberBox SetLayout(LayoutType layoutType);
        IBSNumberBox SetValue(string value);
        IBSNumberBox SetTitle(string title);
        IBSNumberBox SetHtmlAttributes(object htmlAttributes);
        IBSNumberBox SetPlaceholder(string placeholder);
        IBSNumberBox SetTooltip(string tooltip);
        IBSNumberBox SetHelpText(string helpText);
        IBSNumberBox SetDisable(bool isDisable);
        IBSNumberBox SetReadOnly(bool isReadOnly);
        IBSNumberBox SetParentCss(string css = "");
        IBSNumberBox SetBindProp(string bindProp);
        IBSNumberBox SetRequired(string source, string condition);
        IBSNumberBox SetRequiredExpression(string source, string expression);
        IBSNumberBox SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSNumberBox SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSNumberBox SetTitleHidden(bool isHidden);
        IBSNumberBox SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSNumberBox SetMultipleAddOn(bool isMultipleAddOn);
        IBSNumberBox SetRange(int minNumber, long? maxNumber = null, string rangeMessage = "");
        IBSNumberBox SetLength(int length);
        IBSNumberBox SetCallBackFunction(string functionName);
    }
}
