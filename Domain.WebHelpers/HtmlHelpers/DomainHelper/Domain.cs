using System;
using System.Linq.Expressions;
using System.Web.Mvc;
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
    public class Domain : IDomain
    {
        private HtmlHelper _helper;
        public Domain(HtmlHelper helper)
        {
            _helper = helper;
        }

        public IBSCheckBox CheckBox(string id)
        {
            return new BSCheckBox(_helper, id);
        }
        public IBSCheckBoxContainer CheckBoxContainer(string title)
        {
            return new BSCheckBoxContainer(_helper, title);
        }
        public IBSRadioButtonList RadioButtonList(string name)
        {
            return new BSRadioButtonList(_helper, name);
        }
        public IBSTextBox TextBox(string id)
        {
            return new BSTextBox(_helper, id);
        }
        public IBSNumberBox NumberBox(string id)
        {
            return new BSNumberBox(_helper, id);
        }
        public IBSTextArea TextArea(string id)
        {
            return new BSTextArea(_helper, id);
        }
        public IBSDatetimePicker DateTimePicker(string id)
        {
            return new BSDateTimePicker(_helper, id);
        }
        public IBSDropDownList DropDownList(string id)
        {
            return new BSDropDownList(_helper, id);
        }
        public IBSNumericDropDownList NumericDropDownList(string id)
        {
            return new BSNumericDropDownList(_helper, id);
        }
        public IBSEnumDropDownList<TEnum> EnumDropDownList<TEnum>(string id)
        {
            return new BSEnumDropDownList<TEnum>(_helper, id);
        }
        public IBSDateRangePicker DateRangePicker(string startDateId, string endDateId)
        {
            return new BSDateRangePicker(_helper, startDateId, endDateId);
        }
        public IBSMultiSelectList MultiSelectList(string id)
        {
            return new BSMultiSelectList(_helper, id);
        }
        public IBSAutoComplete AutoComplete(string id)
        {
            return new BSAutoComplete(_helper, id);
        }
        public IBSButton Button(string id = "")
        {
            return new BSButton(id);
        }
        public IBSFilter FilterContainer(string id, string title = "Filtre")
        {
            return new BSFilter(_helper, id, title);
        }
        public IBSFilterCheckList FilterCheckList(string id)
        {
            return new BSFilterCheckList(id);
        }
        public IBSFilterRadioList FilterRadioList(string id)
        {
            return new BSFilterRadioList(id);
        }
        public IBSFilterInput FilterInput(string id)
        {
            return new BSFilterInput(id);
        }
        public BSFilterEnumCheckList<TEnum> FilterEnumCheckList<TEnum>(string id)
        {
            return new BSFilterEnumCheckList<TEnum>(id);
        }
        public BSFilterCascadingCheckList FilterCascadingCheckList(string id)
        {
            return new BSFilterCascadingCheckList(id);
        }
        public BSFilterRange FilterRange(string id)
        {
            return new BSFilterRange(id);
        }
        public IBSGrid<TModel> Grid<TModel>(string id = "") where TModel : class
        {
            return new BSGrid<TModel>(_helper, id);
        }
        public IBSGrid Grid(string id = "")
        {
            return new BSGrid(_helper, id);
        }
        public IBSFileUploader FileUpload(string urlAction)
        {
            return new BSFileUploader(urlAction);
        }
        public IBSBasicFileUpload BasicFileUpload(string id)
        {
            return new BSBasicFileUpload(_helper, id);
        }

        public IBSAccordion Accordion(string id)
        {
            return  new BSAccordion(id);
        }

        public IBSTab Tab(string id)
        {
            return new BSTab(id);
        }
    }

    public class Domain<TModel> : IDomain<TModel> where TModel : class
    {
        private HtmlHelper<TModel> _helper;
        public Domain(HtmlHelper<TModel> helper)
        {
            _helper = helper;
        }

        public IBSCheckBoxFor<TModel> CheckBoxFor(Expression<Func<TModel, bool>> expression)
        {
            return new BSCheckBoxFor<TModel>(_helper, expression);
        }
        public IBSRadioButtonListFor<TModel> RadioButtonListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSRadioButtonListFor<TModel, TProperty>(_helper, expression);
        }
        public IBSTextBoxFor<TModel> TextBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSTextBoxFor<TModel, TProperty>(_helper, expression);
        }
        public IBSNumberBoxFor<TModel> NumberBoxFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSNumberBoxFor<TModel, TProperty>(_helper, expression);
        }
        public IBSTextAreaFor<TModel> TextAreaFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSTextAreaFor<TModel, TProperty>(_helper, expression);
        }
        public IBSDatetimePickerFor<TModel> DateTimePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSDateTimePickerFor<TModel, TProperty>(_helper, expression);
        }
        public IBSDropDownListFor<TModel> DropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSDropDownListFor<TModel, TProperty>(_helper, expression);
        }
        public IBSNumericDropDownListFor<TModel> NumericDropDownListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSNumericDropDownListFor<TModel, TProperty>(_helper, expression);
        }
        public IBSEnumDropDownListFor<TModel, TEnum> EnumDropDownListFor<TEnum>(Expression<Func<TModel, TEnum>> expression)
        {
            return new BSEnumDropDownListFor<TModel, TEnum>(_helper, expression);
        }
        public IBSDateRangePickerFor<TModel> DateRangePickerFor<TProperty>(Expression<Func<TModel, TProperty>> expressionStart, Expression<Func<TModel, TProperty>> expressionEnd)
        {
            return new BSDateRangePickerFor<TModel, TProperty>(_helper, expressionStart, expressionEnd);
        }
        public IBSMultiSelectListFor<TModel> MultiSelectListFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSMultiSelectListFor<TModel, TProperty>(_helper, expression);
        }
        public IBSAutoCompleteFor<TModel> AutoCompleteFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSAutoCompleteFor<TModel, TProperty>(_helper, expression);
        }

        public IBSFilterCheckListFor<TModel> FilterCheckListFor(Expression<Func<TModel, object>> expression)
        {
            return new BSFilterCheckListFor<TModel>(_helper, expression);
        }
        public IBSFilterRadioListFor<TModel> FilterRadioListFor(Expression<Func<TModel, object>> expression)
        {
            return new BSFilterRadioListFor<TModel>(_helper, expression);
        }
        public IBSFilterInputFor<TModel> FilterInputFor(Expression<Func<TModel, object>> expression)
        {
            return new BSFilterInputFor<TModel>(_helper, expression);
        }
        public BSFilterEnumCheckListFor<TModel, TEnum> FilterEnumCheckListFor<TEnum>(Expression<Func<TModel, TEnum>> expression)
        {
            return new BSFilterEnumCheckListFor<TModel, TEnum>(_helper, expression);
        }
        public BSFilterCascadingCheckListFor<TModel> FilterCascadingCheckListFor(Expression<Func<TModel, object>> expression)
        {
            return new BSFilterCascadingCheckListFor<TModel>(expression);
        }
        public BSFilterRangeFor<TModel> FilterRangeFor(Expression<Func<TModel, object>> expression)
        {
            return new BSFilterRangeFor<TModel>(expression);
        }
        public IBSGrid<TModel> Grid(string id = "") 
        {
            return new BSGrid<TModel>(_helper, id);
        }
        public IBSBasicFileUploadFor<TModel> BSBasicFileUploadFor<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return new BSBasicFileUploadFor<TModel,TProperty>(_helper,expression);
        }
    }
}
