using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Input.TextBox
{
    public class BSTextBox : IBSTextBox, IHtmlString
    {
        #region private properties
        private HtmlHelper _htmlHelper;

        private string _id;
        private object _value;
        private string _title;
        private string _placeholder;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private object _htmlAttributes;

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

        private int _characterSize;
        private int _labelWidth = 2;
        private string _mask;
        private string _bindProp;
        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        private bool _isAlsoRequired;
        private string _requiredSourceExp;
        private string _requiredExp;
        private string _displaySourceExp;
        private string _displayExp;

        #endregion

        #region ctor
        public BSTextBox(HtmlHelper htmlHelper, string id)
        {
            _id = id;
            _htmlHelper = htmlHelper;
            _isReadOnly = false;
            _isDisable = false;
            _addOnList = new List<AddOn>();
            _isAlsoRequired = false;
        }
        #endregion

        #region fluent methods
        public IBSTextBox SetTitle(string title)
        {
            _title = title;
            return this;
        }
        public IBSTextBox SetHidden(bool isHidden)
        {
            _isHidden = isHidden;
            return this;
        }
        public IBSTextBox SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSTextBox SetValue(object value)
        {
            _value = value;
            return this;
        }
        public IBSTextBox SetLayout(LayoutType layoutType)
        {
            _layoutType = layoutType;
            return this;
        }
        public IBSTextBox SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }
        public IBSTextBox SetPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }
        public IBSTextBox SetHelpText(string helpText)
        {
            _helpText = helpText;
            return this;
        }
        public IBSTextBox SetReadOnly(bool isReadOnly)
        {
            _isReadOnly = isReadOnly;
            return this;
        }
        public IBSTextBox SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }
        public IBSTextBox SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }
        public IBSTextBox SetCustomMask(string mask = "")
        {
            _mask = mask;
            return this;
        }
        public IBSTextBox SetMaskType(MaskType maskType)
        {
            _isMaskSet = true;
            _maskType = maskType;
            return this;
        }
        public IBSTextBox SetCharacterSize(int size, CharacterType characterType = CharacterType.All)
        {
            _characterSize = size;
            _characterType = characterType;
            return this;
        }
        public IBSTextBox SetLabelWidth(int labelWidth)
        {
            this._labelWidth = labelWidth;
            return this;
        }
        public IBSTextBox SetBindProp(string bindProp)
        {
            _bindProp = bindProp;
            return this;
        }
        public IBSTextBox SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }
        public IBSTextBox SetRequiredExpression(string source, string expression)
        {
            _requiredSourceExp = source;
            _requiredExp = expression;
            return this;
        }
        public IBSTextBox SetDisplay(string source, string condition, bool isAlsoRequired = false)
        {
            _displaySource = source;
            _displayCondition = condition;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextBox SetDisplayExpression(string source, string expression, bool isAlsoRequired = false)
        {
            _displaySourceExp = source;
            _displayExp = expression;
            _isAlsoRequired = isAlsoRequired;
            return this;
        }
        public IBSTextBox SetTitleHidden(bool isHidden)
        {
            _isTitleHidden = isHidden;
            return this;
        }
        public IBSTextBox SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left)
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
        public IBSTextBox SetMultipleAddOn(bool isMultipleAddOn)
        {
            this._isMultipleAddOn = isMultipleAddOn;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input", type = "text" });
            if (_isHidden)
                attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "form-control m-input", type = "hidden" });

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
                attributes = Util.MergeHtmlAttributes(attributes, new { data_mask_type = _maskType });

            if (_characterSize > 0)
                attributes = Util.MergeHtmlAttributes(attributes, new { data_mask_repeat = _characterSize, data_mask_repeat_type = _characterType });

            TagBuilder divParent = new TagBuilder("div");
            divParent.AddCssClass("m-form p-2");

            if (!_parentDivCSS.IsBlank())
            {
                divParent.AddCssClass(_parentDivCSS);
            }
            
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

            if (_title.IsBlank())
                _title = _id;

            string input = _htmlHelper.TextBox(_id, _value == null ? "" : _value.ToString(), attributes).ToString();
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
                    divValidation.InnerHtml += _htmlHelper.ValidationMessage(_id).ToString();
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
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                if (inputGroup != null)
                    divContainer.InnerHtml += inputGroup.ToString();
                else
                    divContainer.InnerHtml += input;

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

                if (inputGroup != null)
                    divContainer.InnerHtml += inputGroup.ToString();
                else
                {
                    TagBuilder divInput = new TagBuilder("div");
                    divInput.AddCssClass($"{(_isTitleHidden ? "" : $"col-lg-{12 - _labelWidth} ")}col-sm-12");
                    divInput.InnerHtml += input;

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
            }

            divParent.InnerHtml = divContainer.ToString();
            return divParent.ToString();

            //divValidation.InnerHtml += ;
        }
    }

    internal class AddOn
    {
        public string Text { get; set; }
        public GridColumDirection Direction { get; set; }
    }
}