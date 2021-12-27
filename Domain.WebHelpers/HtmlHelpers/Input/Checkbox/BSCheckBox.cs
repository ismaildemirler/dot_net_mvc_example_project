using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public class BSCheckBox : IBSCheckBox, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;

        private bool _isChecked;
        private bool _isDisable;
        private bool _isVisible;
        private bool _isSolid;

        private string _id;
        private string _name;
        private string _tooltip;
        public string _title;
        private object _htmlAttributes;
        private string _parentDivCSS;
        private bool _isParentDiv;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private string _bindProp;
        private string _functionName;

        private StateType _state;
        private bool _isStateSet;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        #endregion

        #region public properties
        public bool IsChecked { get { return _isChecked; } }
        public string Id { get { return _id; } }
        public bool IsDisabled { get { return _isDisable; } }
        public string Title { get { return _title; } }
        public StateType State { get { return _state; } }
        public bool IsStateSet { get { return _isStateSet; } }
        public object GetHtmlAttributes { get { return _htmlAttributes; } }
        #endregion

        #region ctor
        public BSCheckBox(HtmlHelper htmlHelper, string id)
        {
            this._id = id;
            this._htmlHelper = htmlHelper;
            _isChecked = false;
        }
        #endregion

        #region fluent methods
        public IBSCheckBox SetName(string name)
        {
            this._name = name;
            return this;
        }
        public IBSCheckBox SetChecked(bool isChecked)
        {
            this._isChecked = isChecked;
            return this;
        }
        public IBSCheckBox SetToolTip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSCheckBox SetVisibility(bool isVisible)
        {
            this._isVisible = isVisible;
            return this;
        }
        public IBSCheckBox SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }
        public IBSCheckBox SetTitle(string title)
        {
            this._title = title;
            return this;
        }
        public IBSCheckBox SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSCheckBox SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSCheckBox SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSCheckBox SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSCheckBox SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSCheckBox SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSCheckBox SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSCheckBox SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            _isParentDiv = true;
            return this;
        }
        public IBSCheckBox SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
            return this;
        }
        public IBSCheckBox SetState(StateType state = StateType.@default)
        {
            this._state = state;
            _isStateSet = true;
            return this;
        }

        public IBSCheckBox SetCallBackFunction(string functionName)
        {
            _functionName = functionName;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { });
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);
            attributes = attributes.SetDisplayAttributes(_isParentDiv, _displaySource, _displayCondition);
            
            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            TagBuilder divContainer = new TagBuilder("div");

            bool isColAdded = false;
            if (!_parentDivCSS.IsBlank())
            {
                divContainer.AddCssClass(_parentDivCSS);
                if (_parentDivCSS.Contains("col "))
                    isColAdded = true;
            }
            if (!isColAdded)
                divContainer.AddCssClass("col");

            if (!_displaySource.IsBlank())
            {
                divContainer.MergeAttribute("data-display-source", _displaySource);
                divContainer.MergeAttribute("data-display-condition", _displayCondition);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredAttributes(_displaySource, _displayCondition);
            }

            if (!_displaySourceExp.IsBlank())
            {
                divContainer.MergeAttribute("data-display-source-exp", _displaySourceExp);
                divContainer.MergeAttribute("data-display-expression", _displayExp);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredExpressionAttributes(_displaySourceExp, _displayExp);
            }

            TagBuilder label = new TagBuilder("label");

            label.AddCssClass("m-checkbox");

            if (_isDisable)
                label.AddCssClass("m-checkbox--disabled");

            if (_isSolid)
                label.AddCssClass("m-checkbox--air m-checkbox--solid");

            if (_state != StateType.@default)            
                label.AddCssClass($"m-checkbox--state-{_state.ToString()}");            

            TagBuilder spanTag = new TagBuilder("span");

           

            label.InnerHtml = _htmlHelper.CheckBox(_id, _isChecked, attributes).ToString();
            label.InnerHtml += _title;
            label.InnerHtml += spanTag.ToString();

            divContainer.InnerHtml = label.ToString();
            return divContainer.ToString();
        }


    }
}
