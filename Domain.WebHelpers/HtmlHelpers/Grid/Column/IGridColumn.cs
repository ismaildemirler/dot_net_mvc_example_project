using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.Column
{
    public interface IGridColumn<T>
    {
        IGridColumn<T> SetCustomTitle(string customTitle);
        IGridColumn<T> SetSortable(bool isSortable);
        IGridColumn<T> SetExportable(bool isExportable);
        IGridColumn<T> SetFixed(bool isFixed, GridColumDirection direction = GridColumDirection.left);
        IGridColumn<T> SetWidth(string width);
        IGridColumn<T> SetClassName(string className);
        IGridColumn<T> SetDateFormat(string dateFormat = "DD/MM/YYYY");
        IGridColumn<T> SetVisible(bool isVisible);
        IGridColumn<T> SetDetailColumn();
        IGridColumn<T> SetColumnType(GridColumnType columnType);

        IGridColumn<T> SetColumnLength(int length);
    }
}
