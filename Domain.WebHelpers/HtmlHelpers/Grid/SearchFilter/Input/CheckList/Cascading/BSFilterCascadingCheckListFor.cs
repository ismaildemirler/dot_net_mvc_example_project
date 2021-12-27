using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Cascading
{
    public class BSFilterCascadingCheckListFor<T> : IBSFilterCascadingCheckListFor<T>, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        private IEnumerable<SelectListItem> _selectList;
        private string _targetElemetID;
        private string _targetUrl;
        private Expression<Func<T, object>> _column;

        public BSFilterCascadingCheckListFor(Expression<Func<T, object>> column)
        {
            _column = column;
        }

        public IBSFilterCascadingCheckListFor<T> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            _selectList = selectList;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetTargetElemetID(string targetElemetID)
        {
            _targetElemetID = targetElemetID;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetTargetUrl(string targetUrl)
        {
            _targetUrl = targetUrl;
            return this;
        }
        public IBSFilterCascadingCheckListFor<T> SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }

        public string ToHtmlString()
        {
            var propInfo = Util.GetPropertyInfo<T>(_column);

            if (_customTitle.IsBlank())
                _customTitle = Util.GetDisplayName<T>(propInfo);

            var id = "";
            if (!_id.IsBlank())
            {
                id = _id;
            }
            else
            {
                id = propInfo.Name;
            }

            return ((BSFilterCascadingCheckList)(new BSFilterCascadingCheckList(id).SetTitle(_customTitle).SetInputType(_inputType).SetSelectList(_selectList).SetTargetElemetID(_targetElemetID).SetTargetUrl(_targetUrl))).ToHtmlString();
        }

    }
}