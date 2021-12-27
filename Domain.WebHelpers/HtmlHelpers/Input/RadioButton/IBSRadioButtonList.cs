using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Domain.WebHelpers.HtmlHelpers.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox.Enum;

namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public interface IBSRadioButtonList
    {
        IBSRadioButtonList SetID(string id);
        IBSRadioButtonList SetTitle(string title);
        IBSRadioButtonList SetHelpText(string helpText);
        IBSRadioButtonList SetLayout(LayoutType layoutType);
        IBSRadioButtonList SetSelectedValue(object selectedValue);
        IBSRadioButtonList SetRadioButtons(Action<BSRadioButtonBuilder> radioBuilder);
        IBSRadioButtonList SetSelectList(List<SelectListItem> selectList);
        IBSRadioButtonList SetSolid(bool isSolid);
        IBSRadioButtonList SetState(StateType state = HtmlHelpers.Input.Checkbox.Enum.StateType.@default);
        IBSRadioButtonList SetParentCss(string css = "");
        IBSRadioButtonList SetTitleHidden(bool isHidden);
        IBSRadioButtonList SetCallBackFunction(string functionName);
        IBSRadioButtonList SetDisplayExpression(string source, string expression, bool isAlsoRequired = false);
    }
}
