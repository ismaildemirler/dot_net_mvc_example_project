using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.BasicFileUpload
{
    public class BSBasicFileUploadFor<TModel, TProperty> : IBSBasicFileUploadFor<TModel>, IHtmlString
    {
        #region private properties
        private HtmlHelper<TModel> _htmlHelper;
        private Expression<Func<TModel, TProperty>> _expression;

        private string _id;
        private string _title;
        private string _placeholder;
        private string _parentDivCSS;
        private string _helpText;
        private string _tooltip;
        private object _htmlAttributes;

        private LayoutType _layoutType;
        private List<AddOn> _addOnList;
        private AddOn _addOn;

        private bool _isReadOnly;
        private bool _isDisable;
        private bool _isTitleHidden;
        private bool _isMultipleAddOn;

        private string _bindProp;
        private string _requiredSource;
        private string _requiredCondition;
        private string _displaySource;
        private string _displayCondition;
        #endregion

        #region ctor
        public BSBasicFileUploadFor(HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
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
        public IBSBasicFileUploadFor<TModel> SetLayout(LayoutType layoutType)
        {
            _layoutType = layoutType;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetTitle(string title)
        {
            _title = title;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetPlaceholder(string placeholder)
        {
            _placeholder = placeholder;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetHelpText(string helpText)
        {
            _helpText = helpText;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetDisable(bool isDisable)
        {
            _isDisable = isDisable;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetReadOnly(bool isReadOnly)
        {
            _isReadOnly = isReadOnly;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetParentCss(string css = "")
        {
            if (!css.IsBlank())
            {
                _parentDivCSS = css;
            }
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetBindProp(string bindProp)
        {
            _bindProp = bindProp;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetRequired(string source, string condition)
        {
            _requiredSource = source;
            _requiredCondition = condition;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetDisplay(string source, string condition)
        {
            _displaySource = source;
            _displayCondition = condition;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetTitleHidden(bool isHidden)
        {
            _isTitleHidden = isHidden;
            return this;
        }

        public IBSBasicFileUploadFor<TModel> SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left)
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

        public IBSBasicFileUploadFor<TModel> SetMultipleAddOn(bool isMultipleAddOn)
        {
            _isMultipleAddOn = isMultipleAddOn;
            return this;
        }
        #endregion

        public string ToHtmlString()
        {
            ModelMetadata metadata = ModelMetadata.FromLambdaExpression(_expression, _htmlHelper.ViewData);

            StringBuilder sb = new StringBuilder();
            var attributes = Util.MergeHtmlAttributes(_htmlAttributes, new { @class = "custom-file-input", type = "file" });
            attributes = attributes.SetTooltipAttributes(_tooltip);
            attributes = attributes.SetDisableAttributes(_isDisable);
            attributes = attributes.SetBindPropAttributes(_bindProp);
            attributes = attributes.SetRequiredAttributes(_requiredSource, _requiredCondition);
            attributes = attributes.SetReadOnlyAttributes(_isReadOnly);
            attributes = attributes.SetPlaceHolderAttributes(_placeholder);

            TagBuilder divParent = new TagBuilder("div");
            divParent.AddCssClass("col m-form p-2");

            if (!_displaySource.IsBlank())
            {
                divParent.MergeAttribute("data-display-source", _displaySource);
                divParent.MergeAttribute("data-display-condition", _displayCondition);
            }

            if (!attributes.ContainsKey("id"))
                _id = metadata.PropertyName;

            if (_title.IsBlank())
                _title = metadata.DisplayName ?? metadata.PropertyName;

            sb.AppendFormat("<input type='file' name='{0}' {1}>", metadata.PropertyName, Util.GetHtmlAttributes(attributes));
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
                    group.AddCssClass("custom-file");

                    if (inputGroupPrepend != null)
                        group.InnerHtml += inputGroupPrepend.ToString();

                    group.InnerHtml += sb;

                    if (inputGroupAppend != null)
                        group.InnerHtml += inputGroupAppend.ToString();

                    inputGroup.InnerHtml += group;

                    TagBuilder divValidation = new TagBuilder("div");
                    divValidation.InnerHtml += _htmlHelper.ValidationMessage(metadata.PropertyName).ToString();
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

                    inputGroup.InnerHtml += sb;

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
                    label.MergeAttribute("for", metadata.PropertyName);
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                TagBuilder divCustomFile = new TagBuilder("div");
                divCustomFile.AddCssClass("custom-file");

                if (inputGroup != null)
                    divCustomFile.InnerHtml += inputGroup.ToString();
                else
                    divCustomFile.InnerHtml += sb;

                TagBuilder labelFile = new TagBuilder("label");
                labelFile.AddCssClass("custom-file-label");
                labelFile.MergeAttribute("for", metadata.PropertyName);
                labelFile.InnerHtml = "Dosya Seçin...";
                divCustomFile.InnerHtml += labelFile.ToString();

                TagBuilder divValidation = new TagBuilder("div");
                divValidation.InnerHtml += _htmlHelper.ValidationMessage(metadata.PropertyName).ToString();

                divCustomFile.InnerHtml += divValidation.ToString();

                if (!_helpText.IsBlank())
                {
                    TagBuilder spanHelpText = new TagBuilder("span");
                    spanHelpText.AddCssClass("m-form__help");
                    spanHelpText.InnerHtml = _helpText;
                    divContainer.InnerHtml += spanHelpText.ToString();
                }

                divContainer.InnerHtml += divCustomFile.ToString();
            }
            else
            {
                divContainer.AddCssClass("form-group m-form__group row");

                if (!_isTitleHidden)
                {
                    TagBuilder label = new TagBuilder("label");
                    label.AddCssClass("col-form-label col-lg-2 col-sm-12");
                    label.SetInnerText(_title);
                    divContainer.InnerHtml = label.ToString();
                }

                if (inputGroup != null)
                    divContainer.InnerHtml += inputGroup.ToString();
                else
                {
                    TagBuilder divInput = new TagBuilder("div");
                    divInput.AddCssClass($"{(_isTitleHidden ? "" : "col-lg-10 ")}col-sm-12");

                    TagBuilder divCustomFile = new TagBuilder("div");
                    divCustomFile.AddCssClass("custom-file");
                    divCustomFile.InnerHtml += sb;

                    TagBuilder labelFile = new TagBuilder("label");
                    labelFile.AddCssClass("custom-file-label");
                    labelFile.MergeAttribute("for", metadata.PropertyName);
                    labelFile.InnerHtml = !_placeholder.IsBlank() ? _placeholder : "Dosya Seçin...";
                    divCustomFile.InnerHtml += labelFile.ToString();

                    TagBuilder divValidation = new TagBuilder("div");
                    divValidation.InnerHtml += _htmlHelper.ValidationMessage(metadata.PropertyName).ToString();

                    divInput.InnerHtml += divCustomFile.ToString();
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
