using System;
using System.Text;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public class BSCheckBoxContainer : IBSCheckBoxContainer, IDisposable
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private string _id;
        private string _helpText;
        private object _htmlAttributes;
        private bool _isLayoutSet;
        #endregion

        #region ctor
        public BSCheckBoxContainer(HtmlHelper htmlHelper, string title)
        {
            this._htmlHelper = htmlHelper;
            _htmlHelper.ViewContext.Writer.Write("<div class='m-form p-2 col'>");

            _htmlHelper.ViewContext.Writer.Write("<div class='m-form__group form-group'>");
            _htmlHelper.ViewContext.Writer.Write("<label>{0}</label>", title);
        }
        #endregion

        #region fluent methods
        public IBSCheckBoxContainer SetID(string id)
        {
            if (_isLayoutSet)
                throw new Exception("SetID metodu, SetLayout metodundan önce çalıştırılmalıdır!");

            this._id = id;
            return this;
        }
        public IBSCheckBoxContainer SetLayout(LayoutType layoutType)
        {
            _isLayoutSet = true;

            if (layoutType == LayoutType.vertical)
                _htmlHelper.ViewContext.Writer.Write("<div {0}class='m-checkbox-list'>", _id.IsBlank() ? "" : $"id='{_id}' ");
            else
                _htmlHelper.ViewContext.Writer.Write("<div {0}class='m-checkbox-inline row'>", _id.IsBlank() ? "" : $"id='{_id}' ");

            return this;
        }
        public IBSCheckBoxContainer SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSCheckBoxContainer SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        #endregion

        public void Dispose()
        {
            if (!_isLayoutSet)
                throw new Exception("SetLayout metodu çalıştrılmadığı için hata oluştu!");

            StringBuilder sb = new StringBuilder();
            sb.Append("</div>");

            if(!_helpText.IsBlank())
            {
                sb.Append("<span class='m-form__help'>");
                sb.Append(_helpText);
                sb.Append("</span>");
            }

            sb.Append("</div></div>");
            _htmlHelper.ViewContext.Writer.Write(sb.ToString());
        }
    }
}
