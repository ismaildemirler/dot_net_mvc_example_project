using System;
using System.Linq.Expressions;
using Domain.WebHelpers.HtmlHelpers.Grid.Column.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Grid.Column
{
    public class GridColumn<T> : IGridColumn<T> where T : class
    {
        public Expression<Func<T, object>> expression;

        private string _customTitle;
        private string _customDataIndex;
        private string _width;
        private string _className;
        private bool _isSortable;
        private bool _isExportable;
        private bool _isFixed;
        private string _dateFormat;
        private bool _isVisible;
        private bool _isDetailColumn;
        private GridColumnType _columnType;
        private int _columnLength;
        

        #region ctor
        public GridColumn(Expression<Func<T, object>> exp)
        {
            expression = exp;
            _isVisible = true;
            _isDetailColumn = false;
            _columnType = GridColumnType.Text;
            _isExportable = true;
            _columnLength = 0;
        }
        #endregion

        #region Readonly Properties
        public string CustomTitle { get { return _customTitle; } }
        public string CustomDataIndex { get { return _customDataIndex; } }
        public string Width { get { return _width; } }
        public string ClassName { get { return _className; } }
        public bool IsSortable { get { return _isSortable; } }
        public bool IsExportable { get { return _isExportable; } }
        public bool IsFixed { get { return _isFixed; } }
        public bool IsVisible { get { return _isVisible; } }
        public string DateFormat { get { return _dateFormat; } }
        public bool IsDetail { get { return _isDetailColumn; } }
        public int ColumnLength { get { return _columnLength; } }
        #endregion

        #region Fluent Methods
        public IGridColumn<T> SetVisible(bool visible)
        {
            this._isVisible = visible;
            return this;
        }
        public IGridColumn<T> SetCustomTitle(string customTitle)
        {
            this._customTitle = customTitle;
            return this;
        }
        public IGridColumn<T> SetSortable(bool isSortable)
        {
            this._isSortable = isSortable;
            return this;
        }

        public IGridColumn<T> SetExportable(bool isExportable)
        {
            this._isExportable = isExportable;
            return this;
        }

        public IGridColumn<T> SetFixed(bool isFixed, GridColumDirection direction = GridColumDirection.left)
        {
            this._isFixed = isFixed;
            return this;
        }
        public IGridColumn<T> SetWidth(string width)
        {
            _width = width;
            return this;
        }
        public IGridColumn<T> SetClassName(string className)
        {
            this._className = className;
            return this;
        }

        public IGridColumn<T> SetCustomDataIndex(string customDataIndex)
        {
            this._customDataIndex = customDataIndex;
            return this;
        }

        public IGridColumn<T> SetDateFormat(string dateFormat = "DD/MM/YYYY")
        {
            this._dateFormat = dateFormat;
            return this;
        }
        public IGridColumn<T> SetDetailColumn()
        {
            this._isDetailColumn= true;
            return this;
        }

        public IGridColumn<T> SetColumnType(GridColumnType columnType)
        {
            this._columnType = columnType;
            return this;
        }

        public IGridColumn<T> SetColumnLength(int length)
        {
            _columnLength = length;
            return this;
        }

        #endregion
    }
}
