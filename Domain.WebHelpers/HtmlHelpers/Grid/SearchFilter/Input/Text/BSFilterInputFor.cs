using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Text
{
    public class BSFilterInputFor<T> : IBSFilterInputFor<T>, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _modalHtmlAttributes;
        private Expression<Func<T, object>> _column;
        private HtmlHelper _helper;

        public BSFilterInputFor(HtmlHelper helper, Expression<Func<T, object>> column)
        {
            _helper = helper;
            _inputType = FilterGroupType.Text;
            _column = column;
        }

        public IBSFilterInputFor<T> SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }
        public IBSFilterInputFor<T> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterInputFor<T> SetInputType(FilterGroupType inputType = FilterGroupType.Text)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterInputFor<T> SetHtmlAttributes(object htmlAttributes)
        {
            _modalHtmlAttributes = htmlAttributes;
            return this;
        }

        public string ToHtmlString()
        {
            var propInfo = Util.GetPropertyInfo<T>(_column);
            string customTitle = Util.GetDisplayName<T>(propInfo);

            var id = "";
            if(!_id.IsBlank())
            {
                id = _id;
            }
            else
            {
                id = propInfo.Name;
            }

            return ((BSFilterInput)(new BSFilterInput(id).SetTitle(customTitle).SetInputType(_inputType))).ToHtmlString();
        }
    }
}