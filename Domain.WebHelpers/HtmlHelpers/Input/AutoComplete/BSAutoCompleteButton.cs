using System;

namespace Domain.WebHelpers.HtmlHelpers.Input.AutoComplete
{
    public class BSAutoCompleteButton
    {
        public BSAutoCompleteButton()
        {
            id = "btn_" + Guid.NewGuid().ToString().ToLower().Replace("-", "");
        }
        public string id { get; set; }
        public string url { get; set; }
        public string css { get; set; }
        public string icon { get; set; }
        public string title { get; set; }
        public object htmlAttributes { get; set; }
    }
}
