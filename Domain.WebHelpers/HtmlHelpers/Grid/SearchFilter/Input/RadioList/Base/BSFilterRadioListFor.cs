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
    public class BSFilterRadioListFor<T> : IBSFilterRadioListFor<T>, IHtmlString
    {
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        private IEnumerable<SelectListItem> _selectList;
        private Expression<Func<T, object>> _column;
        private HtmlHelper _htmlHelper;
        private string _tooltip;

        public BSFilterRadioListFor(HtmlHelper<T> htmlHelper, Expression<Func<T, object>> column)
        {
            _htmlHelper = htmlHelper;
            _column = column;
        }

        public IBSFilterRadioListFor<T> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterRadioListFor<T> SetInputType(FilterGroupType inputType)
        {
            _inputType = inputType;
            return this;
        }
        public IBSFilterRadioListFor<T> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterRadioListFor<T> SetSelectList(IEnumerable<SelectListItem> selectList)
        {
            _selectList = selectList;
            return this;
        }
        public IBSFilterRadioListFor<T> SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }
        public IBSFilterRadioListFor<T> SetTooltip(string tooltip)
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

            return ((BSFilterRadioList)(new BSFilterRadioList(id).SetTitle(_customTitle).SetInputType(_inputType).SetSelectList(_selectList)).SetTooltip(_tooltip)).ToHtmlString();
        }

    }
}