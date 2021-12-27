using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public class BSRadioButton : IBSRadioButton
    {
        #region private properties
        private HtmlHelper _helper;
        private bool _isDisabled;
        private string _text;
        private object _htmlAttributes;
        private bool _isSolid;
        private object _value;
        #endregion

        #region public properties
        public bool IsDisabled { get { return _isDisabled; } }
        public string Text { get { return _text; } }
        public object Value { get { return _value; } }
        public object HtmlAttributes { get { return _htmlAttributes; } }
        #endregion

        #region ctor
        public BSRadioButton()
        {
        }
        public BSRadioButton(HtmlHelper helper)
        {
            _helper = helper;
        }
        #endregion

        #region fluent methods
        public IBSRadioButton SetValue(object value)
        {
            this._value = value;
            return this;
        }
        public IBSRadioButton SetText(string text)
        {
            _text = text;
            return this;
        }
        public IBSRadioButton SetDisable(bool isDisabled)
        {
            _isDisabled = isDisabled;
            return this;
        }
        public IBSRadioButton SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
            return this;
        }
        public IBSRadioButton SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }

        #endregion
    }
}
