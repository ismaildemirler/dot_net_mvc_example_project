using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Entities
{
    public class WebSelectListItem
    {
        public string ParentValue { get; set; }
        public string Value { get; set; }
        public string Text { get; set; }
        public bool Selected { get; set; }
    }
}
