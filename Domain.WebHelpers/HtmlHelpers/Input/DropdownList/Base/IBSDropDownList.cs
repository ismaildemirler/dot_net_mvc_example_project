using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base
{
    public interface IBSDropDownList
    {
        IBSDropDownList SetLayout(LayoutType layoutType);
        IBSDropDownList SetSelectedValue(object value);
        IBSDropDownList SetTitle(string title);
        IBSDropDownList SetHtmlAttributes(object htmlAttributes);
        IBSDropDownList SetTooltip(string tooltip);
        IBSDropDownList SetHelpText(string helpText);
        IBSDropDownList SetDisable(bool isDisable);
        IBSDropDownList SetParentCss(string css);
        IBSDropDownList SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSDropDownList SetOptionLabel(string optionLabel);
        IBSDropDownList SetBindProp(string bindProp);
        IBSDropDownList SetRequired(string source, string condition);
        IBSDropDownList SetRequiredExpression(string source, string expression);
        IBSDropDownList SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSDropDownList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSDropDownList SetRelatedInput(string id, string ajaxUrl, string callBackFunction = "");
        IBSDropDownList SetTitleHidden(bool isHidden);
        IBSDropDownList SetCallBackFunction(string functionName);
        IBSDropDownList SetLabelWidth(int labelWidth);

    }
}
