using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.BrandClass
{
    public class ResponseBrandClasses
    {
        public ResponseBrandClasses()
        {
            BrandClasses = new List<BrandClasses>();
        }

        public List<BrandClasses> BrandClasses { get; set; }
    }
}
