using System;
using System.Linq.Expressions;
using Domain.WebHelpers.HtmlHelpers.Accordion;
using Domain.WebHelpers.HtmlHelpers.Grid;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Container;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Base;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Cascading;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.CheckList.Enum;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Range;
using Domain.WebHelpers.HtmlHelpers.Grid.SearchFilter.Input.Text;
using Domain.WebHelpers.HtmlHelpers.Input.AutoComplete;
using Domain.WebHelpers.HtmlHelpers.Input.BasicFileUpload;
using Domain.WebHelpers.HtmlHelpers.Input.Button;
using Domain.WebHelpers.HtmlHelpers.Input.Checkbox;
using Domain.WebHelpers.HtmlHelpers.Input.DateRangePicker;
using Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker;
using Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Base;
using Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Enum;
using Domain.WebHelpers.HtmlHelpers.Input.DropdownList.Numeric;
using Domain.WebHelpers.HtmlHelpers.Input.FileUpload;
using Domain.WebHelpers.HtmlHelpers.Input.MultiSelectList;
using Domain.WebHelpers.HtmlHelpers.Input.NumberBox;
using Domain.WebHelpers.HtmlHelpers.Input.RadioButton;
using Domain.WebHelpers.HtmlHelpers.Input.TextArea;
using Domain.WebHelpers.HtmlHelpers.Input.TextBox;
using Domain.WebHelpers.HtmlHelpers.Tab;

namespace Domain.WebHelpers.HtmlHelpers.DomainHelper
{
    public interface IDomain
    {
        IBSCheckBox CheckBox(string id);
        IBSCheckBoxContainer CheckBoxContainer(string title);
        IBSRadioButtonList RadioButtonList(string name);
        IBSTextBox TextBox(string id);
        IBSNumberBox NumberBox(string id);
        IBSTextArea TextArea(string id);
        IBSDatetimePicker DateTimePicker(string id);
        IBSDropDownList DropDownList(string id);
        IBSNumericDropDownList NumericDropDownList(string id);
        IBSEnumDropDownList<TEnum> EnumDropDownList<TEnum>(string id);
        IBSDateRangePicker DateRangePicker(string startDateId, string endDateId);
        IBSMultiSelectList MultiSelectList(string id);
        IBSAutoComplete AutoComplete(string id);
        IBSButton Button(string id);
        IBSFilter FilterContainer(string id, string title = "Filtre");
        IBSFilterInput FilterInput(string id);
        IBSFilterCheckList FilterCheckList(string id);
        IBSFilterRadioList FilterRadioList(string id);
        BSFilterEnumCheckList<TEnum> FilterEnumCheckList<TEnum>(string id);
        BSFilterCascadingCheckList FilterCascadingCheckList(string id);
        BSFilterRange FilterRange(string id);
        IBSGrid<TModel> Grid<TModel>(string id = "") where TModel : class;
        IBSGrid Grid(string id = "");
        IBSFileUploader FileUpload(string urlAction);
        IBSBasicFileUpload BasicFileUpload(string id);
        IBSAccordion Accordion(string id);
        IBSTab Tab(string id);
    }

    public interface IDomain<TModel> where TModel : class
    {
        IBSCheckBoxFor<TModel> CheckBoxFor(Expression<Func<TModel, bool>> expression);
        IBSRadioButtonListFor<TModel> RadioButtonListFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSTextBoxFor<TModel> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSNumberBoxFor<TModel> NumberBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSTextAreaFor<TModel> TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSDatetimePickerFor<TModel> DateTimePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSDropDownListFor<TModel> DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSNumericDropDownListFor<TModel> NumericDropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSEnumDropDownListFor<TModel, TEnum> EnumDropDownListFor<TEnum>(Expression<Func<TModel, TEnum>> expression);
        IBSDateRangePickerFor<TModel> DateRangePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expressionStartDate, Expression<Func<TModel, TProperty>> expressionEndDate);
        IBSMultiSelectListFor<TModel> MultiSelectListFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSAutoCompleteFor<TModel> AutoCompleteFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
        IBSFilterCheckListFor<TModel> FilterCheckListFor(Expression<Func<TModel, object>> expression);
        IBSFilterRadioListFor<TModel> FilterRadioListFor(Expression<Func<TModel, object>> expression);
        IBSFilterInputFor<TModel> FilterInputFor(Expression<Func<TModel, object>> expression);
        BSFilterRangeFor<TModel> FilterRangeFor(Expression<Func<TModel, object>> expression);
        BSFilterEnumCheckListFor<TModel, TEnum> FilterEnumCheckListFor<TEnum>(Expression<Func<TModel, TEnum>> expression);
        BSFilterCascadingCheckListFor<TModel> FilterCascadingCheckListFor(Expression<Func<TModel, object>> expression);
        IBSGrid<TModel> Grid(string id = "");
        IBSBasicFileUploadFor<TModel> BSBasicFileUploadFor<TProperty>(Expression<Func<TModel, TProperty>> expression);
    }
}
