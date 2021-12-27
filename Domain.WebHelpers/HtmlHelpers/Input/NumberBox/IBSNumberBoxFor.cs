using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextBox
{
    public interface IBSNumberBoxFor<TModel>
    {
        IBSNumberBoxFor<TModel> SetCustomID(string id);
        IBSNumberBoxFor<TModel> SetLayout(LayoutType layoutType);
        IBSNumberBoxFor<TModel> SetCustomTitle(string customTitle);
        IBSNumberBoxFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSNumberBoxFor<TModel> SetPlaceholder(string placeholder);
        IBSNumberBoxFor<TModel> SetTooltip(string tooltip);
        IBSNumberBoxFor<TModel> SetHelpText(string helpText);
        IBSNumberBoxFor<TModel> SetDisable(bool isDisable);
        IBSNumberBoxFor<TModel> SetReadOnly(bool isReadOnly);
        IBSNumberBoxFor<TModel> SetParentCss(string css = "");
        IBSNumberBoxFor<TModel> SetBindProp(string bindProp);
        IBSNumberBoxFor<TModel> SetRequired(string source, string condition);
        IBSNumberBoxFor<TModel> SetRequiredExpression(string source, string expression);
        IBSNumberBoxFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSNumberBoxFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSNumberBoxFor<TModel> SetTitleHidden(bool isHidden);
        IBSNumberBoxFor<TModel> SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSNumberBoxFor<TModel> SetMultipleAddOn(bool isMultipleAddOn);
        IBSNumberBoxFor<TModel> SetLabelWidth(int labelWidth);
        IBSNumberBoxFor<TModel> SetRange(int minNumber,long? maxNumber = null, string rangeMessage = "");
        IBSNumberBoxFor<TModel> SetLength(int length);
        IBSNumberBoxFor<TModel> SetCallBackFunction(string functionName);
    }
}
