using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Container.Request.Services
{
    public class RequestService
    {
        public int ServiceID { get; set; }
        public byte[] Icon { get; set; }
        public byte[] ServiceImage { get; set; }
        public string Header { get; set; }
        public string DescriptionText { get; set; }
        public string ContentText { get; set; }
        public bool ImageDeleted { get; set; }
        public bool IconDeleted { get; set; }
    }
}
