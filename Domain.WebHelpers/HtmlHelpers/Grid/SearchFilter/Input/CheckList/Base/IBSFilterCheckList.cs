using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public interface IBSFilterCheckList
    {
        IBSFilterCheckList SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterCheckList SetTitle(string customTitle);
        IBSFilterCheckList SetInputType(FilterGroupType inputType);
        IBSFilterCheckList SetHtmlAttributes(object htmlAttributes);
        IBSFilterCheckList SetTooltip(string tooltip);
    }
}
