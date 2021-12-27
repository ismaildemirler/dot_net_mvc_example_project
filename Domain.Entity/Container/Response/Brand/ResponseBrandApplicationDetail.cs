using Domain.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.Brand
{
    public class ResponseBrandApplicationDetail
    {
        public BrandApplicationDetail BrandApplicationDetail { get; set; }
        public List<CustomerApplicationBrandClasses> CustomerApplicationBrandClasses { get; set; }
    }
}
