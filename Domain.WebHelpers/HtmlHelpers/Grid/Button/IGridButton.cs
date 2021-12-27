namespace Domain.WebHelpers.HtmlHelpers.Grid.Button
{
    public interface IGridButton
    {
        IGridButton SetID(string id);
        IGridButton SetTitle(string title);
        IGridButton SetTooltip(string tooltip);
        IGridButton SetActionUrl(string actionUrl);
        IGridButton SetCssClass(string cssClass);
        IGridButton SetIcon(string icon);
        IGridButton SetButtonType(GridButtonType buttonType);
        IGridButton SetJsFuntion(string jsFunction);
        IGridButton SetHtmlAttributes(object htmlAttributes);
    }

}