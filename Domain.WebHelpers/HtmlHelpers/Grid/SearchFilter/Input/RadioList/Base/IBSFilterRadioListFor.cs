using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public interface IBSFilterRadioListFor<TModel>
    {
        IBSFilterRadioListFor<TModel> SetCustomID(string customId);
        IBSFilterRadioListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList);
        IBSFilterRadioListFor<TModel> SetCustomTitle(string customTitle);
        IBSFilterRadioListFor<TModel> SetInputType(FilterGroupType inputType = FilterGroupType.Text);
        IBSFilterRadioListFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSFilterRadioListFor<TModel> SetTooltip(string tooltip);
    }
}
