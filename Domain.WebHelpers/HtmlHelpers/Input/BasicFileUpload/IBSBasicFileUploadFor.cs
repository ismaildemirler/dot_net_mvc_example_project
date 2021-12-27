using Domain.WebHelpers.HtmlHelpers.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.BasicFileUpload
{
    public interface IBSBasicFileUploadFor<TModel>
    {
        IBSBasicFileUploadFor<TModel> SetLayout(LayoutType layoutType);
        IBSBasicFileUploadFor<TModel> SetTitle(string title);
        IBSBasicFileUploadFor<TModel> SetHtmlAttributes(object htmlAttributes);
        IBSBasicFileUploadFor<TModel> SetPlaceholder(string placeholder);
        IBSBasicFileUploadFor<TModel> SetTooltip(string tooltip);
        IBSBasicFileUploadFor<TModel> SetHelpText(string helpText);
        IBSBasicFileUploadFor<TModel> SetDisable(bool isDisable);
        IBSBasicFileUploadFor<TModel> SetReadOnly(bool isReadOnly);
        IBSBasicFileUploadFor<TModel> SetParentCss(string css = "");
        IBSBasicFileUploadFor<TModel> SetBindProp(string bindProp);
        IBSBasicFileUploadFor<TModel> SetRequired(string source, string condition);
        IBSBasicFileUploadFor<TModel> SetDisplay(string source, string condition);
        IBSBasicFileUploadFor<TModel> SetTitleHidden(bool isHidden);
        IBSBasicFileUploadFor<TModel> SetAddOn(string addOnText, GridColumDirection direction = GridColumDirection.left);
        IBSBasicFileUploadFor<TModel> SetMultipleAddOn(bool isMultipleAddOn);
    }
}
