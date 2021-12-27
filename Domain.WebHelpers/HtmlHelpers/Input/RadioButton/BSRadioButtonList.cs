using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public class BSRadioButtonList : IBSRadioButtonList, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;

        private List<IBSRadioButton> _radioButtons = new List<IBSRadioButton>();
        private string _name;
        private string _parentDivCSS;
        private string _title;
        private object _selectedValue;
        private string _helpText;
        private string _functionName;

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
        public BSRadioButtonList(HtmlHelper htmlHelper, string name)
        {
            this._htmlHelper = htmlHelper;
            this._name = name;
        }
        #endregion

        #region fluent methods
        public IBSRadioButtonList SetID(string id)
        {
            throw new NotImplementedException();
        }
        public IBSRadioButtonList SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IBSRadioButtonList SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSRadioButtonList SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        } 
        public IBSRadioButtonList SetSelectedValue(object selectedValue)
        {
            _selectedValue = selectedValue;
            return this;
        }
        public IBSRadioButtonList SetRadioButtons(Action<BSRadioButtonBuilder> radioBuilder)
        {
            var builder = new BSRadioButtonBuilder();
            radioBuilder(builder);
            foreach (var radio in builder)
            {
                _radioButtons.Add(radio);
            }
            return this;
        }
        public IBSRadioButtonList SetSelectList(List<SelectListItem> selectList)
        {
            this._selectList = selectList;
            return this;
        }
        public IBSRadioButtonList SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
            return this;
        }
        public IBSRadioButtonList SetState(StateType state = HtmlHelpers.Input.Checkbox.Enum.StateType.@default)
        {
            this._state = state;
            return this;
        }
        public IBSRadioButtonList SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSRadioButtonList SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSRadioButtonList SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }
        public IBSRadioButtonList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            StringBuilder sb = new StringBuilder();
            
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

            if (!_isTitleHidden)
            {
            TagBuilder labelTitle = new TagBuilder("label");
            labelTitle.SetInnerText(_title);
            divContainer.InnerHtml += labelTitle.ToString();
            }

            TagBuilder divRadioContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
                divRadioContainer.AddCssClass("m-radio-list");
            else
                divRadioContainer.AddCssClass("m-radio-inline");

            if (_radioButtons.Count > 0 && _selectList != null && _selectList.Any())
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

                labelRadio.AddCssClass($"m-radio--state-{_state}");

                var attributes = radio.HtmlAttributes;
                if (radio.IsDisabled)
                    attributes = Util.MergeHtmlAttributes(radio.HtmlAttributes, new { disabled = "disabled" });
                
                attributes = Util.MergeHtmlAttributes(attributes, new { id = $"{_name}_{i}"}); 

                labelRadio.InnerHtml = _htmlHelper.RadioButton(_name, radio.Value, _selectedValue?.ToString() == radio.Value.ToString(), (IDictionary<string, object>)attributes).ToString();
                labelRadio.InnerHtml += radio.Text;
                labelRadio.InnerHtml += new TagBuilder("span").ToString();

                divRadioContainer.InnerHtml += labelRadio;
            }
            divContainer.InnerHtml += divRadioContainer.ToString();

            TagBuilder divValidation = new TagBuilder("div");
            divValidation.InnerHtml = _htmlHelper.ValidationMessage(_name).ToString();
            divContainer.InnerHtml += divValidation.ToString();

            if (!_helpText.IsBlank())
            {
                TagBuilder spanHelpText = new TagBuilder("span");
                spanHelpText.AddCssClass("m-form__help");
                spanHelpText.InnerHtml = _helpText;
                divContainer.InnerHtml += spanHelpText.ToString();
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();
        }

    }
}
