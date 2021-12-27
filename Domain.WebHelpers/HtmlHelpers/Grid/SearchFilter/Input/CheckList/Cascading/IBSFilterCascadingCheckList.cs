using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Cascading
{
    public interface IBSFilterCascadingCheckList
    {
        IBSFilterCascadingCheckList SetTargetElemetID(string targetElemetID);
        IBSFilterCascadingCheckList SetTargetUrl(string targetUrl);
        IBSFilterCascadingCheckList SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterCascadingCheckList SetTitle(string customTitle);
        IBSFilterCascadingCheckList SetInputType(FilterGroupType inputType);
        IBSFilterCascadingCheckList SetHtmlAttributes(object htmlAttributes); 
    }
}
