using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Text
{
    public interface IBSFilterInputFor<TModel>
    {
        IBSFilterInputFor<TModel> SetCustomID(string customId);
        IBSFilterInputFor<TModel> SetCustomTitle(string customTitle);
        IBSFilterInputFor<TModel> SetInputType(FilterGroupType inputType);
        IBSFilterInputFor<TModel> SetHtmlAttributes(object htmlAttributes); 
    }
}
