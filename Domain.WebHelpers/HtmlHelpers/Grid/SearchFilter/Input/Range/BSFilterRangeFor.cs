using System;
using System.Linq.Expressions;
using System.Web;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Range
{
    public class BSFilterRangeFor<T> : IBSFilterRangeFor, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _modalHtmlAttributes;
        private Expression<Func<T, object>> _column;

        public BSFilterRangeFor(Expression<Func<T, object>> column)
        {
            _inputType = FilterGroupType.Text;
            _column = column;
        }

        public IBSFilterRangeFor SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }
        public IBSFilterRangeFor SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterRangeFor SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterRangeFor SetHtmlAttributes(object htmlAttributes)
        {
            _modalHtmlAttributes = htmlAttributes;
            return this;
        }

        public string ToHtmlString()
        {
            var propInfo = Util.GetPropertyInfo<T>(_column);
            string customTitle = Util.GetDisplayName<T>(propInfo);
            var id = "";

            if (!_id.IsBlank())
            {
                id = _id;
            }
            else
            {
                id = propInfo.Name;
            }

            return ((BSFilterRange)(new BSFilterRange(id).SetTitle(customTitle).SetInputType(_inputType))).ToHtmlString();
        }
    }
}