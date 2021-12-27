using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.Tab.TabItem
{
    public interface IBSTabItem
    {
        IBSTabItem SetId(string id);
        IBSTabItem SetAjaxUrl(string ajaxUrl);
        IBSTabItem SetIcon(string icon);
        IBSTabItem SetTitle(string title);
        IBSTabItem SetMethod(FormMethod method);
        IBSTabItem SetActive(bool state);
        string ToHtmlStringTab();
        string ToHtmlStringContent();
    }
}
