using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public interface IBSFilterCheckListFor<TModel>
    {
        IBSFilterCheckListFor<TModel> SetCustomID(string customId);
        IBSFilterCheckListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterCheckListFor<TModel> SetCustomTitle(string customTitle);
        IBSFilterCheckListFor<TModel> SetInputType(FilterGroupType inputType = FilterGroupType.Text);
        IBSFilterCheckListFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSFilterCheckListFor<TModel> SetTooltip(string tooltip);
    }
}
