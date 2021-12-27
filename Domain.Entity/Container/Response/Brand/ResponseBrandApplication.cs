using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Response.Brand
{
    public class ResponseBrandApplication
    {
        public string BrandClassesArray { get; set; } 
        public CustomerApplication CustomerApplication { get; set; }
        public BrandApplicationDetail BrandApplicationDetail { get; set; }
    }
}
