using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.BrandClass
{
    public class ResponseBrandSubClasses
    {
        public ResponseBrandSubClasses()
        {
            BrandSubClasses = new List<BrandSubClasses>();
        }

        public List<BrandSubClasses> BrandSubClasses { get; set; }
    }
}
