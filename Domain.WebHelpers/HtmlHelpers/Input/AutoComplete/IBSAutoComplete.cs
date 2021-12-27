using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.Infrastructre;
using System.Collections.Generic;

namespace Domain.WebHelpers.HtmlHelpers.Input.AutoComplete
{
    public interface IBSAutoComplete
    {
        IBSAutoComplete SetLayout(LayoutType layoutType);
        IBSAutoComplete SetValue(string value);
        IBSAutoComplete SetTitle(string title);
        IBSAutoComplete SetPlaceholder(string placeholder);
        IBSAutoComplete SetHtmlAttributes(object htmlAttributes);
        IBSAutoComplete SetTooltip(string tooltip);
        IBSAutoComplete SetHelpText(string helpText);
        IBSAutoComplete SetDisable(bool isDisable);
        IBSAutoComplete SetParentCss(string css);
        IBSAutoComplete SetType(Enums.AutoCompleteType type);
        IBSAutoComplete SetMultiple(bool isMultiple);
        IBSAutoComplete SetLimit(int limit = 10);
        IBSAutoComplete SetAdditionalParameters(object parameters);
        IBSAutoComplete SetAutoCompleteActionUrl(string url, int nesneTipId);
        IBSAutoComplete SetTitleHidden(bool isHidden);
        IBSAutoComplete SetCallBackFunction(string functionName);
        IBSAutoComplete SetClearCallBackFunction(string functionName);
        IBSAutoComplete SetLockAfterSelect(bool locked);
        IBSAutoComplete SetButtons(List<BSAutoCompleteButton> buttons);
        IBSAutoComplete SetLabelWidth(int labelWidth);
    }
}
