using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WebApp.Rule.Util
{
    public enum AlertType
    {
        [Display(Name = "primary")]
        Primary,
        [Display(Name = "success")]
        Success,
        [Display(Name = "danger")]
        Danger,
        [Display(Name = "warning")]
        Warning,
        [Display(Name = "info")]
        Information,
        [Display(Name = "default")]
        Default
    }
}
