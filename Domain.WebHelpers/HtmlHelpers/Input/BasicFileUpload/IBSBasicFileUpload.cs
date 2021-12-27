using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.BasicFileUpload
{
    public interface IBSBasicFileUpload
    {
        IBSBasicFileUpload SetLayout(LayoutType layoutType);
        IBSBasicFileUpload SetValue(string value);
        IBSBasicFileUpload SetTitle(string title);
        IBSBasicFileUpload SetHtmlAttributes(object htmlAttributes);
        IBSBasicFileUpload SetPlaceholder(string placeholder);
        IBSBasicFileUpload SetTooltip(string tooltip);
        IBSBasicFileUpload SetHelpText(string helpText);
        IBSBasicFileUpload SetDisable(bool isDisable);
        IBSBasicFileUpload SetReadOnly(bool isReadOnly);
        IBSBasicFileUpload SetParentCss(string css = "");
        IBSBasicFileUpload SetBindProp(string bindProp);
        IBSBasicFileUpload SetRequired(string source, string condition);
        IBSBasicFileUpload SetDisplay(string source, string condition);
        IBSBasicFileUpload SetTitleHidden(bool isHidden);
        IBSBasicFileUpload SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSBasicFileUpload SetMultipleAddOn(bool isMultipleAddOn);
    }
}
