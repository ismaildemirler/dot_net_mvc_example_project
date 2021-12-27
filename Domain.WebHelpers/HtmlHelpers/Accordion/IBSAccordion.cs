using System.Collections.Generic;
using Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem;
using Domain.WebHelpers.HtmlHelpers.Accordion.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Accordion
{
    public interface IBSAccordion
    {
        IBSAccordion SetHtmlAttributes(object htmlAttributes);
        IBSAccordion SetItems(List<IBSAccordionItem> items);
        IBSAccordion SetStyle(EnumAccordionStyles style);
    }
}
