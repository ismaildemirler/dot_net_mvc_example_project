using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base
{
    public interface IBSDropDownListFor<TModel>
    {
        IBSDropDownListFor<TModel> SetCustomID(string id);
        IBSDropDownListFor<TModel> SetLayout(LayoutType layoutType);
        IBSDropDownListFor<TModel> SetCustomTitle(string customTitle);
        IBSDropDownListFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSDropDownListFor<TModel> SetTooltip(string tooltip);
        IBSDropDownListFor<TModel> SetHelpText(string helpText);
        IBSDropDownListFor<TModel> SetDisable(bool isDisable);
        IBSDropDownListFor<TModel> SetParentCss(string css);
        IBSDropDownListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSDropDownListFor<TModel> SetOptionLabel(string optionLabel);
        IBSDropDownListFor<TModel> SetBindProp(string bindProp);
        IBSDropDownListFor<TModel> SetRequired(string source, string condition);
        IBSDropDownListFor<TModel> SetRequiredExpression(string source, string expression);
        IBSDropDownListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false);
        IBSDropDownListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSDropDownListFor<TModel> SetRelatedInput(string id, string ajaxUrl, string callBackFunction = "");
        IBSDropDownListFor<TModel> SetTitleHidden(bool isHidden);
        IBSDropDownListFor<TModel> SetLabelWidth(int labelWidth = 2);
        IBSDropDownListFor<TModel> SetCallBackFunction(string functionName);
    }
}
