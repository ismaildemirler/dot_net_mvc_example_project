using System;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Core;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Enum;
using Domain.Infrastructure.Utilities.ExtensionMethods;

namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Enum
{
    public class BSFilterEnumCheckListFor<TModel, TEnum> : IBSFilterEnumCheckListFor<TModel,TEnum>, IHtmlString
    {
        private HtmlHelper _htmlHelper;
        private string _id;
        private string _customTitle;
        private FilterGroupType _inputType;
        private object _htmlAttributes;
        private Expression<Func<TModel, TEnum>> _expression;

        public BSFilterEnumCheckListFor(HtmlHelper htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            _htmlHelper = htmlHelper;
            _inputType = FilterGroupType.Numeric;
            _expression = expression;
        }

        public IBSFilterEnumCheckListFor<TModel,TEnum> SetCustomTitle(string customTitle)
        {
            _customTitle = customTitle;
            return this;
        }
        public IBSFilterEnumCheckListFor<TModel,TEnum> SetHtmlAttributes(object htmlAttributes)
        {
            _htmlAttributes = htmlAttributes;
            return this;
        }
        public IBSFilterEnumCheckListFor<TModel, TEnum> SetCustomID(string customId)
        {
            _id = customId;
            return this;
        }

        public string ToHtmlString()
        {
            var propInfo = Util.GetPropertyInfo<TModel, TEnum>(_expression);
            string customTitle = Util.GetDisplayName<TModel>(propInfo);

            var id = "";
            if (!_id.IsBlank())
            {
                id = _id;
            }
            else
            {
                id = propInfo.Name;
            }

            return ((BSFilterEnumCheckList<TEnum>)(new BSFilterEnumCheckList<TEnum>(id).SetTitle(customTitle))).ToHtmlString();
        }
    }
}