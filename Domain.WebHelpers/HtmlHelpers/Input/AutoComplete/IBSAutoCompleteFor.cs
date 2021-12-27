using System.Collections.Generic;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.Infrastructre;

namespace Domain.WebHelpers.HtmlHelpers.Input.AutoComplete
{
    public interface IBSAutoCompleteFor<TModel>
    {
        IBSAutoCompleteFor<TModel> SetCustomID(string id);
        IBSAutoCompleteFor<TModel> SetCustomTitle(string customTitle);
        IBSAutoCompleteFor<TModel> SetLayout(LayoutType layoutType);
        IBSAutoCompleteFor<TModel> SetPlaceholder(string placeholder);
        IBSAutoCompleteFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSAutoCompleteFor<TModel> SetTooltip(string tooltip);
        IBSAutoCompleteFor<TModel> SetHelpText(string helpText);
        IBSAutoCompleteFor<TModel> SetDisable(bool isDisable);
        IBSAutoCompleteFor<TModel> SetParentCss(string css);
        IBSAutoCompleteFor<TModel> SetType(Enums.AutoCompleteType type);
        IBSAutoCompleteFor<TModel> SetMultiple(bool isMultiple);
        IBSAutoCompleteFor<TModel> SetLimit(int limit = 10);
        IBSAutoCompleteFor<TModel> SetAdditionalParameters(object parameters);
        IBSAutoCompleteFor<TModel> SetAutoCompleteActionUrl(string url, int nesneTipId);
        IBSAutoCompleteFor<TModel> SetTitleHidden(bool isHidden);
        IBSAutoCompleteFor<TModel> SetCallBackFunction(string functionName);
        IBSAutoCompleteFor<TModel> SetLockAfterSelect(bool locked);
        IBSAutoCompleteFor<TModel> SetButtons(List<BSAutoCompleteButton> buttons);
        IBSAutoCompleteFor<TModel> SetLabelWidth(int labelWidth);
    }
}
