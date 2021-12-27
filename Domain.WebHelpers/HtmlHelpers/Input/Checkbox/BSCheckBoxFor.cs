using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.Checkbox
{
    public class BSCheckBoxFor<TModel> : IBSCheckBoxFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private string _name;
        private string _id;
        private bool _isDisable;
        private bool _isVisible;
        private object _htmlAttributes;
        private string _parentDivCSS;
        private bool _isParentDiv;
        private Expression<Func<TModel, bool>> _expression;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private string _bindProp;
        private string _functionName;
        private string _customClasses;
        private string _customTitle;

        private StateType _state;
        private bool _isSolid;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        #endregion

        #region ctor
        public BSCheckBoxFor(HtmlHelper<TModel> helper, Expression<Func<TModel, bool>> expression)
        {
            this._expression = expression;
            _htmlHelper = helper;
        }
        #endregion

        #region fluent methods
        public IBSCheckBoxFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetCustomClasses(string customClasses)
        {
            this._customClasses = customClasses;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetVisibility(bool isVisible)
        {
            this._isVisible = isVisible;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            _isParentDiv = true;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetSolid(bool isSolid)
        {
            _isSolid = isSolid;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetState(StateType state = HtmlHelpers.Input.Checkbox.Enum.StateType.@default)
        {
            this._state = state;
            return this;
        }
        public IBSCheckBoxFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { });
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetTooltipAttributes(metadata.Description);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);
            attributes = attributes.SetDisplayAttributes(_isParentDiv, _displaySource, _displayCondition);

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

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

            if (_isSolid)
                label.AddCssClass("m-checkbox--air m-checkbox--solid");

            if (_isDisable)
                label.AddCssClass("m-checkbox--disabled");

            if (_state != HtmlHelpers.Input.Checkbox.Enum.StateType.@default)
            {
                label.AddCssClass($"m-checkbox--state-{_state}");
            }

            if (!String.IsNullOrWhiteSpace(_customClasses))
            {
                label.AddCssClass($" " + _customClasses);
            }

            label.InnerHtml = _htmlHelper.CheckBoxFor(_expression, attributes).ToString();
            if (!string.IsNullOrEmpty(_customTitle))
                label.InnerHtml += _customTitle;
            else
                label.InnerHtml += metadata.DisplayName ?? metadata.PropertyName;

            TagBuilder span = new TagBuilder("span");
            label.InnerHtml += span.ToString();

            divContainer.InnerHtml = label.ToString();
            return divContainer.ToString();
        }
    }
}
