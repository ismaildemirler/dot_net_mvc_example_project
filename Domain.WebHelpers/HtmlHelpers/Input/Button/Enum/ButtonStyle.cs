using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WebHelpers.HtmlHelpers.Input.Button.Enum
{
    public enum ButtonStyle
    {
        [Display(Name = "btn-default")]
        @default,
        [Display(Name = "btn-primary")]
        primary,
        [Display(Name = "btn-secondary")]
        secondary,
        [Display(Name = "btn-success")]
        success,
        [Display(Name = "btn-info")]
        info,
        [Display(Name = "btn-warning")]
        warning,
        [Display(Name = "btn-danger")]
        danger,

        [Display(Name = "btn-link")]
        link,
        [Display(Name = "cbutton")]
        RoundedIcon,
        [Display(Name = "circlebutton")]
        RoundedSmallIcon,
        [Display(Name = "cbutton")]
        RoundedImage,

        [Display(Name = "btn-outline-default")]
        @default_outline,
        [Display(Name = "btn-outline-secondary")]
        secondary_outline,
        [Display(Name = "btn-outline-primary")]
        primary_outline,
        [Display(Name = "btn-outline-success")]
        success_outline,
        [Display(Name = "btn-outline-info")]
        info_outline,
        [Display(Name = "btn-outline-warning")]
        warning_outline,
        [Display(Name = "btn-outline-danger")]
        danger_outline,
        [Display(Name = "btn btn-success m-btn m-btn--custom m-btn--icon")]
        save_continue,
        [Display(Name = "btn btn-secondary m-btn m-btn--custom m-btn--icon")]
        go_back,
        [Display(Name = "btn btn-primary m-btn m-btn--custom m-btn--icon")]
        submit
    }
}
