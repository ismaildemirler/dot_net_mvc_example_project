using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.Infrastructure.Web;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.Infrastructre;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Domain.WebHelpers.HtmlHelpers.Input.AutoComplete
{
    public class BSAutoComplete : IBSAutoComplete, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;
        private IEnumerable<SelectListItem> _selectList;
        private Enums.AutoCompleteType _type;
        private LayoutType _layoutType;
        private string _url;
        private int _nesneTipId;
        private List<BSAutoCompleteButton> _buttons;

        private bool _isDisable;
        private bool _isMultiple;
        private bool _isTitleHidden;
        private bool _isLockedAfterSelect;

        private string _id;
        private string _value;
        private string _placeholder;
        private string _title;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private string _callbackFunctionName;
        private string _clearcallbackFunctionName;
        private int _labelWidth;

        private int _limit;

        private object _htmlAttributes;
        private object _parameters;
        #endregion

        #region ctor
        public BSAutoComplete(HtmlHelper htmlHelper, string id)
        {
            _id = id;
            _htmlHelper = htmlHelper;
            _limit = 10;
            _labelWidth = 3;
            _buttons = new List<BSAutoCompleteButton>();
        }
        #endregion

        #region fluent methods
        public IBSAutoComplete SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IBSAutoComplete SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSAutoComplete SetLayout(LayoutType layoutType)
        {
            _layoutType = layoutType;
            return this;
        }
        public IBSAutoComplete SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }
        public IBSAutoComplete SetHelpText(string helpText)
        {
            _helpText = helpText;
            return this;
        }
        public IBSAutoComplete SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }
        public IBSAutoComplete SetParentCss(string css)
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSAutoComplete SetValue(string value)
        {
            _value = value;
            return this;
        }
        public IBSAutoComplete SetPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }
        public IBSAutoComplete SetType(Enums.AutoCompleteType type)
        {
            _type = type;
            return this;
        }
        public IBSAutoComplete SetMultiple(bool isMultiple)
        {
            _isMultiple = isMultiple;
            return this;
        }
        public IBSAutoComplete SetLimit(int limit = 10)
        {
            _limit = limit;
            return this;
        }
        public IBSAutoComplete SetAdditionalParameters(object parameters)
        {
            _parameters = parameters;
            return this;
        }
        public IBSAutoComplete SetAutoCompleteActionUrl(string url, int nesneTipId)
        {
            _url = url;
            _nesneTipId = nesneTipId;
            return this;
        }
        public IBSAutoComplete SetTitleHidden(bool isHidden)
        {
            _isTitleHidden = isHidden;
            return this;
        }
        public IBSAutoComplete SetCallBackFunction(string functionName)
        {
            _callbackFunctionName = functionName;
            return this;
        }

        public IBSAutoComplete SetClearCallBackFunction(string functionName)
        {
            _clearcallbackFunctionName = functionName;
            return this;
        }

        public IBSAutoComplete SetLockAfterSelect(bool locked)
        {
            _isLockedAfterSelect = locked;
            return this;
        }

        public IBSAutoComplete SetButtons(List<BSAutoCompleteButton> buttons)
        {
            _buttons = buttons;
            return this;
        }
        public IBSAutoComplete SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }

        #endregion

        public string ToHtmlString()
        {
            string functionUrlValue = _url + "Value";
            string functionUrlValues = _url + "Values";

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

            if (_title.IsBlank())
                _title = _id;

            #region Autocomplete InputGroup
            TagBuilder divInputGroup = new TagBuilder("div");
            divInputGroup.AddCssClass("input-group");
            divInputGroup.MergeAttribute("id", "__" + _id);

            TagBuilder divInputGroupPrepend = new TagBuilder("div");
            divInputGroupPrepend.AddCssClass("input-group-prepend");

            TagBuilder iPrepend = new TagBuilder("i");
            iPrepend.AddCssClass("fa fa-search");

            TagBuilder spanPrepend = new TagBuilder("span");
            spanPrepend.AddCssClass("input-group-text");
            spanPrepend.InnerHtml = iPrepend.ToString();

            divInputGroupPrepend.InnerHtml = spanPrepend.ToString();

            divInputGroup.InnerHtml = divInputGroupPrepend.ToString();

            divInputGroup.InnerHtml += _htmlHelper.TextBox("_" + _id, "", attributes).ToString();
            divInputGroup.InnerHtml += _htmlHelper.Hidden(_id, _value).ToString();

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

            TagBuilder divContainer = new TagBuilder("div");
            if (_layoutType == LayoutType.vertical)
            {
                divContainer.AddCssClass("form-group m-form__group");

                if (!_isTitleHidden)
                {
                    TagBuilder label = new TagBuilder("label");
                    label.MergeAttribute("for", _id);
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                divContainer.InnerHtml += divInputGroup.ToString();

                if (divMultiple != null)
                    divContainer.InnerHtml += divMultiple.ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();

                divContainer.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    TagBuilder spanHelpText = new TagBuilder("span");
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
                    TagBuilder label = new TagBuilder("label");
                    label.AddCssClass($"col-form-label col-lg-{_labelWidth} col-sm-12");
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divInput = new TagBuilder("div");
                divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");

                divInput.InnerHtml += divInputGroup.ToString();
                if (divMultiple != null)
                    divInput.InnerHtml += divMultiple.ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();

                divInput.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    TagBuilder spanHelpText = new TagBuilder("span");
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