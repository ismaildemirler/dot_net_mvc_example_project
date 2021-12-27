namespace Domain.WebHelpers.HtmlHelpers.Input.RadioButton
{
    public interface IBSRadioButton
    {
        IBSRadioButton SetValue(object value);
        IBSRadioButton SetText(string text);
        IBSRadioButton SetDisable(bool isDisabled);
        IBSRadioButton SetSolid(bool isSolid);
        IBSRadioButton SetHtmlAttributes(object htmlAttributes);
    }
}
