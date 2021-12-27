using System.ComponentModel.DataAnnotations;

namespace Domain.WebHelpers.HtmlHelpers.Input.DatetimePicker.Enum
{
    public enum DateType
    {
        [Display(Name = "date")]
        Date,
        [Display(Name = "datetime-local")]
        DatetimeLocal,
        [Display(Name = "month")]
        Month,
        [Display(Name = "time")]
        Time
    }
}
