using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;

namespace Domain.WebHelpers.HtmlHelpers.Grid.Column
{
    public class GridColumnBuilder : IList<GridColumn>
    {
        private readonly ModelMetadataProvider _metadataProvider;
        private readonly List<GridColumn> _columns = new List<GridColumn>();

        public GridColumnBuilder() : this(ModelMetadataProviders.Current)
        {

        }

        private GridColumnBuilder(ModelMetadataProvider metadataProvider)
        {
            _metadataProvider = metadataProvider;
        }

        public IGridColumn Add(string columnName)
        {
            var column = new GridColumn(columnName);
            Add(column);

            return column;
        }

        protected IList<GridColumn> Columns
        {
            get { return _columns; }
        }

        public IEnumerator<GridColumn> GetEnumerator()
        {
            return _columns
                .GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static MemberExpression GetMemberExpression(LambdaExpression expression)
        {
            return RemoveUnary(expression.Body) as MemberExpression;
        }

        private static System.Type GetTypeFromMemberExpression(MemberExpression memberExpression)
        {
            if (memberExpression == null) return null;

            var dataType = GetTypeFromMemberInfo(memberExpression.Member, (PropertyInfo p) => p.PropertyType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (MethodInfo m) => m.ReturnType);
            if (dataType == null) dataType = GetTypeFromMemberInfo(memberExpression.Member, (FieldInfo f) => f.FieldType);

            return dataType;
        }

        private static System.Type GetTypeFromMemberInfo<TMember>(MemberInfo member, Func<TMember, System.Type> func) where TMember : MemberInfo
        {
            if (member is TMember)
            {
                return func((TMember)member);
            }
            return null;
        }

        private static Expression RemoveUnary(Expression body)
        {
            var unary = body as UnaryExpression;
            if (unary != null)
            {
                return unary.Operand;
            }
            return body;
        }

        protected virtual void Add(GridColumn column)
        {
            _columns.Add(column);
        }

        void ICollection<GridColumn>.Add(GridColumn column)
        {
            Add(column);
        }

        void ICollection<GridColumn>.Clear()
        {
            _columns.Clear();
        }

        bool ICollection<GridColumn>.Contains(GridColumn column)
        {
            return _columns.Contains(column);
        }

        void ICollection<GridColumn>.CopyTo(GridColumn[] array, int arrayIndex)
        {
            _columns.CopyTo(array, arrayIndex);
        }

        bool ICollection<GridColumn>.Remove(GridColumn column)
        {
            return _columns.Remove(column);
        }

        int ICollection<GridColumn>.Count
        {
            get { return _columns.Count; }
        }

        bool ICollection<GridColumn>.IsReadOnly
        {
            get { return false; }
        }

        int IList<GridColumn>.IndexOf(GridColumn item)
        {
            return _columns.IndexOf(item);
        }

        void IList<GridColumn>.Insert(int index, GridColumn item)
        {
            _columns.Insert(index, item);
        }

        void IList<GridColumn>.RemoveAt(int index)
        {
            _columns.RemoveAt(index);
        }

        GridColumn IList<GridColumn>.this[int index]
        {
            get { return _columns[index]; }
            set { _columns[index] = value; }
        }
    }
}
