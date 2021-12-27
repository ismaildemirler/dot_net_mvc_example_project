using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Infrastructure.Web;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.Infrastructre;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Domain.WebHelpers.HtmlHelpers.Input.AutoComplete
{
    public class BSAutoCompleteFor<TModel, TProperty> : IBSAutoCompleteFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;
        private IEnumerable<SelectListItem> _selectList;
        private LayoutType _layoutType;
        private Enums.AutoCompleteType _type;
        private List<BSAutoCompleteButton> _buttons;

        private bool _isDisable;
        private bool _isMultiple;
        private bool _isTitleHidden;
        private bool _isLockedAfterSelect;
        private int _nesneTipId;

        private string _id;
        private string _value;
        private string _customTitle;
        private string _placeholder;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _url;
        private string _callbackFunctionName;
        private string _clearcallbackFunctionName;
        private int _labelWidth;

        private int _limit;

        private object _htmlAttributes;
        private object _parameters;
        #endregion

        #region ctor
        public BSAutoCompleteFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            _expression = expression;
            _htmlHelper = htmlHelper;
            _isDisable = false;
            _id = "";
            _limit = 10;
            _isMultiple = false;
            _labelWidth = 3;
            _buttons = new List<BSAutoCompleteButton>();
        }
        #endregion

        #region fluent methods
        public IBSAutoCompleteFor<TModel> SetCustomID(string id)
        {
            _id = id;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetLayout(LayoutType layoutType)
        {
            _layoutType = layoutType;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetHelpText(string helpText)
        {
            _helpText = helpText;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetValue(string value)
        {
            _value = value;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetType(Enums.AutoCompleteType type)
        {
            _type = type;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetMultiple(bool isMultiple)
        {
            _isMultiple = isMultiple;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetLimit(int limit = 10)
        {
            _limit = limit;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetAdditionalParameters(object parameters)
        {
            _parameters = parameters;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetAutoCompleteActionUrl(string url, int nesneTipId)
        {
            _url = url;
            _nesneTipId = nesneTipId;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetTitleHidden(bool isHidden)
        {
            _isTitleHidden = isHidden;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetCallBackFunction(string functionName)
        {
            _callbackFunctionName = functionName;
            return this;
        }

        public IBSAutoCompleteFor<TModel> SetClearCallBackFunction(string functionName)
        {
            _clearcallbackFunctionName = functionName;
            return this;
        }

        public IBSAutoCompleteFor<TModel> SetLockAfterSelect(bool locked)
        {
            _isLockedAfterSelect = locked;
            return this;
        }
        public IBSAutoCompleteFor<TModel> SetButtons(List<BSAutoCompleteButton> buttons)
        {
            _buttons = buttons;
            return this;
        }

        public IBSAutoCompleteFor<TModel> SetLabelWidth(int labelWidth)
        {
            _labelWidth = labelWidth;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            var metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            if (_id.IsBlank())
                _id = metadata.PropertyName;

            if (_customTitle.IsBlank())
                _customTitle = metadata.DisplayName ?? metadata.PropertyName;


            var functionUrlValue = _url + "Value";
            var functionUrlValues = _url + "Values";

            if (_parameters == null)
                _parameters = new RouteValueDictionary();

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control typeahead", autocomplete = "off", data_multiple = _isMultiple, id = "_" + _id, data_target = _id });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetPlaceHolderAttributes(_placeholder);

            ((RouteValueDictionary)_parameters).Add("nesneTipId", _nesneTipId);

            attributes = Util.MergeHtmlAttributes(attributes, new { data_url_value = UrlHelpers.EncryptedAction(functionUrlValue, _parameters) });
            attributes = Util.MergeHtmlAttributes(attributes, new { data_url_values = UrlHelpers.EncryptedAction(functionUrlValues, _parameters) });

            ((RouteValueDictionary)_parameters).Add("limit", _limit);

            attributes = Util.MergeHtmlAttributes(attributes, new { data_url = UrlHelpers.EncryptedAction(_url, _parameters) });
            attributes = Util.MergeHtmlAttributes(attributes, new { data_locked_after_select = _isLockedAfterSelect });

            if (_parameters != null)
                attributes = Util.MergeHtmlAttributes(attributes, new { data_ekParametreler = _parameters.ToJson() });

            if (!_callbackFunctionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_callback = _callbackFunctionName });

            if (!_clearcallbackFunctionName.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_clearcallback = _clearcallbackFunctionName });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            var type = typeof(TModel);
            var propInfo = Util.GetPropertyInfo(_expression);
            var name = propInfo.Name;
            var display = (DisplayAttribute)propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (display != null && _helpText.IsBlank())
                _helpText = display.Description;

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

            if (_customTitle.IsBlank())
                _customTitle = _id;

            #region Autocomplete InputGroup
            var divInputGroup = new TagBuilder("div");
            divInputGroup.AddCssClass("input-group");
            divInputGroup.MergeAttribute("id", "__" + _id);

            var divInputGroupPrepend = new TagBuilder("div");
            divInputGroupPrepend.AddCssClass("input-group-prepend");

            var iPrepend = new TagBuilder("i");
            iPrepend.AddCssClass("fa fa-search");

            var spanPrepend = new TagBuilder("span");
            spanPrepend.AddCssClass("input-group-text");
            spanPrepend.InnerHtml = iPrepend.ToString();

            divInputGroupPrepend.InnerHtml = spanPrepend.ToString();

            divInputGroup.InnerHtml = divInputGroupPrepend.ToString();

            divInputGroup.InnerHtml += _htmlHelper.TextBox("_" + _id, "", attributes).ToString();
            divInputGroup.InnerHtml += _htmlHelper.HiddenFor(_expression).ToString();

            if (!_isDisable) //disable yapılmışsa clear gösterilmeyecek
            {
                var divInputGroupAppend = new TagBuilder("div");
                divInputGroupAppend.AddCssClass("input-group-append");

                var spanAppend = new TagBuilder("span");
                spanAppend.AddCssClass("input-group-text");

                var btnDelete = new TagBuilder("a");
                btnDelete.AddCssClass("autocomplete_clear");
                btnDelete.MergeAttribute("id", "ac_clear_" + _id);
                btnDelete.MergeAttribute("title", "Temizle");
                btnDelete.MergeAttribute("data-toggle", "tooltip");
                btnDelete.MergeAttribute("data-target", _id);

                var iAppend = new TagBuilder("i");
                iAppend.AddCssClass("fa fa-times text-danger");

                btnDelete.InnerHtml = iAppend.ToString();

                spanAppend.InnerHtml = btnDelete.ToString();

                divInputGroupAppend.InnerHtml = spanAppend.ToString();

                if (_buttons.Any())
                {
                    foreach (var button in _buttons)
                    {
                        var spanAppendButton = new TagBuilder("span");
                        spanAppendButton.AddCssClass("input-group-text");

                        var btnButton = new TagBuilder("a");
                        btnButton.MergeAttribute("id", button.id);

                        if (!string.IsNullOrEmpty(button.css))
                            btnButton.AddCssClass("button.css");

                        if (!string.IsNullOrEmpty(button.title))
                        {
                            btnButton.MergeAttribute("title", button.title);
                            btnButton.MergeAttribute("data-toggle", "tooltip");
                        }

                        if (!string.IsNullOrEmpty(button.url))
                            btnButton.MergeAttribute("href", button.url);

                        if (button.htmlAttributes != null)
                            btnButton.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(button.htmlAttributes));

                        var iAppendButton = new TagBuilder("i");
                        if (!string.IsNullOrEmpty(button.icon))
                            iAppendButton.AddCssClass(button.icon);

                        btnButton.InnerHtml = iAppendButton.ToString();

                        spanAppendButton.InnerHtml = btnButton.ToString();

                        divInputGroupAppend.InnerHtml += spanAppendButton.ToString();
                    }
                }
                divInputGroup.InnerHtml += divInputGroupAppend.ToString();
            }

            TagBuilder divMultiple = null;
            if (_isMultiple)
            {
                divMultiple = new TagBuilder("div");
                divMultiple.AddCssClass("ac_selection");
                divMultiple.MergeAttribute("id", "ac_selection_" + _id);
            }
            #endregion

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

                divContainer.InnerHtml += divInputGroup.ToString();

                if (divMultiple != null)
                    divContainer.InnerHtml += divMultiple.ToString();

                var divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();

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
                divContainer.AddCssClass("form-group m-form__group row");

                if (!_isTitleHidden)
                {
                    var label = new TagBuilder("label");
                    label.AddCssClass($"col-form-label col-lg-{_labelWidth} col-sm-12");
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                var divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");

                divInput.InnerHtml += divInputGroup.ToString();
                if (divMultiple != null)
                    divInput.InnerHtml += divMultiple.ToString();

                var divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();

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
