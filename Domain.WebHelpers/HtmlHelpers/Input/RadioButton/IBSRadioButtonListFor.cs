using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public interface IBSRadioButtonListFor<TModel>
    {
        IBSRadioButtonListFor<TModel> SetLayout(LayoutType layoutType);
        IBSRadioButtonListFor<TModel> SetRadioButtons(Action<BSRadioButtonBuilder> radioBuilder);
        IBSRadioButtonListFor<TModel> SetSelectList(List<SelectListItem> selectList);
        IBSRadioButtonListFor<TModel> SetSolid(bool isSolid);
        IBSRadioButtonListFor<TModel> SetState(StateType state = HtmlHelpers.Input.Checkbox.Enum.StateType.@default);
        IBSRadioButtonListFor<TModel> SetParentCss(string css = "");
        IBSRadioButtonListFor<TModel> SetTitleHidden(bool isHidden);
        IBSRadioButtonListFor<TModel> SetCallBackFunction(string functionName);
        IBSRadioButtonListFor<TModel> SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
        IBSRadioButtonListFor<TModel> SetCustomTitle(string customTitle);
    }
}
