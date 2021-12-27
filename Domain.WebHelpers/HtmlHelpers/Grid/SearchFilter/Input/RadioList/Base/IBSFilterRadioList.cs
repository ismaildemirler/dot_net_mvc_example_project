using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public interface IBSFilterRadioList
    {
        IBSFilterRadioList SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterRadioList SetTitle(string customTitle);
        IBSFilterRadioList SetInputType(FilterGroupType inputType);
        IBSFilterRadioList SetHtmlAttributes(object htmlAttributes);
        IBSFilterRadioList SetTooltip(string tooltip);
    }
}
