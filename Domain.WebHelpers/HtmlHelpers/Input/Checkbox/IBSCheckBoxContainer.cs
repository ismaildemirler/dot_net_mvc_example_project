using System;
using Domain.WebHelpers.HtmlHelpers.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public interface IBSCheckBoxContainer:IDisposable
    {
        IBSCheckBoxContainer SetID(string id);
        IBSCheckBoxContainer SetHtmlAttributes(object htmlAttributes);
        IBSCheckBoxContainer SetHelpText(string helpText);
        IBSCheckBoxContainer SetLayout(LayoutType layoutType);
    }
}
