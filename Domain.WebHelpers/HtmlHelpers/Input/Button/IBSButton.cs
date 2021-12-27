using Domain.WebHelpers.HtmlHelpers.Input.Button.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.Button
{
    public interface IBSButton
    {
        IBSButton SetTitle(string title);
        IBSButton SetActionUrl(string actionUrl);
        IBSButton SetIcon(string icon);
        IBSButton SetIconAlignment(ButtonIconAlignment iconAlignment);
        IBSButton SetImageUrl(string imageUrl);
        IBSButton SetSize(ButtonSize buttonSize);
        IBSButton SetDisabled(bool isDisabled);
        IBSButton SetType(ButtonType buttonType);
        IBSButton SetStyle(ButtonStyle buttonStyle);
        IBSButton SetBackButton(bool isBackButton);
        IBSButton SetHtmlAttributes(object htmlAttributes);
        IBSButton SetCustomClasses(string additionalClasses);
        IBSButton SetConfirm(string confirmText, string relatedFormId = "");
        IBSButton SetExportButton(bool isExport, int timeOutMiliSecond = 5000);
        IBSButton SetTooltip(string tooltip);
    }
}