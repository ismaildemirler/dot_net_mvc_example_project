using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.MultiSelectList
{
    public interface IBSMultiSelectListFor<TModel>
    {
        IBSMultiSelectListFor<TModel> SetCustomID(string id);
        IBSMultiSelectListFor<TModel> SetLayout(LayoutType layoutType);
        IBSMultiSelectListFor<TModel> SetCustomTitle(string customTitle);
        IBSMultiSelectListFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSMultiSelectListFor<TModel> SetTooltip(string tooltip);
        IBSMultiSelectListFor<TModel> SetHelpText(string helpText);
        IBSMultiSelectListFor<TModel> SetDisable(bool isDisable);
        IBSMultiSelectListFor<TModel> SetParentCss(string css);
        IBSMultiSelectListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSMultiSelectListFor<TModel> SetRequired(string source, string condition);
        IBSMultiSelectListFor<TModel> SetRequiredExpression(string source, string expression);
        IBSMultiSelectListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSMultiSelectListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSMultiSelectListFor<TModel> SetTitleHidden(bool isHidden);
        IBSMultiSelectListFor<TModel> SetCallBackFunction(string functionName);
        IBSMultiSelectListFor<TModel> SetLabelWidth(int length);
    }
}
