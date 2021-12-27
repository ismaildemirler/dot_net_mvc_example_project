using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.ComplexType
{
    public class BlogContentComplexType
    {
        public Guid BlogContentId { get; set; }
        public string Title { get; set; }
        public string Contents { get; set; }
        public string ShortContent { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public byte[] PostImage { get; set; }
        public DateTime PostDate { get; set; }
    }
}
