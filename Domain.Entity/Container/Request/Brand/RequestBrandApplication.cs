using Domain.Entity.Concrete;
using System;
using System.Collections.Generic;

namespace Domain.Entity.Container.Request.Brand
{
    public class RequestBrandApplication
    {
        public Guid BrandApplicationDetailIdForInsert { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public BrandApplicationDetail BrandApplicationDetail { get; set; }
        public List<CustomerApplicationBrandClasses> CustomerApplicationBrandClasses { get; set; }
    }
}
