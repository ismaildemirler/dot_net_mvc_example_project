using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Range
{
    public interface IBSFilterRangeFor
    {
        IBSFilterRangeFor SetCustomID(string customId);
        IBSFilterRangeFor SetCustomTitle(string customTitle);
        IBSFilterRangeFor SetInputType(FilterGroupType inputType);
        IBSFilterRangeFor SetHtmlAttributes(object htmlAttributes); 
    }
}
