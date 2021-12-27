using System;
using Domain.WebHelpers.HtmlHelpers.Grid.Button;
using Domain.WebHelpers.HtmlHelpers.Grid.Column;
using Domain.WebHelpers.HtmlHelpers.Grid.Enum;
using static Domain.WebHelpers.Infrastructre.Enums;

namespace Domain.WebHelpers.HtmlHelpers.Grid
{
    public interface IBSGrid<T> where T : class
    {
        IBSGrid<T> SetTitle(string title);
        IBSGrid<T> SetColumns(Action<GridColumnBuilder<T>> columnBuilder);
        IBSGrid<T> SetToolbarButtons(Action<GridButtonBuilder> btnBuilder);
        IBSGrid<T> SetCssIcon(string cssIcon = "");
        IBSGrid<T> SetHeaderVisible(bool isHeaderVisible);
        IBSGrid<T> SetAjaxUrl(string ajaxUrl, object postData = null);
        IBSGrid<T> SetFunctionName(string functionName = "Listele");
        IBSGrid<T> SetDefaultOrder(int sortIndex, EnumSortType sortType = EnumSortType.desc);
        IBSGrid<T> SetFunctionCallInstantly(bool isFunctionCallInstantly = true);
        IBSGrid<T> SetPageLength(int pageLength = 10);
        IBSGrid<T> SetPaging(bool isPagingEnabled);
        IBSGrid<T> SetCallBackFunctionName(string callBackFunctionName);
        IBSGrid<T> SetEmptyText(string emptyText);
        IBSGrid<T> SetParentTable();
        IBSGrid<T> SetDetailTable();
        IBSGrid<T> SetDetailTablePartialUrl(string detailTablePartialUrl);
        IBSGrid<T> SetSelectionType(GridSelectionType selectionType);
        IBSGrid<T> SetExportButtonShow(bool showExportButton, bool exportAllData = false);
        IBSGrid<T> SetPageLengthShow(bool showPageLength);
        IBSGrid<T> SetShowRowNumber(bool showRowNumber);
        IBSGrid<T> SetSearchFilterGuid(string searhFilterGuid);
    }

    public interface IBSGrid
    {
        IBSGrid SetTitle(string title);
        IBSGrid SetColumns(Action<GridColumnBuilder> columnBuilder);
        IBSGrid SetToolbarButtons(Action<GridButtonBuilder> btnBuilder);
        IBSGrid SetCssIcon(string cssIcon = "");
        IBSGrid SetHeaderVisible(bool isHeaderVisible);
        IBSGrid SetAjaxUrl(string ajaxUrl, object postData = null);
        IBSGrid SetFunctionName(string functionName = "Listele");
        IBSGrid SetDefaultOrder(int sortIndex, EnumSortType sortType = EnumSortType.desc);
        IBSGrid SetFunctionCallInstantly(bool isFunctionCallInstantly = true);
        IBSGrid SetPageLength(int pageLength = 10);
        IBSGrid SetPaging(bool isPagingEnabled);
        IBSGrid SetCallBackFunctionName(string callBackFunctionName);
        IBSGrid SetEmptyText(string emptyText);
        IBSGrid SetParentTable();
        IBSGrid SetDetailTable();
        IBSGrid SetDetailTablePartialUrl(string detailTablePartialUrl);
        IBSGrid SetHeight(string height);
        IBSGrid SetSelectionType(GridSelectionType selectionType);
        IBSGrid SetExportButtonShow(bool showExportButton, bool exportAllData = false);
        IBSGrid SetPageLengthShow(bool showPageLength);
        IBSGrid SetShowRowNumber(bool showRowNumber);
        IBSGrid SetSearchFilterGuid(string searhFilterGuid);
    }
}
