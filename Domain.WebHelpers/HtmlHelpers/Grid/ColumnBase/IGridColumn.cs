using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.Column
{
    public interface IGridColumn
    {
        IGridColumn SetTitle(string title);
        IGridColumn SetSortable(bool isSortable);
        IGridColumn SetExportable(bool isExportable);
        IGridColumn SetFixed(bool isFixed, GridColumDirection direction = GridColumDirection.left);
        IGridColumn SetWidth(string width);
        IGridColumn SetClassName(string className);
        IGridColumn SetDateFormat(string dateFormat = "DD/MM/YYYY");
        IGridColumn SetVisible(bool isVisible);
        IGridColumn SetDetailColumn();
        IGridColumn SetColumnType(GridColumnType columnType);
        IGridColumn SetColumnLength(int length);
    }
}
