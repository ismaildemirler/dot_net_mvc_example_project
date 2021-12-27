using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Accordion.AccordionItem
{
    public interface IBSAccordionItem
    {
        IBSAccordionItem SetAjaxUrl(string ajaxUrl);
        IBSAccordionItem SetStyle(EnumAccordiomItemStyles style);
        IBSAccordionItem SetIcon(string icon);
        IBSAccordionItem SetTitle(string title);
        IBSAccordionItem SetMethod(FormMethod method);
        IBSAccordionItem SetCollapse(bool collapsed);
        void SetParentId(string parentId, string id);
        string ToHtmlString();
    }
}
