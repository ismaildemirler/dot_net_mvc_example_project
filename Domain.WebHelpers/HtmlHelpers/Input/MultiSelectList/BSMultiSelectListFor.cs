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
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.MultiSelectList
{
    public class BSMultiSelectListFor<TModel, TProperty> : IBSMultiSelectListFor<TModel>, IHtmlString
    {
        #region private properties
        HtmlHelper<TModel> _htmlHelper;
        Expression<Func<TModel, TProperty>> _expression;
        IEnumerable<SelectListItem> _selectList;
        LayoutType _layoutType;

        private bool _isDisable;
        private bool _isTitleHidden;

        private string _id;
        private string _customTitle;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _functionName;
        private object _htmlAttributes;

        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private bool _isAlsoRequired;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;
        private int _labelLength;
        #endregion

        #region ctor
        public BSMultiSelectListFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            this._expression = expression;
            this._htmlHelper = htmlHelper;
            this._isDisable = false;
            this._id = "";
            _labelLength = 2;
        }
        #endregion

        #region fluent methods
        public IBSMultiSelectListFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetLayout(LayoutType layoutType)
        {
            this._layoutType = layoutType;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            this._selectList = selectList;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSMultiSelectListFor<TModel> SetCallBackFunction(string functionName)
        {
            this._functionName = functionName;
            return this;
        }

        public IBSMultiSelectListFor<TModel> SetLabelWidth(int length)
        {
            _labelLength = length;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            var metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            var modelValues = metadata.Model;
            var values = new List<string>();

            if (modelValues != null)
            {
                if (modelValues.GetType() == typeof(int[]))
                {
                    values = ((int[])modelValues).Select(p => p.ToString()).ToList();
                }
                else if (modelValues.GetType() == typeof(string[]))
                {
                    values = ((string[])modelValues).ToList();
                }
            }
            
            if (_selectList == null)
            {
                _selectList = Enumerable.Empty<SelectListItem>();
            }
            else
            {
                foreach (var item in _selectList)
                {
                    item.Selected = values.Contains(item.Value);
                }
            }

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input select2-multiple", multiple = "multiple", data_toggle = "select2" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);

            if (!_functionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _functionName });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            var divParent = new TagBuilder("div");
            divParent.AddCssClass("m-form p-2");

            var isColAdded = false;
            if (!_parentDivCSS.IsBlank())
            {
                divParent.AddCssClass(_parentDivCSS);
                if (_parentDivCSS.Contains("col "))
                    isColAdded = true;
            }
            if (!isColAdded)
                divParent.AddCssClass("col");

            if (!_displaySource.IsBlank())
            {
                divParent.MergeAttribute("data-display-source", _displaySource);
                divParent.MergeAttribute("data-display-condition", _displayCondition);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredAttributes(_displaySource, _displayCondition);
            }

            if (!_displaySourceExp.IsBlank())
            {
                divParent.MergeAttribute("data-display-source-exp", _displaySourceExp);
                divParent.MergeAttribute("data-display-expression", _displayExp);
                if (_isAlsoRequired)
                    attributes = attributes.SetRequiredExpressionAttributes(_displaySourceExp, _displayExp);
            }

            if (_customTitle.IsBlank())
                _customTitle = metadata.DisplayName ?? metadata.PropertyName;

            if (_id.IsBlank())
                _id = metadata.PropertyName;

            var type = typeof(TModel);
            var propInfo = Util.GetPropertyInfo(_expression);
            var name = propInfo.Name;
            var display = (DisplayAttribute)propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (display != null && _helpText.IsBlank())
                _helpText = display.Description;

            var divContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
            {
                divContainer.AddCssClass("form-group m-form__group");

                if (!_isTitleHidden)
                {
                    var label = new TagBuilder("label");
                    label.MergeAttribute("for", _id);
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                divContainer.InnerHtml += _htmlHelper.ListBoxFor(_expression, _selectList, attributes).ToString();
                
                var divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

                divContainer.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    var spanHelpText = new TagBuilder("span");
                    spanHelpText.AddCssClass("m-form__help");
                    spanHelpText.InnerHtml = _helpText;
                    divContainer.InnerHtml += spanHelpText.ToString();
                }
            }
            else
            {
                var rowLength = 12;
                var elementLength = rowLength - _labelLength;
                divContainer.AddCssClass("form-group m-form__group row");

                if (!_isTitleHidden)
                {
                    var label = new TagBuilder("label");
                    label.AddCssClass($"col-form-label col-lg-{_labelLength} col-sm-12");
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                var divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{elementLength} ")}col-sm-12");

                divInput.InnerHtml += _htmlHelper.ListBoxFor(_expression, _selectList, attributes).ToString();

                var divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

                divInput.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    var spanHelpText = new TagBuilder("span");
                    spanHelpText.AddCssClass("m-form__help");
                    spanHelpText.InnerHtml = _helpText;
                    divInput.InnerHtml += spanHelpText.ToString();
                }
                divContainer.InnerHtml += divInput.ToString();
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();
        }
    }
}
