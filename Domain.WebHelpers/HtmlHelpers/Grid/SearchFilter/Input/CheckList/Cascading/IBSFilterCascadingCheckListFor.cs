using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Cascading
{
    public interface IBSFilterCascadingCheckListFor<TModel>
    {
        IBSFilterCascadingCheckListFor<TModel> SetCustomID(string customId);
        IBSFilterCascadingCheckListFor<TModel> SetTargetElemetID(string targetElemetID);
        IBSFilterCascadingCheckListFor<TModel> SetTargetUrl(string targetUrl);
        IBSFilterCascadingCheckListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterCascadingCheckListFor<TModel> SetCustomTitle(string customTitle);
        IBSFilterCascadingCheckListFor<TModel> SetInputType(FilterGroupType inputType = FilterGroupType.Text);
        IBSFilterCascadingCheckListFor<TModel> SetHtmlAttributes(object htmlAttributes); 
    }
}
