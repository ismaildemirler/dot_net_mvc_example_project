using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public class BSRadioButtonListFor<TModel, TProperty> : IBSRadioButtonListFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;

        private string _parentDivCSS;
        private string _functionName;
        private string _customTitle;
        private List<IBSRadioButton> _radioButtons = new List<IBSRadioButton>();

        private bool _isSolid;
        private bool _isTitleHidden;

        private StateType _state;
        private List<SelectListItem> _selectList;
        private LayoutType _layoutType;

        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        #endregion

        #region ctor
        public BSRadioButtonListFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this._htmlHelper = htmlHelper;
            this._expression = expression;
        }
        #endregion

        #region fluent methods
        public IBSRadioButtonListFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }

        public IBSRadioButtonListFor<TModel> SetRadioButtons(Action<BSRadioButtonBuilder> radioBuilder)
        {
            var builder = new BSRadioButtonBuilder();
            radioBuilder(builder);
            foreach (var radio in builder)
            {
                _radioButtons.Add(radio);
            }
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetSelectList(List<SelectListItem> selectList)
        {
            this._selectList = selectList;
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetState(StateType state = HtmlHelpers.Input.Checkbox.Enum.StateType.@default)
        {
            this._state = state;
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSRadioButtonListFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }

        public IBSRadioButtonListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            System.Type type = typeof(TModel);

            var propInfo = Util.GetPropertyInfo(_expression);
            string name = propInfo.Name;
            var display = (DisplayAttribute)propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            TagBuilder divParent = new TagBuilder("div");
            divParent.AddCssClass("m-form p-2");

            bool isColAdded = false;
            if (!_parentDivCSS.IsBlank())
            {
                divParent.AddCssClass(_parentDivCSS);
                if (_parentDivCSS.Contains("col "))
                    isColAdded = true;
            }
            if (!isColAdded)
                divParent.AddCssClass("col");

            TagBuilder divContainer = new TagBuilder("div");
            divContainer.AddCssClass("m-form__group form-group radio-group");

            if (!_functionName.IsBlank())
                divContainer.MergeAttribute("data-callback", _functionName);

            if (!_isTitleHidden)
            {
                TagBuilder labelTitle = new TagBuilder("label");
                if (!String.IsNullOrEmpty(_customTitle))
                {
                    labelTitle.SetInnerText(_customTitle);
                    divContainer.InnerHtml += labelTitle.ToString();
                }
                else if ((display != null && !display.Name.IsBlank()))
                {
                    labelTitle.SetInnerText(display.Name);
                    divContainer.InnerHtml += labelTitle.ToString();
                }
            }

            if (!_displaySourceExp.IsBlank())
            {
                divParent.MergeAttribute("data-display-source-exp", _displaySourceExp);
                divParent.MergeAttribute("data-display-expression", _displayExp);
                if (_isAlsoRequired)
                {
                    divParent.MergeAttribute("data_custom_val_required", "true");
                    divParent.MergeAttribute("data_custom_val_required_source_exp", _displaySourceExp);
                    divParent.MergeAttribute("data_custom_val_required_expression", _displayExp);
                }
            }

            TagBuilder divRadioContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
                divRadioContainer.AddCssClass("m-radio-list");
            else
                divRadioContainer.AddCssClass("m-radio-inline");

            if (_radioButtons.Count > 0 && _selectList != null && _selectList.Count() > 0)
                throw new Exception("Radiobutton builder ve selectlist aynı anda set edilmiştir. Yalnızca birini kullanabilirsiniz!");

            if(_selectList != null)
            {
                _radioButtons = new List<IBSRadioButton>();
                foreach (var item in _selectList)
                    _radioButtons.Add(new BSRadioButton().SetDisable(item.Disabled).SetText(item.Text).SetValue(item.Value));
            }

            for (int i = 0; i < _radioButtons.Count; i++)
            { 
                var radio = (BSRadioButton)_radioButtons[i];

                TagBuilder labelRadio = new TagBuilder("label");
                labelRadio.AddCssClass("m-radio");

                if (radio.IsDisabled)
                    labelRadio.AddCssClass("m-radio--disabled");

                if (_isSolid)
                    labelRadio.AddCssClass("m-radio--solid");

                labelRadio.AddCssClass($"m-radio--state-{_state.ToString()}");

                var attributes = radio.HtmlAttributes;
                if (radio.IsDisabled)
                    attributes = Util.MergeHtmlAttributes(radio.HtmlAttributes, new { disabled = "disabled" });
                
                attributes = Util.MergeHtmlAttributes(attributes, new { id = $"{name}_{i}"}); 

                labelRadio.InnerHtml = _htmlHelper.RadioButtonFor(_expression, radio.Value, (IDictionary<string, object>)attributes).ToString();
                labelRadio.InnerHtml += radio.Text;
                labelRadio.InnerHtml += new TagBuilder("span").ToString();
                
                divRadioContainer.InnerHtml += labelRadio;
            }
            divContainer.InnerHtml += divRadioContainer.ToString();

            TagBuilder divValidation = new TagBuilder("div");
            divValidation.InnerHtml = _htmlHelper.ValidationMessageFor(_expression).ToString();
            divContainer.InnerHtml += divValidation.ToString();

            if (display != null && !display.Description.IsBlank())
            {
                TagBuilder spanHelpText = new TagBuilder("span");
                spanHelpText.AddCssClass("m-form__help");
                spanHelpText.InnerHtml = display.Description;
                divContainer.InnerHtml += spanHelpText.ToString();
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();
        }
    }
}
