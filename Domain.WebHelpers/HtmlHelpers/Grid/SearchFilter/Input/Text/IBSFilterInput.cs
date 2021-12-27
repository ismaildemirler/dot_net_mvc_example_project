using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Text
{
    public interface IBSFilterInput
    {
        IBSFilterInput SetTitle(string customTitle);
        IBSFilterInput SetInputType(FilterGroupType inputType);
        IBSFilterInput SetHtmlAttributes(object htmlAttributes); 
    }
}
