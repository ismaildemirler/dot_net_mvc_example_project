using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextBox
{
    public class BSTextBoxFor<TModel, TProperty> : IBSTextBoxFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;

        private LayoutType _layoutType;
        private MaskType _maskType;
        private CharacterType _characterType;
        private List<AddOn> _addOnList;
        private AddOn _addOn;

        private bool _isReadOnly;
        private bool _isDisable;
        private bool _isMaskSet;
        private bool _isTitleHidden;
        private bool _isMultipleAddOn;
        private bool _isHidden;
        private bool _isRangeSet;

        private string _id;
        private string _customTitle;
        private string _placeholder;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private object _htmlAttributes;
        private int _characterSize;
        private int _labelWidth = 2;
        private int _minNumber;
        private long _maxNumber;

        private string _mask;
        private string _requiredSource;
        private string _requiredCondition;

        private string _requiredSourceExp;
        private string _requiredExp;

        private string _displaySourceExp;
        private string _displayExp;
        private bool _isAlsoRequired;
        private string _displaySource;
        private string _displayCondition;
        private string _bindProp;
        private string _rangeMessage;
        private object _value;
        #endregion

        #region ctor
        public BSTextBoxFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            _id = "";
            _expression = expression;
            _htmlHelper = htmlHelper;
            _isReadOnly = false;
            _isDisable = false;
            _addOnList = new List<AddOn>();
        }
        #endregion

        #region fluent methods
        public IBSTextBoxFor<TModel> SetCustomID(string id)
        {
            this._id = id;
            return this;
        }

        public IBSTextBoxFor<TModel> SetValue(object value)
        {
            _value = value;
            return this;
        }
        public IBSTextBoxFor<TModel> SetHidden(bool isHidden)
        {
            this._isHidden = isHidden;
            return this;
        }
        public IBSTextBoxFor<TModel> SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }
        public IBSTextBoxFor<TModel> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IBSTextBoxFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSTextBoxFor<TModel> SetLayout(LayoutType layoutType)
        {
            _layoutType = layoutType;
            return this;
        }
        public IBSTextBoxFor<TModel> SetDisable(bool isDisable)
        {
            this._isDisable = isDisable;
            return this;
        }
        public IBSTextBoxFor<TModel> SetPlaceholder(string placeholder)
        {
            this._placeholder = placeholder;
            return this;
        }
        public IBSTextBoxFor<TModel> SetHelpText(string helpText)
        {
            this._helpText = helpText;
            return this;
        }
        public IBSTextBoxFor<TModel> SetReadOnly(bool isReadOnly)
        {
            this._isReadOnly = isReadOnly;
            return this;
        }
        public IBSTextBoxFor<TModel> SetTooltip(string tooltip)
        {
            this._tooltip = tooltip;
            return this;
        }
        public IBSTextBoxFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSTextBoxFor<TModel> SetCustomMask(string mask = "")
        {
            this._mask = mask;
            return this;
        }
        public IBSTextBoxFor<TModel> SetMaskType(MaskType maskType)
        {
            this._isMaskSet = true;
            this._maskType = maskType;
            return this;
        }
        public IBSTextBoxFor<TModel> SetRange(int minNumber, long maxNumber, string rangeMessage = "")
        {
            if (!_isMaskSet)
            {
                throw new Exception("Bu özellik sadece SetMaskType.Number veya SetMaskType.Decimal için kullanılabilir.");
            }
            this._minNumber = minNumber;
            this._maxNumber = maxNumber;
            this._rangeMessage = rangeMessage;
            _isRangeSet = true;
            return this;
        }
        public IBSTextBoxFor<TModel> SetBindProp(string bindProp)
        {
            this._bindProp = bindProp;
            return this;
        }
        public IBSTextBoxFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSTextBoxFor<TModel> SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSTextBoxFor<TModel> SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextBoxFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextBoxFor<TModel> SetCharacterSize(int size, CharacterType characterType = CharacterType.All)
        {
            this._characterSize = size;
            this._characterType = characterType;
            return this;
        }
        public IBSTextBoxFor<TModel> SetTitleHidden(bool isHidden)
        {
            this._isTitleHidden = isHidden;
            return this;
        }
        public IBSTextBoxFor<TModel> SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left)
        {
            if (_isMultipleAddOn)
            {
                _addOnList.Add(new AddOn()
                {
                    Text = addOnText,
                    Direction = direction
                });
            }
            else
            {
                if (_addOn != null)
                    throw new Exception("Zaten add-on eklenmiş görünmektedir. Multiple add-on eklemek için SetMultipleAddOn özelliğini true yapmalısınız.");

                _addOn = new AddOn()
                {
                    Text = addOnText,
                    Direction = direction
                };
            }
            return this;
        }
        public IBSTextBoxFor<TModel> SetMultipleAddOn(bool isMultipleAddOn)
        {
            this._isMultipleAddOn = isMultipleAddOn;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input", type = "text" });

            if (_isHidden)
                attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input" });

            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);

            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetRequiredExpressionAttributes(_requiredSourceExp, _requiredExp);
            attributes = attributes.SetReadOnlyAttributes(_isReadOnly);
            attributes = attributes.SetPlaceHolderAttributes(_placeholder);

            if (!_mask.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_mask = _mask });
            else if (_isMaskSet)
            {
                attributes = Util.MergeHtmlAttributes(attributes, new {data_mask_type = _maskType});
                if (_isRangeSet && (_maskType==MaskType.Number || _maskType ==MaskType.Decimal))
                {
                    _rangeMessage = !_rangeMessage.IsBlank() ? _rangeMessage : string.Format("{0} alanı {1} ile {2} arasında olmalıdır", metadata.DisplayName ?? metadata.PropertyName, _minNumber, _maxNumber);
                    attributes = Util.MergeHtmlAttributes(attributes, new { data_val_range_max = _maxNumber, data_val_range_min = _minNumber, data_val_range = _rangeMessage });
                }
            }

            if (_characterSize > 0)
                attributes = Util.MergeHtmlAttributes(attributes, new { data_mask_repeat = _characterSize, data_mask_repeat_type = _characterType });

            if (!_id.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { id = _id });

            TagBuilder divParent = new TagBuilder("div");
            if (_isHidden)
                divParent.AddCssClass("d-none");

            divParent.AddCssClass("m-form p-2");

            if (!_parentDivCSS.IsBlank())
            {
                divParent.AddCssClass(_parentDivCSS);
            }

            if (!_displaySource.IsBlank())
            {
                divParent.MergeAttribute("data-display-source", _displaySource);
                divParent.MergeAttribute("data-display-condition", _displayCondition);
                if(_isAlsoRequired)
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

            Type type = typeof(TModel);
            var propInfo = Util.GetPropertyInfo(_expression);
            var display = (DisplayAttribute)propInfo.GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();
            string name = propInfo.Name;

            if (display != null && _helpText.IsBlank())
                _helpText = display.Description;

            string input = _htmlHelper.TextBoxFor(_expression, _value == null ? "" : _value.ToString(), attributes).ToString();
            TagBuilder inputGroup = null;

            #region addOn
            if (_addOnList.Count == 0 && _addOn != null)
            {
                _addOnList.Add(_addOn);
            }

            if (_addOnList.Count > 0)
            {
                TagBuilder inputGroupPrepend = null;
                if ((_addOnList.Count(w => w.Direction == GridColumDirection.left) > 0))
                {
                    inputGroupPrepend = new TagBuilder("div");
                    inputGroupPrepend.AddCssClass("input-group-prepend");

                    foreach (var leftAddOn in _addOnList.Where(w => w.Direction == GridColumDirection.left))
                    {
                        TagBuilder inputGroupText = new TagBuilder("span");
                        inputGroupText.AddCssClass("input-group-text");
                        inputGroupText.SetInnerText(leftAddOn.Text);
                        inputGroupPrepend.InnerHtml += inputGroupText;
                    }
                }

                TagBuilder inputGroupAppend = null;
                if ((_addOnList.Count(w => w.Direction == GridColumDirection.right) > 0))
                {
                    inputGroupAppend = new TagBuilder("div");
                    inputGroupAppend.AddCssClass("input-group-append");

                    foreach (var rightAddOn in _addOnList.Where(w => w.Direction == GridColumDirection.right))
                    {
                        TagBuilder inputGroupText = new TagBuilder("span");
                        inputGroupText.AddCssClass("input-group-text");
                        inputGroupText.SetInnerText(rightAddOn.Text);
                        inputGroupAppend.InnerHtml += inputGroupText;
                    }
                }

                inputGroup = new TagBuilder("div");

                if (_layoutType == LayoutType.horizontal)
                {
                    inputGroup.AddCssClass($"{ (_isTitleHidden ? "" : " col-lg-9 ")} col-sm-12");

                    TagBuilder group = new TagBuilder("div");
                    group.AddCssClass("input-group");

                    if (inputGroupPrepend != null)
                        group.InnerHtml += inputGroupPrepend.ToString();

                    group.InnerHtml += input;

                    if (inputGroupAppend != null)
                        group.InnerHtml += inputGroupAppend.ToString();

                    inputGroup.InnerHtml += group;

                    TagBuilder divValidation = new TagBuilder("div");
                    divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();
                    inputGroup.InnerHtml += divValidation.ToString();

                    if (!_helpText.IsBlank())
                    {
                        TagBuilder spanHelpText = new TagBuilder("span");
                        spanHelpText.AddCssClass("m-form__help");
                        spanHelpText.InnerHtml = _helpText;
                        inputGroup.InnerHtml += spanHelpText.ToString();
                    }
                }
                else
                {
                    inputGroup.AddCssClass("input-group");

                    if (inputGroupPrepend != null)
                        inputGroup.InnerHtml += inputGroupPrepend.ToString();

                    inputGroup.InnerHtml += input;

                    if (inputGroupAppend != null)
                        inputGroup.InnerHtml += inputGroupAppend.ToString();
                }
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
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                if (inputGroup != null)
                    divContainer.InnerHtml += inputGroup.ToString();
                else
                    divContainer.InnerHtml += input;

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

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
                    label.SetInnerText(_customTitle);
                    divContainer.InnerHtml = label.ToString();
                }

                if (inputGroup != null)
                    divContainer.InnerHtml += inputGroup.ToString();
                else
                {
                    TagBuilder divInput = new TagBuilder("div");
                    divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");
                    divInput.InnerHtml += input;

                    TagBuilder divValidation = new TagBuilder("div");
                    divValidation.InnerHtml += _htmlHelper.ValidationMessageFor(_expression).ToString();

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
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();
        }
    }
}