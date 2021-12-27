using System.Text;
using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.Tab.TabItem
{
    public class BSTabItem : IBSTabItem
    {
        private string _ajaxUrl;
        private string _title;
        private string _icon;
        private string _id;
        private string _method;
        private bool _state;

        public BSTabItem()
        {
            _state = false;
            _method = FormMethod.Get.ToString();
        }
        public IBSTabItem SetId(string id)
        {
            _id = id;
            return this;
        }

        public IBSTabItem SetAjaxUrl(string ajaxUrl)
        {
            _ajaxUrl = ajaxUrl;
            return this;
        }

        public IBSTabItem SetIcon(string icon)
        {
            _icon = icon;
            return this;
        }

        public IBSTabItem SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public IBSTabItem SetMethod(FormMethod method)
        {
            _method = method.ToString().ToUpper();
            return this;
        }

        public IBSTabItem SetActive(bool state)
        {
            _state = state;
            return this;
        }

        public string ToHtmlStringTab()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<li class=\"nav-item m-tabs__item\"><a class=\"ajaxTabBtn nav-link m-tabs__link {0}\" data-url=\"{1}\" data-method=\"{2}\" data-toggle=\"tab\" href=\"#m_tabs_{3}\" role=\"tab\">{4}&nbsp; {5}</a></li>", _state ? "active" : "", _ajaxUrl, _method, _id, $"<i class=\"{_icon}\"></i>", _title);
            return sb.ToString();
        }

        public string ToHtmlStringContent()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("<div class=\"tab-pane {0}\" id=\"m_tabs_{1}\" role=\"tabpanel\"></div>", _state ? "active" : "", _id);
            return sb.ToString();
        }
    }
}
