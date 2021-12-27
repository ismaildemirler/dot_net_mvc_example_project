using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base
{
    public class BSFilterCheckListFor<T> : IBSFilterCheckListFor<T>, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        private IEnumerable<SelectListItem> _selectList;
        private Expression<Func<T, object>> _column;
        private HtmlHelper _htmlHelper;
        private string _tooltip;

        public BSFilterCheckListFor(HtmlHelper<T> htmlHelper, Expression<Func<T, object>> column)
        {
            _htmlHelper = htmlHelper;
            _column = column;
        }

        public IBSFilterCheckListFor<T> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterCheckListFor<T> SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterCheckListFor<T> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterCheckListFor<T> SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            _selectList = selectList;
            return this;
        }
        public IBSFilterCheckListFor<T> SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }
        public IBSFilterCheckListFor<T> SetTooltip(string tooltip)
        {
            _tooltip = tooltip;
            return this;
        }

        public string ToHtmlString()
        {
            var propInfo = Util.GetPropertyInfo<T>(_column);

            if(_customTitle.IsBlank())
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

            return ((BSFilterCheckList)(new BSFilterCheckList(id).SetTitle(_customTitle).SetInputType(_inputType).SetSelectList(_selectList)).SetTooltip(_tooltip)).ToHtmlString();
        }

    }
}