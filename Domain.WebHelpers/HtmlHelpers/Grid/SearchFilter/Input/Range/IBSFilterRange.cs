using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Range
{
    public interface IBSFilterRange
    {
        IBSFilterRange SetTitle(string customTitle);
        IBSFilterRange SetInputType(FilterGroupType inputType = FilterGroupType.Text);
        IBSFilterRange SetHtmlAttributes(object htmlAttributes); 
    }
}
