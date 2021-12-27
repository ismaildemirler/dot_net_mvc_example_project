using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WebHelpers.HtmlHelpers.Core
{
    public class ActionAttribute : Attribute
    {
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string Parameters { get; set; }
    }
}
