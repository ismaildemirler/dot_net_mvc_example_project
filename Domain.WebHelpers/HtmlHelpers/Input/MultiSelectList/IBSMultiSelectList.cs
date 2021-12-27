using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.MultiSelectList
{
    public interface IBSMultiSelectList
    {
        IBSMultiSelectList SetLayout(LayoutType layoutType);
        IBSMultiSelectList SetTitle(string title);
        IBSMultiSelectList SetHtmlAttributes(object htmlAttributes);
        IBSMultiSelectList SetTooltip(string tooltip);
        IBSMultiSelectList SetHelpText(string helpText);
        IBSMultiSelectList SetDisable(bool isDisable);
        IBSMultiSelectList SetParentCss(string css);
        IBSMultiSelectList SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSMultiSelectList SetRequired(string source, string condition);
        IBSMultiSelectList SetRequiredExpression(string source, string expression);
        IBSMultiSelectList SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSMultiSelectList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSMultiSelectList SetTitleHidden(bool isHidden);
        IBSMultiSelectList SetCallBackFunction(string functionName);
    }
}
