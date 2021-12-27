namespace Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Enum
{
    public interface IBSFilterEnumCheckListFor<TModel, TEnum>
    {
        IBSFilterEnumCheckListFor<TModel, TEnum> SetCustomID(string customId);
        IBSFilterEnumCheckListFor<TModel, TEnum> SetCustomTitle(string customTitle);
        IBSFilterEnumCheckListFor<TModel, TEnum> SetHtmlAttributes(object htmlAttributes); 
    }
}
