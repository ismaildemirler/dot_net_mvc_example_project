using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WebHelpers.HtmlHelpers.Input.Button.Enum
{
    public enum ButtonSize
    {
        [Display(Name = "")]
        @default,
        [Display(Name = "btn-lg")]
        large,
        [Display(Name = "btn-sm")]
        small,
        [Display(Name = "btn-xs")]
        extrasmall
    }
}
