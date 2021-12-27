using System.Collections.Generic;
using Domain.WebHelpers.HtmlHelpers.Tab.Enum;
using Domain.WebHelpers.HtmlHelpers.Tab.TabItem;

namespace Domain.WebHelpers.HtmlHelpers.Tab
{
    public interface IBSTab
    {
        IBSTab SetHtmlAttributes(object htmlAttributes);
        IBSTab SetItems(List<IBSTabItem> items);
        IBSTab SetStyle(EnumTabStyles style);
    }
}
